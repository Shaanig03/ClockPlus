using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ClockLib;
using MaterialDesignColors;
using MaterialDesignThemes;
using Microsoft.Win32;

namespace ClockPlus
{
    /// <summary>
    /// the class used like an interface between ClockLib and ClockPlus Application
    /// </summary>
    public class ClockLibController: ClockControllerDef
    {
        public WrapPanel alarm_container;
        public WrapPanel timer_container;
        public MainWindow mainWindow;
        
        // updates display
        public override void UpdateDisplay()
        {
            // clear container items
            alarm_container.Children.Clear();


            // loop through each alarmDef
            for (int i = 0; i < ClockLib.ClockLib.max_alarmDefs; i++)
            {
                AlarmItemDef alarmDef;

                // get alarmDef
                if (ClockLib.ClockLib.alarmDefs.TryGetValue(i, out alarmDef))
                {
                    // create item control && store alarm index inside tag
                    AlarmItemControl itemControl = new AlarmItemControl();

                    itemControl.Tag = i;
                    // alarm enable/disable
                    itemControl.ts_enabled.Checked += (sender, e) => Ts_enabled_Checked(sender, e, itemControl);
                    itemControl.ts_enabled.Unchecked += (sender, e) => Ts_enabled_UnChecked(sender, e, itemControl);

                    // set title
                    itemControl.l_title.Content = alarmDef.title;

                    // set alarm time
                    string ampm = "AM";
                    if(alarmDef.alarmTime.Contains("pm") || alarmDef.alarmTime.Contains("PM")) { ampm = "PM"; }
                    itemControl.l_alarmTime.Content = alarmDef.alarmTime.Replace("am", "").Replace("AM", "").Replace("pm", "").Replace("PM", "");
                    itemControl.l_ampm.Content = ampm;

                    // set enabled/disabled
                    itemControl.ts_enabled.IsChecked = alarmDef.enabled;

                    itemControl.delete_btn.Checked += (sender, e) => Delete_btn_Checked(sender, e, itemControl);

                    // update item control (the delete mode thing)
                    itemControl.Update();

                    // add tags
                    switch (alarmDef.dateType)
                    {
                        case "default":

                            foreach(char _dayIndex in alarmDef.dateValue)
                            {

                                string tagName = "";

                                switch (_dayIndex)
                                {
                                    case '0':
                                        tagName = "Su";
                                        break;
                                    case '1':
                                        tagName = "M";
                                        break;
                                    case '2':
                                        tagName = "Tu";
                                        break;
                                    case '3':
                                        tagName = "We";
                                        break;
                                    case '4':
                                        tagName = "Th";
                                        break;
                                    case '5':
                                        tagName = "Fri";
                                        break;
                                    case '6':
                                        tagName = "Sa";
                                        break;
                                    default:
                                        tagName = "";
                                        break;
                                }

                                TagControl tagControl = new TagControl();
                                tagControl.l_tagName.Content = tagName;
                                itemControl.tag_container.Children.Add(tagControl);
                            }
                  
                            break;
                        case "specificDate":
                            TagControl tagControl2 = new TagControl();
                            tagControl2.Width = 59;
                            tagControl2.l_tagName.Content = alarmDef.dateValue;
                            itemControl.tag_container.Children.Add(tagControl2);
                            break;
                        case "specificDates":

                            // get the dates which this alarm would be active
                            List<DateTime> assignedDates = new List<DateTime>();
                            string[] str_assignedDates = alarmDef.dateValue.Split('@'); foreach (string _str_date in str_assignedDates) { assignedDates.Add(DateTime.Parse(_str_date)); }
                            assignedDates.Sort((x, y) => DateTime.Compare(x, y));

                            // date count
                            int dateCount = assignedDates.Count;


                            // next date for alarm
                            DateTime selectedDate = new DateTime(1, 1, 1);

                            DateTime today = DateTime.Today;
                            // loop through each assigned date to select the next date for alarm
                            foreach (DateTime _alarmDate in assignedDates)
                            {
                                if (today >= _alarmDate)
                                {
                                    // empty
                                }
                                else
                                {
                                    TagControl tagControl3 = new TagControl();
                                    tagControl3.Width = 59;
                                    tagControl3.l_tagName.Content = _alarmDate.Month + "/" + _alarmDate.Day + "/" + _alarmDate.Year;
                                    itemControl.tag_container.Children.Add(tagControl3);
                                }
                            }
                            break;
                        case "everyMonth":
                            TagControl tagControl4 = new TagControl();
                            tagControl4.Width = 72;
                            tagControl4.l_tagName.Content = "everyMonth";
                            itemControl.tag_container.Children.Add(tagControl4);
                            break;
                        case "everyYear":
                            TagControl tagControl5 = new TagControl();
                            tagControl5.Width = 65;
                            tagControl5.l_tagName.Content = "everyYear";
                            itemControl.tag_container.Children.Add(tagControl5);
                            break;
                        default:
                            // 'default', 'specificDate', 'specificDates', 'everyMonth', 'everyYear'
                            break;
                    }

                    // add item control
                    alarm_container.Children.Add(itemControl);
                }
            }

            // update timers
            timer_container.Children.Clear();

            for (int ii = 0; ii < ClockLib.ClockLib.max_timerDefs; ii++)
            {
                AlarmItemDef timerDef;

                if(ClockLib.ClockLib.timerDefs.TryGetValue(ii, out timerDef))
                {
                    // create ui control
                    TimerItemControl timerItemControl = new TimerItemControl();

                    // store timer index in tag
                    timerItemControl.Tag = ii;

                    
                    // set labels
                    timerItemControl.l_title.Content = timerDef.title;

                    // buttons
                    timerItemControl.btn_play.Click += (sender, e) => TimerBtn_Play(sender, e, timerItemControl);
                    timerItemControl.btn_reset.Click += (sender, e) => TimerBtn_Reset(sender, e, timerItemControl);


                    // update control
                    timerItemControl.Update();

                    timerItemControl.delete_btn.Checked += (sender, e) => DeleteTimer_btn_Checked(sender, e, timerItemControl);


                    // add ui control to container
                    timer_container.Children.Add(timerItemControl);
                }
            }
            
        }

        public override void UpdateDisplayTime()
        {
            if (mainWindow.WindowState == WindowState.Minimized) { return; }

            if (mainWindow.IsVisible && timer_container.IsVisible)
            {
                foreach (object _ctrl in timer_container.Children)
                {
                    TimerItemControl itemControl = _ctrl as TimerItemControl;
                    itemControl.UpdateRemainingTime();
                }
            }
        }

        private void TimerBtn_Play(object sender, RoutedEventArgs e, TimerItemControl timerItemControl)
        {
            int timerIndex = (int)timerItemControl.Tag;

       

            ClockLib.ClockLib.StartOrStopTimer(ClockLib.ClockLib.timerDefs[timerIndex]);
            timerItemControl.UpdateIcon();
        }
        private void TimerBtn_Reset(object sender, RoutedEventArgs e, TimerItemControl timerItemControl)
        {
            int timerIndex = (int)timerItemControl.Tag;

            ClockLib.ClockLib.ResetTimer(ClockLib.ClockLib.timerDefs[timerIndex]);
        }

        // delete timer button
        private void DeleteTimer_btn_Checked(object sender, RoutedEventArgs e, TimerItemControl itemControl)
        {
            // get alarm index
            int index = (int)itemControl.Tag;

            Defines.controller.RemoveTimer(index);
        }

        // delete alarm button
        private void Delete_btn_Checked(object sender, RoutedEventArgs e, AlarmItemControl itemControl)
        {
            // get alarm index
            int index = (int)itemControl.Tag;

            Defines.controller.RemoveAlarm(index);
        }

        // enables an alarm by toggle switch
        private void Ts_enabled_Checked(object sender, RoutedEventArgs e, AlarmItemControl itemControl)
        {
            if (Defines.deleteModeEnabled) { return; }
            // get alarm index
            int index = (int)itemControl.Tag;

            ToggleButton toggleButton = sender as ToggleButton;
            ClockLib.ClockLib.alarmDefs[index].enabled = (bool)toggleButton.IsChecked;
            ClockLib.ClockLib.Update(false);
        }

        // disables an alarm by toggle switch
        private void Ts_enabled_UnChecked(object sender, RoutedEventArgs e, AlarmItemControl itemControl)
        {
            if (Defines.deleteModeEnabled) { return; }

            // get alarm index
            int index = (int)itemControl.Tag;

            ToggleButton toggleButton = sender as ToggleButton;
            ClockLib.ClockLib.alarmDefs[index].enabled = (bool)toggleButton.IsChecked;
            ClockLib.ClockLib.Update(false);
        }
    }


    public static class Defines
    {
        public static ClockLibController controller = new ClockLibController();
        public static bool deleteModeEnabled = false;

        public static float ConvertRange(float originalStart, float originalEnd, float newStart, float newEnd, float value)
        {
            double scale = (float)(newEnd - newStart) / (originalEnd - originalStart);
            return (float)(newStart + ((value - originalStart) * scale));
        }



        /// <summary>
        /// opens the edit alarm dialog to edit an alarm
        /// </summary>
        /// <param name="alarmDef"></param>
        /// <param name="index"></param>
        public static void EditAlarm(AlarmItemDef alarmDef, int index)
        {
            AddEditAlarm edit_dialog = new AddEditAlarm();

            // load alarm sounds
            ClockLib.ClockLib.Load_AlarmSounds();

            edit_dialog.Update_AlarmSounds();
            edit_dialog.LoadAlarm(alarmDef, index);
            edit_dialog.ShowDialog();
        }

        // continue
        public static void EditTimer(AlarmItemDef timerDef, int index)
        {
            AddEditTimer addEditTimerDialog = new AddEditTimer();

            // load alarm sounds
            ClockLib.ClockLib.Load_AlarmSounds();

            addEditTimerDialog.Update_AlarmSounds();
            addEditTimerDialog.LoadTimer(timerDef, index);
            addEditTimerDialog.ShowDialog();
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer remainingTimeUpdater = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
        public NotifyIcon notifyIcon;

        public Icon ConvertToIco(System.Drawing.Image img, string file, int size)
        {
            Icon icon;
            using (var msImg = new MemoryStream())
            using (var msIco = new MemoryStream())
            {
                img.Save(msImg, ImageFormat.Png);
                using (var bw = new BinaryWriter(msIco))
                {
                    bw.Write((short)0);           //0-1 reserved
                    bw.Write((short)1);           //2-3 image type, 1 = icon, 2 = cursor
                    bw.Write((short)1);           //4-5 number of images
                    bw.Write((byte)size);         //6 image width
                    bw.Write((byte)size);         //7 image height
                    bw.Write((byte)0);            //8 number of colors
                    bw.Write((byte)0);            //9 reserved
                    bw.Write((short)0);           //10-11 color planes
                    bw.Write((short)32);          //12-13 bits per pixel
                    bw.Write((int)msImg.Length);  //14-17 size of image data
                    bw.Write(22);                 //18-21 offset of image data
                    bw.Write(msImg.ToArray());    // write image data
                    bw.Flush();
                    bw.Seek(0, SeekOrigin.Begin);
                    icon = new Icon(msIco);
                }
                return icon;
            }
            using (var fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                icon.Save(fs);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            

            Loaded += MainWindow_Loaded;
          
        }

        private void NotifyIcon_MouseDoubleClick1(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            this.ShowInTaskbar = true;
            this.Activate();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // set controls
            Defines.controller.alarm_container = alarm_container;
            Defines.controller.timer_container = timer_container;
            Defines.controller.mainWindow = this;

            ClockLib.ClockLib.label_stopWatchTime = label_stopWatchTime;
            // set clock controller
            ClockLib.ClockLib.mainController = Defines.controller;

            // load ClockLib
            ClockLib.ClockLib.Load();

            // load toggleSwitch (Start on Windows Start)
            Load_startONWinStart_ToggleButton();

            // remaining time upater
            remainingTimeUpdater.Tick += RemainingTimeUpdater_Tick;
            remainingTimeUpdater.Start();

           

            // add alarm click event
            btn_addAlarm.Click += Btn_addAlarm_Click;

            string image = ClockLib.ClockLib.executablePath + @"\icon.png";
            // Initialize the NotifyIcon
            notifyIcon = new NotifyIcon
            {
                Icon = ConvertToIco(System.Drawing.Image.FromFile(image), image, 10), // Set your icon file
                Text = "ClockPlus",
                Visible = true,
                ContextMenuStrip = CreateContextMenu()
            };

            notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick1;



            btn_addTimer.Click += Btn_addTimer_Click;

            //Defines.controller.AddTimer("Test", "00:35:00", "default");

            // load toast ntf expire time
            DateTime dt = new DateTime(1, 1, 1);
            tp_toastNtf_expireTime.SelectedTime = dt.AddSeconds(ClockLib.ClockLib.setting_toastNotificationExpirePeriodInSeconds);

            // settings (select toast ntf expire time)
            tp_toastNtf_expireTime.SelectedTimeChanged += Tp_toastNtf_expireTime_SelectedTimeChanged;


            // load alarm volume
            settings_slider_alarmVolume.Value = ClockLib.ClockLib.soundVolume;
            settings_slider_alarmVolume.ValueChanged += Settings_slider_alarmVolume_ValueChanged;

            //settings_mediaElement.Play();
        }

        private void Settings_slider_alarmVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // update sound volume
            ClockLib.ClockLib.soundVolume = e.NewValue;
            ClockLib.ClockLib.UpdateVolume();

            // save volume to .xml
            ClockLib.ClockLib.storage_alarms.SaveVolume();
        }

        private void Tp_toastNtf_expireTime_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            DateTime newValue = e.NewValue.Value;
            if (newValue != null)
            {
                int totalSeconds = (int)newValue.TimeOfDay.TotalSeconds;
                ClockLib.ClockLib.setting_toastNotificationExpirePeriodInSeconds = (int)newValue.TimeOfDay.TotalSeconds;
                ClockLib.ClockLib.storage_alarms.DoSave();
            }

        }

        private ContextMenuStrip CreateContextMenu()
        {
            var contextMenu = new ContextMenuStrip();
            contextMenu.ItemClicked += ContextMenu_ItemClicked;
            contextMenu.Items.Add("Close Application");
            return contextMenu;
        }

        private void ContextMenu_ItemClicked(object? sender, ToolStripItemClickedEventArgs e)
        {

            if(e.ClickedItem.Text == "Close Application")
            {
                notifyIcon.Dispose();
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void Btn_addTimer_Click(object sender, RoutedEventArgs e)
        {
            AddEditTimer addEditTimerDialog = new AddEditTimer();

            // load alarm sounds
            ClockLib.ClockLib.Load_AlarmSounds();

            addEditTimerDialog.Update_AlarmSounds();
            addEditTimerDialog.ShowDialog();
        }

        private void RemainingTimeUpdater_Tick(object? sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized) { return; }

            WrapPanel alarm_container = Defines.controller.alarm_container;
            if (this.IsVisible && alarm_container.IsVisible)
            {
                foreach(object _obj in alarm_container.Children)
                {
                    AlarmItemControl itemControl = (AlarmItemControl)_obj;
                    itemControl.Update();
                }
            }
        }

        private void Btn_addAlarm_Click(object sender, RoutedEventArgs e)
        {
            AddEditAlarm dialog = new AddEditAlarm();

            // load alarm sounds
            ClockLib.ClockLib.Load_AlarmSounds();

            dialog.Update_AlarmSounds();
            dialog.ShowDialog();
        }

        // delete mode button
        private void btn_addAlarm_Copy_Click(object sender, RoutedEventArgs e)
        {
            DeleteModeUpdate();
        }

        void DeleteModeUpdate()
        {
            if (Defines.deleteModeEnabled) { Defines.deleteModeEnabled = false; } else { Defines.deleteModeEnabled = true; }

            foreach (object _object in Defines.controller.alarm_container.Children)
            {
                AlarmItemControl itemControl = _object as AlarmItemControl;
                itemControl.Update();
            }
            foreach (object _object in Defines.controller.timer_container.Children)
            {
                TimerItemControl itemControl = _object as TimerItemControl;
                itemControl.Update();
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Hide the window instead of closing it
            e.Cancel = true;
            this.WindowState = WindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void btn_deleteTimer_Click(object sender, RoutedEventArgs e)
        {
            DeleteModeUpdate();
        }

        private void btn_stopWatch_play_Click(object sender, RoutedEventArgs e)
        {
            if (!ClockLib.ClockLib.stopWatchEnabled)
            {
                ClockLib.ClockLib.StopWatch_Play();
            }
            else
            {
                ClockLib.ClockLib.StopWatch_Stop();
            }
            UpdateStopwatchIcon();
        }
        void UpdateStopwatchIcon()
        {
            if (ClockLib.ClockLib.stopWatchEnabled)
            {
                icon_btnStopWatch_play.Kind = MaterialDesignThemes.Wpf.PackIconKind.Stop;
            }
            else
            {
                icon_btnStopWatch_play.Kind = MaterialDesignThemes.Wpf.PackIconKind.Play;
            }
        }
        private void btn_stopWatch_reset_Click(object sender, RoutedEventArgs e)
        {
            ClockLib.ClockLib.StopWatch_Reset();
            UpdateStopwatchIcon();
        }

        private void tb_startOnWinStart_Checked(object sender, RoutedEventArgs e)
        {
            SetStartOnWinStart(true);
        }

        private void tb_startOnWinStart_Unchecked(object sender, RoutedEventArgs e)
        {
            SetStartOnWinStart(false);
        }
        public static void SetStartOnWinStart(bool start)
        {
            // get exePath
            string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

            // get registry key path
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            // get appName
            string appName = System.IO.Path.GetFileName(exePath);

            // set/delete value
            if (start) { reg.SetValue(appName, exePath); } else { reg.DeleteValue(appName, false); }

        }
        public void Load_startONWinStart_ToggleButton()
        {
            // get exePath
            string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

            // get registry key path
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            // get appName
            string appName = System.IO.Path.GetFileName(exePath);
            if (reg.GetValue(appName) != null)
            {
                tb_startOnWinStart.IsChecked = true;
            }

        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            settings_mediaElement.Position = new TimeSpan(0, 0, 1);
            settings_mediaElement.Play();
        }
    }
}
