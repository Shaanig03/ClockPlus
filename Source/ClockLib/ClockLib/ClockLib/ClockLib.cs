using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Threading;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using System.DirectoryServices;
using BirdNest;
using BirdNest.Experimental;

using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;
using System.Media;
using System.Reflection;
using Windows.Devices.Display.Core;

using System.Net;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Text;
using System.Runtime.InteropServices;
using NAudio.Wave;
namespace ClockLib
{


    /// <summary>
    /// ClockLib is a c# wpf library for quickly implementing alarms, timers and a stopwatch to your application
    /// 
    /// NOTE: I lost the latest version of Clock Plus project files, I only had a released build and previous versions or backups of the project files, so I copied the decompiled contents of the dll from the latest released build  to a previous version
    /// there might not be comments since it was copied and pasted from dnSpy, check out the previous version project file to see comments
    /// </summary>
    public static class ClockLib
    {
        public static string selectedAlarmSound = "default";
        // Token: 0x0600000C RID: 12 RVA: 0x00002100 File Offset: 0x00000300
        public static void Load()
        {
            ClockLib.executablePath = AppDomain.CurrentDomain.BaseDirectory;
            ClockLib.executablePath = ClockLib.executablePath.Substring(0, ClockLib.executablePath.Length - 1);
            ClockLib.f_alarms = ClockLib.executablePath + "\\alarms.xml";
            ClockLib.storage_alarms = new XMLStorageAlarms(ClockLib.f_alarms, true);
            ClockLib.Update(true);
            ClockLib.Load_AlarmSounds();
            ToastNotificationManagerCompat.OnActivated += ClockLib.ToastNotificationManagerCompat_OnActivated;
            ClockLib.timer.Interval = new TimeSpan(0, 0, 1);
            ClockLib.timer.Tick += ClockLib.Timer_Tick;
            ClockLib.timer.Start();
            ClockLib.snoozeUpdateTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            ClockLib.snoozeUpdateTimer.Tick += ClockLib.SnoozeUpdateTimer_Tick;
            ClockLib.displayTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            ClockLib.displayTimer.Tick += ClockLib.DisplayTimer_Tick;
            ClockLib.timerDefTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            ClockLib.timerDefTimer.Tick += ClockLib.TimerDefTimer_Tick;
            ClockLib.soundPlayer2Looper.Interval = new TimeSpan(0, 0, 1);
            ClockLib.soundPlayer2Looper.Tick += ClockLib.SoundPlayer2Looper_Tick;
            ClockLib.stopSoundTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            ClockLib.stopSoundTimer.Tick += ClockLib.StopSoundTimer_Tick;
        }

        // Token: 0x0600000D RID: 13 RVA: 0x0000228D File Offset: 0x0000048D
        public static void UpdateVolume()
        {
            ClockLib.soundPlayer2.Volume = ClockLib.soundVolume;
        }

        // Token: 0x0600000E RID: 14 RVA: 0x000022A0 File Offset: 0x000004A0
        private static void StopSoundTimer_Tick(object sender, EventArgs e)
        {
            ClockLib.StopSound();
            ClockLib.stopSoundTimer.Stop();
        }

        // Token: 0x0600000F RID: 15 RVA: 0x000022B4 File Offset: 0x000004B4
        private static void SoundPlayer2Looper_Tick(object sender, EventArgs e)
        {
            ClockLib.soundPlayer2.Open(new Uri(ClockLib.soundPlayer2_soundPath));
            ClockLib.soundPlayer2.Volume = ClockLib.soundVolume;
            ClockLib.soundPlayer2.Play();
            Debug.WriteLine("playing sound");
        }

        // Token: 0x06000010 RID: 16 RVA: 0x000022F4 File Offset: 0x000004F4
        private static void TimerDefTimer_Tick(object sender, EventArgs e)
        {
            bool flag = ClockLib.timerDefTimer_disable;
            if (!flag)
            {
                DateTime now = DateTime.Now;
                bool needsUpdating = false;
                foreach (KeyValuePair<int, AlarmItemDef> keyValue in ClockLib.timerDefs)
                {
                    AlarmItemDef timerDef = keyValue.Value;
                    TimeSpan remainingTime = timerDef.assigned_alarmPeriod - now;
                    bool flag2 = timerDef.enabled && !ClockLib.timerDefTimer_disable;
                    if (flag2)
                    {
                        bool flag3 = now >= timerDef.assigned_alarmPeriod && remainingTime.Milliseconds <= 0 && !ClockLib.timerDefTimer_disable;
                        if (flag3)
                        {
                            ClockLib.ExecuteCommands(timerDef.commands);
                            ClockLib.ResetTimer(timerDef, true);
                            new ClockNotification
                            {
                                alarmedTime = timerDef.assigned_alarmPeriod,
                                data = timerDef,
                                ntfType = ClockNotificationType.Timer
                            }.ShowNotification();
                            //#1
                            needsUpdating = true;
                        }
                    }
                }
                bool flag4 = needsUpdating;
                if (flag4)
                {
                    ClockLib.Update(true);
                    ClockLib.StartOrStop_FastTimerAndDisplayTimer();
                }
            }
        }

        // Token: 0x06000011 RID: 17 RVA: 0x00002418 File Offset: 0x00000618
        public static void StartOrStop_FastTimerAndDisplayTimer()
        {
            bool any_timer_running = false;
            foreach (KeyValuePair<int, AlarmItemDef> _keyValue in ClockLib.timerDefs)
            {
                bool enabled = _keyValue.Value.enabled;
                if (enabled)
                {
                    any_timer_running = true;
                    break;
                }
            }
            bool flag = ClockLib.stopWatchEnabled;
            if (flag)
            {
                any_timer_running = true;
            }
            bool flag2 = any_timer_running;
            if (flag2)
            {
                ClockLib.displayTimer.Start();
                ClockLib.timerDefTimer.Start();
            }
            else
            {
                ClockLib.displayTimer.Stop();
                ClockLib.timerDefTimer.Stop();
            }
        }

        // Token: 0x06000012 RID: 18 RVA: 0x000024C8 File Offset: 0x000006C8
        private static void DisplayTimer_Tick(object sender, EventArgs e)
        {
            bool flag = ClockLib.mainController != null;
            if (flag)
            {
                ClockLib.mainController.UpdateDisplayTime();
            }
            ClockLib.UpdateStopWatchDisplay();
        }

        // Token: 0x06000013 RID: 19 RVA: 0x000024F8 File Offset: 0x000006F8
        public static void StopWatch_Reset()
        {
            ClockLib.stopWatchEnabled = false;
            ClockLib.stopWatchTime = TimeSpan.Zero;
            ClockLib.label_stopWatchTime.Foreground = Brushes.Gray;
            ClockLib.label_stopWatchTime.Content = "00:00:00";
            ClockLib.StartOrStop_FastTimerAndDisplayTimer();
            ClockLib.storage_alarms.DoSave();
        }

        // Token: 0x06000014 RID: 20 RVA: 0x00002548 File Offset: 0x00000748
        public static void StopWatch_Stop()
        {
            bool flag = !ClockLib.stopWatchEnabled;
            if (!flag)
            {
                ClockLib.stopWatchEnabled = false;
                ClockLib.stopWatchTime = ClockLib.GetStopWatchTime();
                ClockLib.label_stopWatchTime.Foreground = Brushes.Gray;
                ClockLib.StartOrStop_FastTimerAndDisplayTimer();
                ClockLib.storage_alarms.DoSave();
            }
        }

        // Token: 0x06000015 RID: 21 RVA: 0x00002598 File Offset: 0x00000798
        public static void StopWatch_Play()
        {
            bool flag = ClockLib.stopWatchEnabled;
            if (!flag)
            {
                bool flag2 = ClockLib.stopWatchTime > TimeSpan.Zero;
                if (flag2)
                {
                    ClockLib.stopWatchPlayPeriod = DateTime.Now.Subtract(ClockLib.stopWatchTime);
                }
                else
                {
                    ClockLib.stopWatchPlayPeriod = DateTime.Now;
                }
                ClockLib.stopWatchEnabled = true;
                ClockLib.label_stopWatchTime.Foreground = Brushes.Black;
                ClockLib.StartOrStop_FastTimerAndDisplayTimer();
            }
        }

        // Token: 0x06000016 RID: 22 RVA: 0x00002608 File Offset: 0x00000808
        public static void UpdateStopWatchDisplay()
        {
            bool flag = ClockLib.stopWatchEnabled;
            if (flag)
            {
                TimeSpan stopWatchTime = ClockLib.GetStopWatchTime();
                string formattedTime = stopWatchTime.GetFullTimeInString();
                ClockLib.label_stopWatchTime.Content = formattedTime;
            }
        }

        // Token: 0x06000017 RID: 23 RVA: 0x0000263C File Offset: 0x0000083C
        public static TimeSpan GetStopWatchTime()
        {
            return DateTime.Now - ClockLib.stopWatchPlayPeriod;
        }

        // Token: 0x06000018 RID: 24 RVA: 0x00002660 File Offset: 0x00000860
        public static string GetFullTimeInString(this TimeSpan timeSpan)
        {
            return string.Format("{0:hh\\:mm\\:ss\\.ff}", timeSpan);
        }

        // Token: 0x06000019 RID: 25 RVA: 0x00002684 File Offset: 0x00000884
        public static void UpdateTimer(this AlarmItemDef timerDef, string title, string time)
        {
            ClockLib.timerDefTimer_disable = true;
            timerDef.title = title;
            timerDef.alarmTime = time;
            timerDef.enabled = false;
            timerDef.timer_remainingTime = TimeSpan.Zero;
            timerDef.str_current_alarmPeriod = "";
            timerDef.assigned_alarmPeriod = new DateTime(1, 1, 1);
            ClockLib.Update(true);
            ClockLib.StartOrStop_FastTimerAndDisplayTimer();
        }

        // Token: 0x0600001A RID: 26 RVA: 0x000026E0 File Offset: 0x000008E0
        public static void ResetTimer(AlarmItemDef timerDef, bool pause = false)
        {
            bool flag = timerDef.str_current_alarmPeriod == "";
            if (!flag)
            {
                ClockLib.timerDefTimer_disable = true;
                DateTime now = DateTime.Now;
                DateTime new_alarmPeriod = now.Add(TimeSpan.Parse(timerDef.alarmTime));
                if (pause)
                {
                    timerDef.enabled = false;
                }
                timerDef.assigned_alarmPeriod = new_alarmPeriod;
                timerDef.str_current_alarmPeriod = new_alarmPeriod.ToString();
                timerDef.timer_remainingTime = new_alarmPeriod - now;
                ClockLib.Update(true);
                ClockLib.StartOrStop_FastTimerAndDisplayTimer();
            }
        }

        // Token: 0x0600001B RID: 27 RVA: 0x00002760 File Offset: 0x00000960
        public static void SetSelectedTimerDef(AlarmItemDef timerDef)
        {
            ClockLib.current_sel_timerDef = timerDef;
        }

        // Token: 0x0600001C RID: 28 RVA: 0x0000276C File Offset: 0x0000096C
        public static void StartOrStopTimer(AlarmItemDef timerDef)
        {
            bool enabled = timerDef.enabled;
            if (enabled)
            {
                ClockLib.StopTimer(timerDef);
            }
            else
            {
                ClockLib.StartTimer(timerDef);
            }
        }

        // Token: 0x0600001D RID: 29 RVA: 0x00002798 File Offset: 0x00000998
        public static void StopTimer(AlarmItemDef timerDef)
        {
            bool flag = !timerDef.enabled;
            if (!flag)
            {
                ClockLib.timerDefTimer_disable = true;
                timerDef.timer_remainingTime = timerDef.assigned_alarmPeriod - DateTime.Now;
                timerDef.enabled = false;
                ClockLib.Update(true);
                ClockLib.StartOrStop_FastTimerAndDisplayTimer();
            }
        }

        // Token: 0x0600001E RID: 30 RVA: 0x000027E8 File Offset: 0x000009E8
        public static void StartTimer(AlarmItemDef timerDef)
        {
            bool enabled = timerDef.enabled;
            if (!enabled)
            {
                ClockLib.timerDefTimer_disable = true;
                string assigned_alarmPeriod = timerDef.str_current_alarmPeriod;
                bool flag = assigned_alarmPeriod == "";
                if (flag)
                {
                    DateTime alarmPeriod = DateTime.Now.Add(TimeSpan.Parse(timerDef.alarmTime));
                    timerDef.assigned_alarmPeriod = alarmPeriod;
                    timerDef.str_current_alarmPeriod = alarmPeriod.ToString();
                }
                else
                {
                    DateTime new_alarmPeriod = DateTime.Now.Add(timerDef.timer_remainingTime);
                    timerDef.assigned_alarmPeriod = new_alarmPeriod;
                    timerDef.str_current_alarmPeriod = new_alarmPeriod.ToString();
                }
                timerDef.timer_remainingTime = timerDef.assigned_alarmPeriod - DateTime.Now;
                timerDef.enabled = true;
                ClockLib.Update(true);
                ClockLib.StartOrStop_FastTimerAndDisplayTimer();
            }
        }

        // Token: 0x0600001F RID: 31 RVA: 0x000028AC File Offset: 0x00000AAC
        public static AlarmItemDef AddTimer(string timerTitle, string timeSpan, string _alarmSound = "default", List<ClockCommand> clockCommands = null)
        {
            bool flag = ClockLib.timerDefs.Count >= ClockLib.max_timerDefs;
            AlarmItemDef result;
            if (flag)
            {
                Debug.WriteLine("Max timer items: " + ClockLib.max_timerDefs.ToString() + ", has been reached");
                result = null;
            }
            else
            {
                int assigningIndex = -1;
                for (int i = 0; i < ClockLib.max_timerDefs; i++)
                {
                    bool flag2 = !ClockLib.timerDefs.ContainsKey(i);
                    if (flag2)
                    {
                        assigningIndex = i;
                        break;
                    }
                }
                bool flag3 = assigningIndex == -1;
                if (flag3)
                {
                    Debug.Fail("Error: ClockLib.AddTimer(): failed to a assign index because max timer item limit has been reached");
                    result = null;
                }
                else
                {
                    AlarmItemDef timerDef = new AlarmItemDef
                    {
                        title = timerTitle,
                        alarmTime = timeSpan,
                        enabled = false,
                        index = assigningIndex,
                        alarmSound = _alarmSound,
                        commands = clockCommands,
                        timer_remainingTime = DateTime.Now.Add(TimeSpan.Parse(timeSpan)) - DateTime.Now
                    };
                    ClockLib.timerDefs.Add(assigningIndex, timerDef);
                    result = timerDef;
                }
            }
            return result;
        }

        // Token: 0x06000020 RID: 32 RVA: 0x000029B2 File Offset: 0x00000BB2
        private static void SnoozeUpdateTimer_Tick(object sender, EventArgs e)
        {
            ClockLib.Update(true);
            ClockLib.snoozeUpdateTimer.Stop();
        }

        // Token: 0x06000021 RID: 33 RVA: 0x000029C8 File Offset: 0x00000BC8
        private static void ToastNotificationManagerCompat_OnActivated(ToastNotificationActivatedEventArgsCompat e)
        {
            string[] args = e.Argument.Split(';', StringSplitOptions.None);
            string text = args[0];
            string a = text;
            if (!(a == "Alarm:Snooze"))
            {
                if (!(a == "Alarm:Dismiss"))
                {
                    ClockLib.stopSoundTimer.Start();
                }
                else
                {
                    ClockLib.stopSoundTimer.Start();
                }
            }
            else
            {
                int alarm_index = int.Parse(args[1]);
                AlarmItemDef alarmDef = null;
                bool flag = ClockLib.alarmDefs.TryGetValue(alarm_index, out alarmDef);
                if (flag)
                {
                    object cb_input = "";
                    bool flag2 = e.UserInput.TryGetValue("cb", out cb_input);
                    if (flag2)
                    {
                        string item_addedDate = args[2];
                        bool flag3 = alarmDef.itemAddedDate.ToString() == item_addedDate;
                        if (flag3)
                        {
                            DateTime snooze_time = DateTime.Now;
                            string str_cb_input = cb_input as string;
                            string text2 = str_cb_input;
                            string a2 = text2;
                            if (!(a2 == "5Min"))
                            {
                                if (!(a2 == "10Min"))
                                {
                                    if (!(a2 == "20Min"))
                                    {
                                        if (!(a2 == "30Min"))
                                        {
                                            if (a2 == "1Hour")
                                            {
                                                snooze_time = snooze_time.AddHours(1.0);
                                            }
                                        }
                                        else
                                        {
                                            snooze_time = snooze_time.AddMinutes(30.0);
                                        }
                                    }
                                    else
                                    {
                                        snooze_time = snooze_time.AddMinutes(20.0);
                                    }
                                }
                                else
                                {
                                    snooze_time = snooze_time.AddMinutes(10.0);
                                }
                            }
                            else
                            {
                                snooze_time = snooze_time.AddMinutes(5.0);
                            }
                            alarmDef.snoozed = true;
                            alarmDef.str_current_alarmPeriod = snooze_time.ToString();
                            alarmDef.assigned_alarmPeriod = snooze_time;
                            alarmDef.enabled = true;
                            ClockLib.snoozeUpdateTimer.Start();
                            ClockLib.stopSoundTimer.Start();
                        }
                        else
                        {
                            Debug.Fail("Warning: the alarm you are trying to delay has been removed");
                        }
                    }
                }
                else
                {
                    Debug.Fail("Warning: the alarm you are trying to delay has been removed");
                }
                ClockLib.stopSoundTimer.Start();
            }
        }

        // Token: 0x06000022 RID: 34 RVA: 0x00002BBC File Offset: 0x00000DBC
        public static void ShowNotification(this ClockNotification clockNotification)
        {
            ClockNotificationType notificationType = clockNotification.ntfType;
            ToastContentBuilder builder = new ToastContentBuilder();
            ClockNotificationType clockNotificationType = notificationType;
            ClockNotificationType clockNotificationType2 = clockNotificationType;
            if (clockNotificationType2 != ClockNotificationType.Alarm)
            {
                if (clockNotificationType2 != ClockNotificationType.Timer)
                {
                    MessageBox.Show("line 1120 public static void ShowNotification(this ClockNotification clockNotification): its default?? this shouldn't happen");
                }
                else
                {
                    AlarmItemDef timerDef = clockNotification.data as AlarmItemDef;
                    builder.AddText("Timer (" + timerDef.title + ")", null, null, null, null, null, null);
                    string str_txt = string.Format("{0} timer, {1}", timerDef.alarmTime, clockNotification.alarmedTime.ToString("hh:mm tt"));
                    builder.AddText(str_txt, new AdaptiveTextStyle?(AdaptiveTextStyle.Body), null, null, null, null, null);
                    ClockLib.PlaySound(timerDef.alarmSound);
                }
            }
            else
            {
                AlarmItemDef alarmDef = clockNotification.data as AlarmItemDef;
                builder.AddText("Alarm (" + alarmDef.title + ")", null, null, null, null, null, null);
                string str_alarmTime = "";
                bool everyMonthOrYear = false;
                bool flag = alarmDef.dateType == "everyMonth";
                if (flag)
                {
                    str_alarmTime = "new month, " + DateTime.Now.ToString("MMMM");
                    everyMonthOrYear = true;
                }
                else
                {
                    bool flag2 = alarmDef.dateType == "everyYear";
                    if (flag2)
                    {
                        str_alarmTime = "new year ^_^, " + DateTime.Now.ToString("yyyy");
                        everyMonthOrYear = true;
                    }
                }
                bool flag3 = !everyMonthOrYear;
                if (flag3)
                {
                    str_alarmTime = clockNotification.alarmedTime.ToString("hh:mm:ss tt");
                }
                else
                {
                    str_alarmTime = str_alarmTime + " " + clockNotification.alarmedTime.ToString("hh:mm:ss tt");
                }
                builder.AddText(str_alarmTime, new AdaptiveTextStyle?(AdaptiveTextStyle.Body), null, null, null, null, null);
                bool flag4 = alarmDef.dateType == "default" || alarmDef.dateType == "specificDate";
                if (flag4)
                {
                    builder.AddText("Snooze for", new AdaptiveTextStyle?(AdaptiveTextStyle.Default), null, null, null, null, null);
                    builder.AddComboBox("cb", "5Min", new ValueTuple<string, string>[]
                    {
                        new ValueTuple<string, string>("5Min", "5 Minutes"),
                        new ValueTuple<string, string>("10Min", "10 Minutes"),
                        new ValueTuple<string, string>("20Min", "20 Minutes"),
                        new ValueTuple<string, string>("30Min", "30 Minutes"),
                        new ValueTuple<string, string>("1Hour", "1 Hour")
                    });
                    builder.AddButton(new ToastButton().SetContent("Snooze").AddArgument("Alarm:Snooze"));
                    builder.AddButton(new ToastButton().SetContent("Dismiss").AddArgument("Alarm:Dismiss"));
                }
                string str_alarm_index = alarmDef.index.ToString();
                builder.AddArgument(str_alarm_index);
                builder.AddArgument(alarmDef.itemAddedDate.ToString());
                builder.AddArgument(clockNotification.alarmedTime.ToString());
                ClockLib.PlaySound(alarmDef.alarmSound);
            }

            ToastNotification toastNotification = new ToastNotification(builder.GetXml());
            toastNotification.ExpirationTime = new DateTimeOffset?(new DateTimeOffset(DateTime.Now.AddSeconds((double)ClockLib.setting_toastNotificationExpirePeriodInSeconds)));
            toastNotification.Dismissed += ClockLib.ToastNotification_Dismissed;
            ToastNotifierCompat notifierCompat = ToastNotificationManagerCompat.CreateToastNotifier();
            notifierCompat.Show(toastNotification);
        }

        // Token: 0x06000023 RID: 35 RVA: 0x00002FF5 File Offset: 0x000011F5
        private static void ToastNotification_Dismissed(ToastNotification sender, ToastDismissedEventArgs args)
        {
            ClockLib.stopSoundTimer.Start();
        }

        // Token: 0x06000024 RID: 36 RVA: 0x00003004 File Offset: 0x00001204
        public static void Load_AlarmSounds()
        {
            ClockLib.alarmSounds.Clear();
            string p_data = ClockLib.executablePath + "\\Data";
            string p_sounds = p_data + "\\Sounds";
            bool flag = !Directory.Exists(p_data);
            if (flag)
            {
                Directory.CreateDirectory(p_data);
            }
            bool flag2 = !Directory.Exists(p_sounds);
            if (flag2)
            {
                Directory.CreateDirectory(p_sounds);
            }
            string[] files = Directory.GetFiles(p_sounds);
            foreach (string _file in files)
            {
                string ext = Path.GetExtension(_file);
                bool flag3 = ClockLib.sound_exts.Contains(ext);
                if (flag3)
                {
                    string sound_fileName = Path.GetFileNameWithoutExtension(_file);
                    ClockLib.alarmSounds.Add(new ClockAlarmSoundDef
                    {
                        soundName = sound_fileName,
                        soundPath = _file
                    });
                    Debug.WriteLine("Clock Alarm Sound '" + sound_fileName + "' loaded");
                }
            }
        }

        // Token: 0x06000025 RID: 37 RVA: 0x000030EC File Offset: 0x000012EC
        public static void PlaySound(string soundName = "default")
        {
            ClockAlarmSoundDef soundDef = ClockLib.alarmSounds.Find((ClockAlarmSoundDef s) => s.soundName == soundName);
            bool flag = soundDef == null;
            if (flag)
            {
                throw new Exception("Error: ClockLib.PlaySound(): failed to find sound '" + soundName + "'");
            }
            ClockLib.soundPlayerLooping = true;
            ClockLib.soundPlayer2 = new MediaPlayer();
            string soundPath = soundDef.soundPath;
            ClockLib.soundPlayer2_soundPath = soundPath;
            ClockLib.soundPlayer2.Open(new Uri(soundPath));
            WaveFileReader waveFileReader = new WaveFileReader(soundPath);
            ClockLib.soundPlayer2Looper.Interval = waveFileReader.TotalTime;
            ClockLib.soundPlayer2.Volume = ClockLib.soundVolume;
            ClockLib.soundPlayer2.Play();
            ClockLib.soundPlayer2Looper.Start();
        }

        // Token: 0x06000026 RID: 38 RVA: 0x000031B0 File Offset: 0x000013B0
        public static void StopSound()
        {
            bool flag = ClockLib.soundPlayer != null;
            if (flag)
            {
                ClockLib.soundPlayer.Stop();
                ClockLib.soundPlayer.Dispose();
            }
            ClockLib.soundPlayer2.Stop();
            ClockLib.soundPlayerLooping = false;
            ClockLib.soundPlayer2Looper.Stop();
        }

        // Token: 0x06000027 RID: 39 RVA: 0x00003200 File Offset: 0x00001400
        public static void Update(bool updateUI = true)
        {
            ClockLib.addingItems = true;
            ClockLib.timerDefTimer_disable = true;
            ClockLib.clockItems.Clear();
            DateTime now = DateTime.Now;
            DateTime today = DateTime.Today;
            string dayOfWeek = now.DayOfWeek.ToString();
            string str_currentDayIndex = dayOfWeek.DayToIndex().ToString();
            foreach (KeyValuePair<int, AlarmItemDef> _keyValue in ClockLib.alarmDefs)
            {
                int _index = _keyValue.Key;
                AlarmItemDef alarmDef = _keyValue.Value;
                bool enabled = alarmDef.enabled;
                if (enabled)
                {
                    string days = alarmDef.dateValue;
                    string str_current_alarmPeriod = alarmDef.str_current_alarmPeriod;
                    string dateType = alarmDef.dateType;
                    string a = dateType;
                    if (!(a == "default"))
                    {
                        if (!(a == "specificDate"))
                        {
                            if (!(a == "everyMonth"))
                            {
                                if (!(a == "everyYear"))
                                {
                                    if (a == "specificDates")
                                    {
                                        bool flag = str_current_alarmPeriod == "";
                                        if (flag)
                                        {
                                            string alarmTime = alarmDef.alarmTime;
                                            List<DateTime> assignedDates = new List<DateTime>();
                                            string[] str_assignedDates = alarmDef.dateValue.Split('@', StringSplitOptions.None);
                                            foreach (string _str_date in str_assignedDates)
                                            {
                                                assignedDates.Add(DateTime.Parse(_str_date));
                                            }
                                            assignedDates.Sort((DateTime x, DateTime y) => DateTime.Compare(x, y));
                                            int dateCount = assignedDates.Count;
                                            int datesReached = 0;
                                            DateTime selectedDate = new DateTime(1, 1, 1);
                                            foreach (DateTime _alarmDate in assignedDates)
                                            {
                                                bool flag2 = today >= _alarmDate;
                                                if (!flag2)
                                                {
                                                    selectedDate = _alarmDate;
                                                    break;
                                                }
                                                datesReached++;
                                            }
                                            bool flag3 = datesReached != dateCount;
                                            if (flag3)
                                            {
                                                string year = selectedDate.Year.ToString();
                                                string month = selectedDate.Month.ToString();
                                                string day = selectedDate.Day.ToString();
                                                string str_alarmPeriod = string.Concat(new string[]
                                                {
                                                    year,
                                                    "/",
                                                    month,
                                                    "/",
                                                    day,
                                                    " ",
                                                    alarmTime
                                                });
                                                alarmDef.str_current_alarmPeriod = str_alarmPeriod;
                                                alarmDef.assigned_alarmPeriod = DateTime.Parse(str_alarmPeriod);
                                            }
                                        }
                                        ClockItem clockItem5 = new ClockItem
                                        {
                                            index = _index,
                                            alarmPeriod = alarmDef.assigned_alarmPeriod,
                                            itemType = ClockItemType.Alarm
                                        };
                                        ClockLib.clockItems.Add(clockItem5);
                                    }
                                }
                                else
                                {
                                    bool flag4 = str_current_alarmPeriod == "";
                                    if (flag4)
                                    {
                                        DateTime newYear = new DateTime(new DateTime(today.Year, 1, 1).AddYears(1).Year, 1, 1);
                                        string str_alarmPeriod2 = newYear.Year.ToString() + "/1/1 " + alarmDef.alarmTime;
                                        alarmDef.str_current_alarmPeriod = str_alarmPeriod2;
                                        alarmDef.assigned_alarmPeriod = DateTime.Parse(str_alarmPeriod2);
                                    }
                                    ClockItem clockItem6 = new ClockItem
                                    {
                                        index = _index,
                                        alarmPeriod = alarmDef.assigned_alarmPeriod,
                                        itemType = ClockItemType.Alarm
                                    };
                                    ClockLib.clockItems.Add(clockItem6);
                                }
                            }
                            else
                            {
                                bool flag5 = str_current_alarmPeriod == "";
                                if (flag5)
                                {
                                    DateTime monthAdded = new DateTime(today.Year, today.Month, 1).AddMonths(1);
                                    DateTime newMonth = new DateTime(monthAdded.Year, monthAdded.Month, 1);
                                    string str_alarmPeriod3 = string.Concat(new string[]
                                    {
                                        newMonth.Year.ToString(),
                                        "/",
                                        newMonth.Month.ToString(),
                                        "/1 ",
                                        alarmDef.alarmTime
                                    });
                                    alarmDef.str_current_alarmPeriod = str_alarmPeriod3;
                                    alarmDef.assigned_alarmPeriod = DateTime.Parse(str_alarmPeriod3);
                                }
                                ClockItem clockItem7 = new ClockItem
                                {
                                    index = _index,
                                    alarmPeriod = alarmDef.assigned_alarmPeriod,
                                    itemType = ClockItemType.Alarm
                                };
                                ClockLib.clockItems.Add(clockItem7);
                            }
                        }
                        else
                        {
                            bool flag6 = str_current_alarmPeriod == "";
                            if (flag6)
                            {
                                string alarmTime2 = alarmDef.alarmTime;
                                string alarmDate = alarmDef.dateValue;
                                string str_alarmPeriod4 = alarmDate + " " + alarmTime2;
                                alarmDef.str_current_alarmPeriod = str_alarmPeriod4;
                                alarmDef.assigned_alarmPeriod = DateTime.Parse(str_alarmPeriod4);
                            }
                            ClockItem clockItem8 = new ClockItem
                            {
                                index = _index,
                                alarmPeriod = alarmDef.assigned_alarmPeriod,
                                itemType = ClockItemType.Alarm
                            };
                            ClockLib.clockItems.Add(clockItem8);
                        }
                    }
                    else
                    {
                        bool flag7 = str_current_alarmPeriod != "" && days != "";
                        if (flag7)
                        {
                            DateTime alarmPeriod = alarmDef.assigned_alarmPeriod;
                            DateTime today_dateOnly = new DateTime(today.Year, today.Month, today.Day);
                            DateTime alarmPeriod_dateOnly = new DateTime(alarmPeriod.Year, alarmPeriod.Month, alarmPeriod.Day);
                            bool flag8 = !alarmDef.snoozed && today_dateOnly > alarmPeriod_dateOnly;
                            if (flag8)
                            {
                                alarmDef.str_current_alarmPeriod = "";
                                alarmDef.assigned_alarmPeriod = new DateTime(1, 1, 1);
                            }
                            bool flag9 = today_dateOnly == alarmPeriod_dateOnly;
                            if (flag9)
                            {
                                bool flag10 = now > alarmPeriod;
                                if (flag10)
                                {
                                    alarmDef.str_current_alarmPeriod = "";
                                    alarmDef.assigned_alarmPeriod = new DateTime(1, 1, 1);
                                }
                            }
                        }
                        bool flag11 = alarmDef.str_current_alarmPeriod != "";
                        if (!flag11)
                        {
                            DateTime new_alarmPeriod = DateTime.Parse(alarmDef.alarmTime);
                            bool flag12 = now >= new_alarmPeriod;
                            if (flag12)
                            {
                                DateTime tomm = today;
                                bool flag13 = days == "";
                                if (flag13)
                                {
                                    tomm = tomm.AddDays(1.0);
                                }
                                else
                                {
                                    for (int i = 1; i < 8; i++)
                                    {
                                        DateTime looping_dt = tomm.AddDays((double)i);
                                        string str_looping_dayIndex = looping_dt.DayOfWeek.ToString().DayToIndex().ToString();
                                        bool flag14 = days.Contains(str_looping_dayIndex);
                                        if (flag14)
                                        {
                                            tomm = looping_dt;
                                            break;
                                        }
                                    }
                                }
                                string tomm_date = string.Concat(new string[]
                                {
                                    tomm.Year.ToString(),
                                    "/",
                                    tomm.Month.ToString(),
                                    "/",
                                    tomm.Day.ToString()
                                });
                                string new_alarmTime = tomm_date + " " + alarmDef.alarmTime;
                                DateTime tomm_alarmPeriod = DateTime.Parse(new_alarmTime);
                                alarmDef.str_current_alarmPeriod = tomm_alarmPeriod.ToString();
                                alarmDef.assigned_alarmPeriod = tomm_alarmPeriod;
                            }
                            else
                            {
                                bool flag15 = days != "";
                                if (flag15)
                                {
                                    DateTime activeDay = today;
                                    for (int j = 0; j < 8; j++)
                                    {
                                        DateTime looping_dt2 = activeDay.AddDays((double)j);
                                        string str_looping_dayIndex2 = looping_dt2.DayOfWeek.ToString().DayToIndex().ToString();
                                        bool flag16 = days.Contains(str_looping_dayIndex2);
                                        if (flag16)
                                        {
                                            activeDay = looping_dt2;
                                            break;
                                        }
                                    }
                                    string str_full_alarmTime = string.Concat(new string[]
                                    {
                                        activeDay.Year.ToString(),
                                        "/",
                                        activeDay.Month.ToString(),
                                        "/",
                                        activeDay.Day.ToString(),
                                        " ",
                                        alarmDef.alarmTime
                                    });
                                    new_alarmPeriod = DateTime.Parse(str_full_alarmTime);
                                }
                                alarmDef.str_current_alarmPeriod = new_alarmPeriod.ToString();
                                alarmDef.assigned_alarmPeriod = new_alarmPeriod;
                            }
                        }
                        bool flag17 = days == "";
                        if (flag17)
                        {
                            ClockItem clockItem9 = new ClockItem
                            {
                                index = _index,
                                alarmPeriod = alarmDef.assigned_alarmPeriod,
                                itemType = ClockItemType.Alarm
                            };
                            ClockLib.clockItems.Add(clockItem9);
                        }
                        else
                        {
                            bool flag18 = days.Contains(str_currentDayIndex);
                            if (flag18)
                            {
                                ClockItem clockItem10 = new ClockItem
                                {
                                    index = _index,
                                    alarmPeriod = alarmDef.assigned_alarmPeriod,
                                    itemType = ClockItemType.Alarm
                                };
                                ClockLib.clockItems.Add(clockItem10);
                                Debug.WriteLine("New Clock Item Added");
                            }
                        }
                    }
                }
            }
            ClockLib.lastUpdate = DateTime.Now;
            ClockLib.addingItems = false;
            ClockLib.timerDefTimer_disable = false;
            bool flag19 = updateUI && ClockLib.mainController != null;
            if (flag19)
            {
                ClockLib.mainController.UpdateDisplay();
            }
            ClockLib.storage_alarms.DoSave();
        }

        // Token: 0x06000028 RID: 40 RVA: 0x00003B54 File Offset: 0x00001D54
        private static void Timer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime today = DateTime.Today;
            bool flag = today != ClockLib.currentDate;
            if (flag)
            {
                ClockLib.currentDate = today;
                ClockLib.Update(true);
            }
            else
            {
                bool requireDeleting = false;
                foreach (ClockItem _clockItem in ClockLib.clockItems)
                {
                    bool flag2 = now >= _clockItem.alarmPeriod;
                    if (flag2)
                    {
                        _clockItem._delete = true;
                        requireDeleting = true;
                        ClockNotificationType _ntfType = ClockNotificationType.Alarm;
                        object _ntfData = null;
                        bool flag3 = _clockItem.itemType == ClockItemType.Alarm;
                        if (flag3)
                        {
                            AlarmItemDef alarmDef = ClockLib.alarmDefs[_clockItem.index];
                            alarmDef.snoozed = false;
                            alarmDef.str_current_alarmPeriod = "";
                            ClockLib.ExecuteCommands(alarmDef.commands);
                            bool flag4 = alarmDef.dateType == "default" && alarmDef.dateValue == "";
                            if (flag4)
                            {
                                alarmDef.enabled = false;
                            }
                            else
                            {
                                bool flag5 = alarmDef.dateType == "specificDate";
                                if (flag5)
                                {
                                    alarmDef.enabled = false;
                                }
                                else
                                {
                                    bool flag6 = alarmDef.dateType == "specificDates";
                                    if (flag6)
                                    {
                                        List<DateTime> assignedDates = new List<DateTime>();
                                        string[] str_assignedDates = alarmDef.dateValue.Split('@', StringSplitOptions.None);
                                        foreach (string _str_date in str_assignedDates)
                                        {
                                            assignedDates.Add(DateTime.Parse(_str_date));
                                        }
                                        assignedDates.Sort((DateTime x, DateTime y) => DateTime.Compare(x, y));
                                        int dateCount = assignedDates.Count;
                                        int datesReached = 0;
                                        DateTime selectedDate = new DateTime(1, 1, 1);
                                        foreach (DateTime _alarmDate in assignedDates)
                                        {
                                            bool flag7 = today >= _alarmDate;
                                            if (!flag7)
                                            {
                                                selectedDate = _alarmDate;
                                                break;
                                            }
                                            datesReached++;
                                        }
                                        bool flag8 = datesReached == dateCount;
                                        if (flag8)
                                        {
                                            alarmDef.enabled = false;
                                        }
                                    }
                                }
                            }
                            _ntfData = alarmDef;
                            _ntfType = ClockNotificationType.Alarm;
                        }
                        ClockNotification clockNotification = new ClockNotification
                        {
                            alarmedTime = _clockItem.alarmPeriod,
                            data = _ntfData,
                            ntfType = _ntfType
                        };
                        ClockLib.clockNotifications.Add(clockNotification);
                    }
                }
                bool flag9 = requireDeleting;
                if (flag9)
                {
                    ClockLib.clockItems.RemoveAll((ClockItem _item) => _item._delete);
                    ClockLib.Update(true);
                }
                bool flag10 = ClockLib.clockNotifications.Count > 0;
                if (flag10)
                {
                    foreach (ClockNotification _c_ntf in ClockLib.clockNotifications)
                    {
                        _c_ntf.ShowNotification();
                    }
                    ClockLib.clockNotifications.Clear();
                }
            }
        }

        // Token: 0x06000029 RID: 41 RVA: 0x00003EC0 File Offset: 0x000020C0
        public static int AddAlarm(string _title, string _alarmTime, string _dateType = "default", string _dateValue = "", bool _enabled = true, string _current_alarmPeriod = "", string _alarmSound = "default", List<ClockCommand> clockCommands = null)
        {
            bool flag = ClockLib.alarmDefs.Count >= ClockLib.max_alarmDefs;
            int result;
            if (flag)
            {
                Debug.WriteLine("Max alarm items: " + ClockLib.max_alarmDefs.ToString() + ", has been reached");
                result = -1;
            }
            else
            {
                int assigningIndex = -1;
                for (int i = 0; i < ClockLib.max_alarmDefs; i++)
                {
                    bool flag2 = !ClockLib.alarmDefs.ContainsKey(i);
                    if (flag2)
                    {
                        assigningIndex = i;
                        break;
                    }
                }
                bool flag3 = assigningIndex == -1;
                if (flag3)
                {
                    Debug.Fail("Error: ClockLib.AddAlarm: failed to a assign index because max item limit has reached");
                    result = -1;
                }
                else
                {
                    AlarmItemDef item = new AlarmItemDef
                    {
                        index = assigningIndex,
                        title = _title,
                        alarmTime = _alarmTime,
                        dateType = _dateType,
                        dateValue = _dateValue,
                        enabled = _enabled,
                        str_current_alarmPeriod = _current_alarmPeriod,
                        alarmSound = _alarmSound,
                        itemAddedDate = DateTime.Now,
                        commands = clockCommands
                    };
                    ClockLib.alarmDefs.Add(assigningIndex, item);
                    result = assigningIndex;
                }
            }
            return result;
        }

        // Token: 0x0600002A RID: 42 RVA: 0x00003FC8 File Offset: 0x000021C8
        public static string GetRemainingTime(this DateTime endDate, DateTime startDate)
        {
            TimeSpan ts = endDate - startDate;
            int days = ts.Days;
            int hours = ts.Hours;
            int minutes = ts.Minutes;
            int seconds = ts.Seconds;
            bool has_remainingDays = days > 0;
            bool has_remainingHours = hours > 0;
            bool has_remainingMinutes = minutes > 0;
            bool has_remainingSeconds = seconds > 0;
            string str = "";
            bool flag = has_remainingDays;
            if (flag)
            {
                string str_day = "day";
                bool flag2 = days > 1;
                if (flag2)
                {
                    str_day += "s";
                }
                str = str + days.ToString() + " " + str_day;
                bool flag3 = has_remainingHours || has_remainingMinutes || has_remainingSeconds;
                if (flag3)
                {
                    str += ",";
                }
            }
            bool flag4 = has_remainingHours;
            if (flag4)
            {
                string str_hour = "hour";
                bool flag5 = hours > 1;
                if (flag5)
                {
                    str_hour += "s";
                }
                bool flag6 = has_remainingDays;
                if (flag6)
                {
                    str += " ";
                }
                str = str + hours.ToString() + " " + str_hour;
                bool flag7 = has_remainingMinutes || has_remainingSeconds;
                if (flag7)
                {
                    str += ",";
                }
            }
            bool flag8 = has_remainingMinutes;
            if (flag8)
            {
                string str_minute = "minute";
                bool flag9 = minutes > 1;
                if (flag9)
                {
                    str_minute += "s";
                }
                bool flag10 = has_remainingDays || has_remainingHours;
                if (flag10)
                {
                    str += " ";
                }
                str = str + minutes.ToString() + " " + str_minute;
                bool flag11 = has_remainingSeconds;
                if (flag11)
                {
                    str += ",";
                }
            }
            bool flag12 = has_remainingSeconds;
            if (flag12)
            {
                string str_second = "second";
                bool flag13 = seconds > 1;
                if (flag13)
                {
                    str_second += "s";
                }
                bool flag14 = has_remainingDays || has_remainingHours || has_remainingMinutes;
                if (flag14)
                {
                    str += " ";
                }
                str = str + seconds.ToString() + " " + str_second;
            }
            return str;
        }

        // Token: 0x0600002B RID: 43 RVA: 0x000041D4 File Offset: 0x000023D4
        public static int DayToIndex(this string day)
        {
            if (day != null)
            {
                switch (day.Length)
                {
                    case 6:
                        {
                            char c = day[0];
                            if (c != 'F')
                            {
                                if (c != 'M')
                                {
                                    if (c == 'S')
                                    {
                                        if (day == "Sunday")
                                        {
                                            return 0;
                                        }
                                    }
                                }
                                else if (day == "Monday")
                                {
                                    return 1;
                                }
                            }
                            else if (day == "Friday")
                            {
                                return 5;
                            }
                            break;
                        }
                    case 7:
                        if (day == "Tuesday")
                        {
                            return 2;
                        }
                        break;
                    case 8:
                        {
                            char c = day[0];
                            if (c != 'S')
                            {
                                if (c == 'T')
                                {
                                    if (day == "Thursday")
                                    {
                                        return 4;
                                    }
                                }
                            }
                            else if (day == "Saturday")
                            {
                                return 6;
                            }
                            break;
                        }
                    case 9:
                        if (day == "Wednesday")
                        {
                            return 3;
                        }
                        break;
                }
            }
            return -1;
        }

        // Token: 0x0600002C RID: 44 RVA: 0x000042DC File Offset: 0x000024DC
        public static string IndexToDay(this int dayIndex)
        {
            string result;
            switch (dayIndex)
            {
                case 0:
                    result = "Sunday";
                    break;
                case 1:
                    result = "Monday";
                    break;
                case 2:
                    result = "Tuesday";
                    break;
                case 3:
                    result = "Wednesday";
                    break;
                case 4:
                    result = "Thursda%y";
                    break;
                case 5:
                    result = "Friday";
                    break;
                case 6:
                    result = "Saturday";
                    break;
                default:
                    result = "";
                    break;
            }
            return result;
        }

        // Token: 0x0600002D RID: 45
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern ClockLib.EXECUTION_STATE SetThreadExecutionState(ClockLib.EXECUTION_STATE esFlags);

        // Token: 0x0600002E RID: 46 RVA: 0x00004353 File Offset: 0x00002553
        public static void PreventSleep()
        {
            ClockLib.SetThreadExecutionState((ClockLib.EXECUTION_STATE)2147483649U);
        }

        // Token: 0x0600002F RID: 47 RVA: 0x00004361 File Offset: 0x00002561
        public static void AllowSleep()
        {
            ClockLib.SetThreadExecutionState((ClockLib.EXECUTION_STATE)2147483648U);
        }

        // Token: 0x06000030 RID: 48 RVA: 0x00004370 File Offset: 0x00002570
        public static void ExecuteCommands(List<ClockCommand> commands)
        {
            foreach (ClockCommand _command in commands)
            {
                switch (_command.type)
                {
                    case ClockCommandType.Open_Application_Or_Directory:
                        {
                            string path = _command.args[0];
                            Process.Start(new ProcessStartInfo("cmd.exe", string.Format("/c start {0}{0} {0}{1}{0} & exit", '"', path))
                            {
                                WindowStyle = ProcessWindowStyle.Hidden
                            });
                            break;
                        }
                    case ClockCommandType.Close_Application:
                        {
                            string processName = _command.args[0];
                            Process[] processes = Process.GetProcessesByName(processName);
                            foreach (Process _process in processes)
                            {
                                _process.Kill();
                            }
                            break;
                        }
                    case ClockCommandType.Open_URL:
                        {
                            string url = _command.args[0];
                            Process.Start(url);
                            break;
                        }
                    case ClockCommandType.Turn_On_ComputerSleepMode:
                        ClockLib.AllowSleep();
                        break;
                    case ClockCommandType.Turn_Off_ComputerSleepMode:
                        ClockLib.PreventSleep();
                        break;
                    case ClockCommandType.Shutdown_Computer:
                        Process.Start("shutdown", "/s /t 0");
                        break;
                    case ClockCommandType.Restart_Computer:
                        Process.Start("shutdown", "/r /t 0");
                        break;
                }
            }
        }

        // Token: 0x04000003 RID: 3
        public static Dictionary<int, AlarmItemDef> alarmDefs = new Dictionary<int, AlarmItemDef>();

        // Token: 0x04000004 RID: 4
        public static int max_alarmDefs = 1000;

        // Token: 0x04000005 RID: 5
        public static Dictionary<int, AlarmItemDef> timerDefs = new Dictionary<int, AlarmItemDef>();

        // Token: 0x04000006 RID: 6
        public static int max_timerDefs = 1000;

        // Token: 0x04000007 RID: 7
        public static DateTime stopWatchPlayPeriod;

        // Token: 0x04000008 RID: 8
        public static TimeSpan stopWatchTime = TimeSpan.Zero;

        // Token: 0x04000009 RID: 9
        public static bool stopWatchEnabled = false;

        // Token: 0x0400000A RID: 10
        public static List<ClockItem> clockItems = new List<ClockItem>();

        // Token: 0x0400000B RID: 11
        public static DispatcherTimer timer = new DispatcherTimer();

        // Token: 0x0400000C RID: 12
        public static DispatcherTimer snoozeUpdateTimer = new DispatcherTimer();

        // Token: 0x0400000D RID: 13
        public static DispatcherTimer displayTimer = new DispatcherTimer();

        // Token: 0x0400000E RID: 14
        public static DispatcherTimer timerDefTimer = new DispatcherTimer();

        // Token: 0x0400000F RID: 15
        public static DispatcherTimer stopSoundTimer = new DispatcherTimer();

        // Token: 0x04000010 RID: 16
        private static bool timerDefTimer_disable = false;

        // Token: 0x04000011 RID: 17
        public static List<ClockNotification> clockNotifications = new List<ClockNotification>();

        // Token: 0x04000012 RID: 18
        public static List<ClockAlarmSoundDef> alarmSounds = new List<ClockAlarmSoundDef>();

        // Token: 0x04000013 RID: 19
        public static SoundPlayer soundPlayer;

        // Token: 0x04000014 RID: 20
        public static string soundPlayer2_soundPath = "";

        // Token: 0x04000015 RID: 21
        public static MediaPlayer soundPlayer2 = new MediaPlayer();

        // Token: 0x04000016 RID: 22
        public static double soundVolume = 0.5f;

        // Token: 0x04000017 RID: 23
        public static DispatcherTimer soundPlayer2Looper = new DispatcherTimer();

        // Token: 0x04000018 RID: 24
        public static bool soundPlayerLooping = false;

        // Token: 0x04000019 RID: 25
        public static string[] sound_exts = new string[]
        {
            ".wav"
        };

        // Token: 0x0400001A RID: 26
        public static int setting_toastNotificationExpirePeriodInSeconds = 300;

        // Token: 0x0400001B RID: 27
        public static string executablePath = "";

        // Token: 0x0400001C RID: 28
        public static DateTime lastUpdate = DateTime.Now;

        // Token: 0x0400001D RID: 29
        public static DateTime currentDate = DateTime.Today;

        // Token: 0x0400001E RID: 30
        public static XMLStorageAlarms storage_alarms;

        // Token: 0x0400001F RID: 31
        public static string f_alarms;

        // Token: 0x04000020 RID: 32
        public static ClockControllerDef mainController;

        // Token: 0x04000021 RID: 33
        public static Label label_stopWatchTime;

        // Token: 0x04000022 RID: 34
        public static List<ClockCommandType> commandTypes = new List<ClockCommandType>
        {
            ClockCommandType.Open_Application_Or_Directory,
            ClockCommandType.Close_Application,
            ClockCommandType.Open_URL,
            ClockCommandType.Turn_On_ComputerSleepMode,
            ClockCommandType.Turn_Off_ComputerSleepMode,
            ClockCommandType.Shutdown_Computer,
            ClockCommandType.Restart_Computer
        };

        // Token: 0x04000023 RID: 35
        private static bool addingItems = false;

        // Token: 0x04000024 RID: 36
        public static ToastNotification toastNtf;

        // Token: 0x04000025 RID: 37
        public static AlarmItemDef current_sel_timerDef;

        // Token: 0x02000012 RID: 18
    
        [Flags]
        public enum EXECUTION_STATE : uint
        {
            // Token: 0x04000054 RID: 84
            ES_CONTINUOUS = 2147483648U,
            // Token: 0x04000055 RID: 85
            ES_SYSTEM_REQUIRED = 1U,
            // Token: 0x04000056 RID: 86
            ES_DISPLAY_REQUIRED = 2U
        }
    }


    /// <summary>
    /// when using AddAlarm to define an alarm item they can be enabled or disabled, but if it's enabled then calling the ClockLib.Update() method would
    /// create this ClockItem with an alarm period, ClockItems are only used for defined alarms
    /// </summary>
    public class ClockItem
    {
        // Token: 0x04000026 RID: 38
        public int index;

        // Token: 0x04000027 RID: 39
        public ClockItemType itemType;

        // Token: 0x04000028 RID: 40
        public DateTime alarmPeriod;

        // Token: 0x04000029 RID: 41
        public bool _delete = false;
    }

    /// <summary>
    /// clock alarm sound def includes the sound's name(without extension) and sound's full path (including both path, fileName and extension)
    /// </summary>1q
    /// </summary>
    public class ClockNotification
    {
        // Token: 0x0400002C RID: 44
        public object data;

        // Token: 0x0400002D RID: 45
        public DateTime alarmedTime;

        // Token: 0x0400002E RID: 46
        public ClockNotificationType ntfType = ClockNotificationType.Alarm;
    }

    // Token: 0x0200000A RID: 10
    public enum ClockNotificationType
    {
        // Token: 0x04000030 RID: 48
        Alarm,
        // Token: 0x04000031 RID: 49
        Timer
    }

    /// <summary>
    /// clock items are only Alarm, so there is only 'Alarm'
    /// </summary>

    public enum ClockItemType
    {
        Alarm
    }


    public class AlarmItemDef
    {
        // Token: 0x04000034 RID: 52
        public int index = -1;

        // Token: 0x04000035 RID: 53
        public string title = "";

        // Token: 0x04000036 RID: 54
        public string alarmTime = "";

        // Token: 0x04000037 RID: 55
        public string dateType = "";

        // Token: 0x04000038 RID: 56
        public string dateValue = "";

        // Token: 0x04000039 RID: 57
        public bool enabled = true;

        // Token: 0x0400003A RID: 58
        public string str_current_alarmPeriod = "";

        // Token: 0x0400003B RID: 59
        public DateTime assigned_alarmPeriod = new DateTime(1, 1, 1);

        // Token: 0x0400003C RID: 60
        public string alarmSound;

        // Token: 0x0400003D RID: 61
        public DateTime itemAddedDate;

        // Token: 0x0400003E RID: 62
        public DateTime timer_playPeriod = new DateTime(1, 1, 1);

        // Token: 0x0400003F RID: 63
        public TimeSpan timer_remainingTime;

        // Token: 0x04000040 RID: 64
        public List<ClockCommand> commands = new List<ClockCommand>();

        // Token: 0x04000041 RID: 65
        public bool snoozed = false;
    }

    /// <summary>
    /// Clock Command Class to execute a command on alarm
    /// </summary>
    public class ClockCommand
    {
        public ClockCommandType type;
        public List<string> args = new List<string>();
    }

    /// <summary>
    /// Clock Command Type
    /// </summary>
    public enum ClockCommandType
    {
        Open_Application_Or_Directory,
        Close_Application,
        Open_URL,
        Execute_CS_File,
        Turn_On_ComputerSleepMode,
        Turn_Off_ComputerSleepMode,
        Shutdown_Computer,
        Restart_Computer,
        Unknown
    }

    public class ClockAlarmSoundDef
    {
        // Token: 0x0400002A RID: 42
        public string soundName;

        // Token: 0x0400002B RID: 43
        public string soundPath;
    }
}


