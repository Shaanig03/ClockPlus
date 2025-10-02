using BirdNest.Experimental;
using BirdNest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace ClockLib
{
    /// <summary>
    /// Created using the 'XMLStorage' class, which is used for storing and loading data from an xml file
    /// </summary>
    public class XMLStorageAlarms : XMLStorage
    {
        public bool isSaveNew = false;

        // Token: 0x0600003B RID: 59 RVA: 0x000047D0 File Offset: 0x000029D0
        public XMLStorageAlarms(string _file = "", bool _autoLoad = true) : base(_file, _autoLoad)
        {
        }

        // Token: 0x0600003C RID: 60 RVA: 0x000047DC File Offset: 0x000029DC
        public override void DoSave()
        {
            bool flag = !File.Exists(this.file);
            if (flag)
            {
                this.CreateDataFile();
            }
            XmlNode alarmParentNode = this.xdoc.SelectSingleNode("storage/alarms");
            alarmParentNode.RemoveAll();
            for (int i = 0; i < ClockLib.max_alarmDefs; i++)
            {
                AlarmItemDef alarmDef;
                bool flag2 = ClockLib.alarmDefs.TryGetValue(i, out alarmDef);
                if (flag2)
                {
                    XmlNode parentNode3 = alarmParentNode;
                    string name = "alarm";
                    string innerText = "";
                    string[,] array = new string[7, 2];
                    array[0, 0] = "alarmTitle";
                    array[0, 1] = alarmDef.title;
                    array[1, 0] = "alarmTime";
                    array[1, 1] = alarmDef.alarmTime;
                    array[2, 0] = "dateType";
                    array[2, 1] = alarmDef.dateType;
                    array[3, 0] = "dateValue";
                    array[3, 1] = alarmDef.dateValue;
                    array[4, 0] = "enabled";
                    array[4, 1] = alarmDef.enabled.ToString();
                    array[5, 0] = "current_alarmPeriod";
                    array[5, 1] = alarmDef.str_current_alarmPeriod;
                    array[6, 0] = "alarmSound";
                    array[6, 1] = alarmDef.alarmSound;
                    XmlNode parentNode = parentNode3.NewNode(name, innerText, array);
                    XmlNode commandParentNode = parentNode.NewNode("cmds", "", new string[0, 0]);
                    bool flag3 = alarmDef.commands != null;
                    if (flag3)
                    {
                        foreach (ClockCommand _command in alarmDef.commands)
                        {
                            XmlNode parentNode4 = commandParentNode;
                            string name2 = "cmd";
                            string innerText2 = "";
                            string[,] array2 = new string[1, 2];
                            array2[0, 0] = "type";
                            array2[0, 1] = _command.type.ToString();
                            XmlNode commandNode = parentNode4.NewNode(name2, innerText2, array2);
                            foreach (string _str_arg in _command.args)
                            {
                                commandNode.NewNode("param", _str_arg, new string[0, 0]);
                            }
                        }
                    }
                }
            }
            XmlNode timerParentNode = this.xdoc.SelectSingleNode("storage/timers");
            timerParentNode.RemoveAll();
            for (int i_timer = 0; i_timer < ClockLib.max_timerDefs; i_timer++)
            {
                AlarmItemDef timerDef;
                bool flag4 = ClockLib.timerDefs.TryGetValue(i_timer, out timerDef);
                if (flag4)
                {
                    TimeSpan remainingTime = timerDef.assigned_alarmPeriod - DateTime.Now;
                    bool flag5 = !timerDef.enabled;
                    if (flag5)
                    {
                        remainingTime = timerDef.timer_remainingTime;
                    }
                    XmlNode parentNode5 = timerParentNode;
                    string name3 = "timer";
                    string innerText3 = "";
                    string[,] array3 = new string[5, 2];
                    array3[0, 0] = "title";
                    array3[0, 1] = timerDef.title;
                    array3[1, 0] = "time";
                    array3[1, 1] = timerDef.alarmTime;
                    array3[2, 0] = "current_alarmPeriod";
                    array3[2, 1] = timerDef.str_current_alarmPeriod;
                    array3[3, 0] = "alarmSound";
                    array3[3, 1] = timerDef.alarmSound;
                    array3[4, 0] = "remainingTime";
                    array3[4, 1] = string.Format("{0}:{1}:{2}.{3}", new object[]
                    {
                        remainingTime.Hours,
                        remainingTime.Minutes,
                        remainingTime.Seconds,
                        remainingTime.Milliseconds
                    });
                    XmlNode parentNode2 = parentNode5.NewNode(name3, innerText3, array3);
                    XmlNode commandParentNode2 = parentNode2.NewNode("cmds", "", new string[0, 0]);
                    bool flag6 = timerDef.commands != null;
                    if (flag6)
                    {
                        foreach (ClockCommand _command2 in timerDef.commands)
                        {
                            XmlNode parentNode6 = commandParentNode2;
                            string name4 = "cmd";
                            string innerText4 = "";
                            string[,] array4 = new string[1, 2];
                            array4[0, 0] = "type";
                            array4[0, 1] = _command2.type.ToString();
                            XmlNode commandNode2 = parentNode6.NewNode(name4, innerText4, array4);
                            foreach (string _str_arg2 in _command2.args)
                            {
                                commandNode2.NewNode("param", _str_arg2, new string[0, 0]);
                            }
                        }
                    }
                }
            }
            XmlNode stopWatchNode = this.xdoc.SelectSingleNode("storage/stopWatch");
            bool stopWatchEnabled = ClockLib.stopWatchEnabled;
            string str_timeSpan;
            if (stopWatchEnabled)
            {
                str_timeSpan = string.Format("{0:hh\\:mm\\:ss\\.fff}", ClockLib.GetStopWatchTime());
            }
            else
            {
                str_timeSpan = string.Format("{0:hh\\:mm\\:ss\\.fff}", ClockLib.stopWatchTime);
            }
            stopWatchNode.Attributes.GetNamedItem("time").Value = str_timeSpan;
            XmlNode tstntf_expireTimeNode = this.xdoc.SelectSingleNode("storage/toastNotification_expireTimeInSeconds");
            bool flag7 = tstntf_expireTimeNode != null;
            if (flag7)
            {
                tstntf_expireTimeNode.Attributes.GetNamedItem("Seconds").Value = ClockLib.setting_toastNotificationExpirePeriodInSeconds.ToString();
            }
            else
            {
                XmlNode parentNode7 = this.xdoc.SelectSingleNode("storage");
                string name5 = "toastNotification_expireTimeInSeconds";
                string innerText5 = "";
                string[,] array5 = new string[1, 2];
                array5[0, 0] = "Seconds";
                array5[0, 1] = ClockLib.setting_toastNotificationExpirePeriodInSeconds.ToString();
                tstntf_expireTimeNode = parentNode7.RegisterNode(name5, innerText5, array5);
            }
            this.SaveVolume();
            SaveSelectedAlarmSound();
          
            base.Save();
        }
        public void SaveSelectedAlarmSound()
        {
            XmlNode node_selectedAlarmSound = xdoc.SelectSingleNode("storage/selectedAlarmSound");
            node_selectedAlarmSound.InnerText = ClockLib.selectedAlarmSound;
            this.Save();
        }
        // Token: 0x0600003D RID: 61 RVA: 0x00004DD0 File Offset: 0x00002FD0
        public void SaveVolume()
        {
            XmlNode node_alarmVolume = this.xdoc.SelectSingleNode("storage/alarmVolume");
            bool flag = node_alarmVolume != null;
            if (flag)
            {
                node_alarmVolume.InnerText = ClockLib.soundVolume.ToString();
            }
            else
            {
                this.xdoc.SelectSingleNode("storage").RegisterNode("alarmVolume", ClockLib.soundVolume.ToString(), new string[0, 0]);
            }
            base.Save();
        }

        // Token: 0x0600003E RID: 62 RVA: 0x00004E44 File Offset: 0x00003044
        public override void DoLoad()
        {
            base.Load();
            bool flag = !this.loaded;
            if (flag)
            {
                this.CreateDataFile();
                this.isSaveNew = true;
            }
            ClockLib.alarmDefs.Clear();
            XmlNode alarmParentNode = this.xdoc.SelectSingleNode("storage/alarms");
            foreach (object obj in alarmParentNode.ChildNodes)
            {
                XmlNode _alarmNode = (XmlNode)obj;
                string title = _alarmNode.Attributes.GetNamedItem("alarmTitle").Value;
                string alarmTime = _alarmNode.Attributes.GetNamedItem("alarmTime").Value;
                string dateType = _alarmNode.Attributes.GetNamedItem("dateType").Value;
                string dateValue = _alarmNode.Attributes.GetNamedItem("dateValue").Value;
                string enabled = _alarmNode.Attributes.GetNamedItem("enabled").Value;
                string current_alarmPeriod = _alarmNode.Attributes.GetNamedItem("current_alarmPeriod").Value;
                string alarmSound = _alarmNode.Attributes.GetNamedItem("alarmSound").Value;
                List<ClockCommand> clockCommands = new List<ClockCommand>();
                XmlNode parentNodeCommands = _alarmNode.SelectSingleNode("cmds");
                bool flag2 = parentNodeCommands != null;
                if (flag2)
                {
                    foreach (object obj2 in parentNodeCommands.ChildNodes)
                    {
                        XmlNode _cmdNode = (XmlNode)obj2;
                        string str_cmdType = _cmdNode.Attributes.GetNamedItem("type").Value;
                        ClockCommandType cmdType = ClockCommandType.Execute_CS_File;
                        bool flag3 = Enum.TryParse<ClockCommandType>(str_cmdType, out cmdType);
                        if (!flag3)
                        {
                            throw new Exception("XMLStorageAlarms.Load(): Enum.TryParse from 'str_cmdType' failed");
                        }
                        List<string> _args = new List<string>();
                        foreach (object obj3 in _cmdNode.ChildNodes)
                        {
                            XmlNode _argNode = (XmlNode)obj3;
                            _args.Add(_argNode.InnerText);
                        }
                        clockCommands.Add(new ClockCommand
                        {
                            type = cmdType,
                            args = _args
                        });
                    }
                }
                int alarmIndex = ClockLib.AddAlarm(title, alarmTime, dateType, dateValue, bool.Parse(enabled), current_alarmPeriod, alarmSound, clockCommands);
                bool flag4 = current_alarmPeriod != "";
                if (flag4)
                {
                    ClockLib.alarmDefs[alarmIndex].assigned_alarmPeriod = DateTime.Parse(current_alarmPeriod);
                }
            }
            ClockLib.timerDefs.Clear();
            XmlNode timerParentNode = this.xdoc.SelectSingleNode("storage/timers");
            foreach (object obj4 in timerParentNode.ChildNodes)
            {
                XmlNode _timerNode = (XmlNode)obj4;
                string title2 = _timerNode.Attributes.GetNamedItem("title").Value;
                string time = _timerNode.Attributes.GetNamedItem("time").Value;
                string current_alarmPeriod2 = _timerNode.Attributes.GetNamedItem("current_alarmPeriod").Value;
                string alarmSound2 = _timerNode.Attributes.GetNamedItem("alarmSound").Value;
                string str_remainingTime = _timerNode.Attributes.GetNamedItem("remainingTime").Value;
                TimeSpan remainingTime = TimeSpan.Parse(str_remainingTime);
                List<ClockCommand> clockCommands2 = new List<ClockCommand>();
                XmlNode parentNodeCommands2 = _timerNode.SelectSingleNode("cmds");
                bool flag5 = parentNodeCommands2 != null;
                if (flag5)
                {
                    foreach (object obj5 in parentNodeCommands2.ChildNodes)
                    {
                        XmlNode _cmdNode2 = (XmlNode)obj5;
                        string str_cmdType2 = _cmdNode2.Attributes.GetNamedItem("type").Value;
                        ClockCommandType cmdType2 = ClockCommandType.Execute_CS_File;
                        bool flag6 = Enum.TryParse<ClockCommandType>(str_cmdType2, out cmdType2);
                        if (!flag6)
                        {
                            throw new Exception("XMLStorageAlarms.Load(): Enum.TryParse from 'str_cmdType' failed");
                        }
                        List<string> _args2 = new List<string>();
                        foreach (object obj6 in _cmdNode2.ChildNodes)
                        {
                            XmlNode _argNode2 = (XmlNode)obj6;
                            _args2.Add(_argNode2.InnerText);
                        }
                        clockCommands2.Add(new ClockCommand
                        {
                            type = cmdType2,
                            args = _args2
                        });
                    }
                }
                AlarmItemDef timerDef = ClockLib.AddTimer(title2, time, "default", null);
                timerDef.str_current_alarmPeriod = current_alarmPeriod2;
                timerDef.alarmSound = alarmSound2;
                timerDef.timer_remainingTime = remainingTime;
                timerDef.commands = clockCommands2;
            }
            XmlNode stopWatchNode = this.xdoc.SelectSingleNode("storage/stopWatch");
            string stopWatch_time = stopWatchNode.Attributes.GetNamedItem("time").Value;
            bool flag7 = stopWatch_time != "" && ClockLib.label_stopWatchTime != null;
            if (flag7)
            {
                ClockLib.stopWatchTime = TimeSpan.Parse(stopWatch_time);
                string formattedTime = string.Format("{0:hh\\:mm\\:ss\\.ff}", ClockLib.stopWatchTime);
                ClockLib.label_stopWatchTime.Content = formattedTime;
            }
            int tstNtf_expireTime = 300;
            XmlNode tstNtf_expireTimeNode = this.xdoc.SelectSingleNode("storage/toastNotification_expireTimeInSeconds");
            bool flag8 = tstNtf_expireTimeNode != null;
            if (flag8)
            {
                bool flag9 = int.TryParse(tstNtf_expireTimeNode.Attributes.GetNamedItem("Seconds").Value, out tstNtf_expireTime);
                if (flag9)
                {
                    ClockLib.setting_toastNotificationExpirePeriodInSeconds = tstNtf_expireTime;
                }
            }
            XmlNode node_alarmVolume = this.xdoc.SelectSingleNode("storage/alarmVolume");
            bool flag10 = node_alarmVolume != null;
            if (flag10)
            {
                double volume = double.Parse(node_alarmVolume.InnerText);
                ClockLib.soundVolume = volume;
                ClockLib.UpdateVolume();
            }
        }

        // Token: 0x0600003F RID: 63 RVA: 0x000054AC File Offset: 0x000036AC
        public void CreateDataFile()
        {
            XmlWriter writer = this.file.NewXmlWriter();
            writer.WriteStartElement("storage");

            writer.WriteStartElement("alarms");
            writer.WriteEndElement();

            writer.WriteStartElement("timers");
            writer.WriteEndElement();

            writer.WriteStartElement("stopWatch");
            writer.WriteAttributeString("time", "00:00:00");
            writer.WriteEndElement();

            writer.WriteStartElement("selectedAlarmSound");
            writer.WriteString("default");
            writer.WriteEndElement();




            writer.WriteEndElement();


            writer.Close();
            this.xdoc.Load(this.file);
            Debug.WriteLine("new xml data file '" + this.file + "' created");

           
        }
    }
}
