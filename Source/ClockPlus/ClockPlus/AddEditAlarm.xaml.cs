using ClockLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MaterialDesignColors;
using MaterialDesignThemes;
using MaterialDesignThemes.Wpf;

namespace ClockPlus
{
    /// <summary>
    /// Interaction logic for AddEditAlarm.xaml
    /// </summary>
    public partial class AddEditAlarm : Window
    {
        public AAPageDefault dateValuePage_default = new AAPageDefault();
        public AAPageSpecificDate dateValuePage_specificDate = new AAPageSpecificDate();
        public AAPageSpecificDates dateValuePage_specificDates = new AAPageSpecificDates();

        public string selected_alarmTitle = "";
        public DateTime? selected_alarmTime = DateTime.Parse("12:00 PM");
        public string selected_alarmType = "default";
        public List<ClockCommand> selected_commands = new List<ClockCommand>();

        public int updatingAlarmIndex = -1;

        public void LoadAlarm(AlarmItemDef itemDef, int index)
        {
            updatingAlarmIndex = index;

            // set title
            string title = itemDef.title;
            selected_alarmTitle = title;
            txtbox_title.Text = title;

            // set alarm time
            DateTime alarmTime = DateTime.Parse(itemDef.alarmTime);
            selected_alarmTime = alarmTime;
            tp_alarmTime.SelectedTime = alarmTime;

            // set alarm type
            string alarmType = itemDef.dateType;
            selected_alarmType = alarmType;
            switch (alarmType)
            {
                case "default":
                    cb_alarmType.SelectedIndex = 0;

                    if (itemDef.dateValue.Contains("0"))
                    {
                        dateValuePage_default.tb_sun.IsChecked = true;
                    }
                    if (itemDef.dateValue.Contains("1"))
                    {
                        dateValuePage_default.tb_mon.IsChecked = true;
                    }
                    if (itemDef.dateValue.Contains("2"))
                    {
                        dateValuePage_default.tb_tues.IsChecked = true;
                    }
                    if (itemDef.dateValue.Contains("3"))
                    {
                        dateValuePage_default.tb_wed.IsChecked = true;
                    }
                    if (itemDef.dateValue.Contains("4"))
                    {
                        dateValuePage_default.tb_th.IsChecked = true;
                    }
                    if (itemDef.dateValue.Contains("5"))
                    {
                        dateValuePage_default.tb_fri.IsChecked = true;
                    }
                    if (itemDef.dateValue.Contains("6"))
                    {
                        dateValuePage_default.tb_sat.IsChecked = true;
                    }

                    break;
                case "specificDate":
                    // tested
                    cb_alarmType.SelectedIndex = 1;
                    dateValuePage_specificDate.pickDate.SelectedDate = DateTime.Parse(itemDef.dateValue);
                    break;
                case "specificDates":
                    // tested
                    // get the dates which this alarm would be active
                    List<DateTime> assignedDates = new List<DateTime>();
                    string[] str_assignedDates = itemDef.dateValue.Split('@'); foreach (string _str_date in str_assignedDates) { assignedDates.Add(DateTime.Parse(_str_date)); }
                    assignedDates.Sort((x, y) => DateTime.Compare(x, y));

                    foreach(DateTime _assignedDate in assignedDates)
                    {
                        ListBoxItem lbItem = new ListBoxItem();
                        lbItem.Content = _assignedDate.Month + "/" + _assignedDate.Day + "/" + _assignedDate.Year;

                        dateValuePage_specificDates.lb_dates.Items.Add(lbItem);
                    }
           

                    cb_alarmType.SelectedIndex = 2;
                    break;
                case "everyMonth":
                    cb_alarmType.SelectedIndex = 3;
                    break;
                case "everyYear":
                    cb_alarmType.SelectedIndex = 4;
                    break;
                default:
                    break;
            }

            // set alarm sound
            string alarmSound = itemDef.alarmSound;
            int c = cb_alarmSound.Items.Count;
            for (int i = 0; i < cb_alarmSound.Items.Count; i++)
            {
                ComboBoxItem cbItem = cb_alarmSound.Items.GetItemAt(i) as ComboBoxItem;
                string cbItem_content = cbItem.Content as string;
                if(cbItem_content == alarmSound) { cb_alarmSound.SelectedItem = cbItem; break; }
            }

            // set commands
            selected_commands = itemDef.commands;
            

        }


        public AddEditAlarm()
        {
            InitializeComponent();
            Loaded += AddEditAlarm_Loaded;
        }

        private void AddEditAlarm_Loaded(object sender, RoutedEventArgs e)
        {
            cb_alarmType.SelectionChanged += Cb_alarmType_SelectionChanged;
            UpdateAlarmType();

            btn_save.Click += Btn_save_Click;
            btn_cancel.Click += Btn_cancel_Click;
            btn_cmds.Click += Btn_cmds_Click;
            this.Activate();
        }

        // command management
        private void Btn_cmds_Click(object sender, RoutedEventArgs e)
        {
            ClockCmdDialog cmdDialog = new ClockCmdDialog();
            cmdDialog.LoadCommands(selected_commands);

            cmdDialog.ShowDialog();

            // only set the commands if save button was clicked
            if (cmdDialog.saved) { selected_commands = cmdDialog.selectedCommands; }
        
        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            Save();
            Debug.WriteLine("Button save - clicked");
        }

        private void Cb_alarmType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAlarmType();
        }

        void UpdateAlarmType()
        {
            ComboBoxItem cbItem = cb_alarmType.SelectedItem as ComboBoxItem;
            if (cbItem != null)
            {
                string itemContent = cbItem.Content as string;

                if (itemContent == "default" || itemContent == "specificDate" || itemContent == "specificDates" || itemContent == "everyMonth" || itemContent == "everyYear")
                {
                    selected_alarmType = itemContent;

                    switch (itemContent)
                    {
                        case "default":
                            DateValueFrame.Content = dateValuePage_default;
                            break;
                        case "specificDate":
                            DateValueFrame.Content = dateValuePage_specificDate;
                            break;
                        case "specificDates":
                            DateValueFrame.Content = dateValuePage_specificDates;
                            break;
                        default:
                            DateValueFrame.Content = null;
                            break;
                    }
                }
            }
        }


        // adds alarm sounds to (combobox)
        public void Update_AlarmSounds()
        {
            cb_alarmSound.Items.Clear();

            List<string> soundNames = new List<string>();
            foreach (ClockAlarmSoundDef soundDef in ClockLib.ClockLib.alarmSounds)
            {
                ComboBoxItem cbItem = new ComboBoxItem();
                string soundName = soundDef.soundName;
                //cbItem.Name = soundDef.soundName;
                cbItem.Content = soundName;

                cb_alarmSound.Items.Add(cbItem);
                soundNames.Add(soundName);
            }

            // get selected alarm sound
            string selectedAlarmSound = ClockLib.ClockLib.selectedAlarmSound;

            if (soundNames.Contains(selectedAlarmSound))
            {
                foreach (object _item in cb_alarmSound.Items)
                {
                    ComboBoxItem cbItem = (ComboBoxItem)_item;

                    string content = cbItem.Content as string;
                    if (content == selectedAlarmSound)
                    {
                        cb_alarmSound.SelectedItem = cbItem;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show($"Warning: alarm sound '{selectedAlarmSound}' not found");
            }


            cb_alarmSound.SelectionChanged += cb_alarmSound_SelectionChanged;
        }

        private void cb_alarmSound_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = cb_alarmSound.SelectedValue as ComboBoxItem;

            if (item != null)
            {
                ClockLib.ClockLib.selectedAlarmSound = (string)item.Content;
                ClockLib.ClockLib.storage_alarms.SaveSelectedAlarmSound();
            }

        }

        // alarm title
        private void txtbox_title_TextChanged(object sender, TextChangedEventArgs e)
        {
            selected_alarmTitle = txtbox_title.Text;
        }


        // time picker (selecting alarm time)
        private void tp_alarmTime_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            DateTime? selValue = e.NewValue;

            if (selValue != null)
            {
                selected_alarmTime = selValue;
            }
        }

        // combobox (selecting alarm type)
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        void Save()
        {
            if(selected_alarmTitle == "") { MessageBox.Show("set an alarm title before saving");return; }

            string dateValue = "";
            string alarmSound = "default";

            ComboBoxItem cb_item_alarmSound = cb_alarmSound.SelectedItem as ComboBoxItem;
            if (cb_item_alarmSound != null)
            {
                alarmSound = cb_item_alarmSound.Content as string;
            }
            else
            {
                MessageBox.Show("warning: no sound was selected for this alarm"); return;
            }

            switch (selected_alarmType)
            {
                // tested 
                case "default":
                    if ((bool)dateValuePage_default.tb_sun.IsChecked) { dateValue += "0"; }
                    if ((bool)dateValuePage_default.tb_mon.IsChecked) { dateValue += "1"; }
                    if ((bool)dateValuePage_default.tb_tues.IsChecked) { dateValue += "2"; }
                    if ((bool)dateValuePage_default.tb_wed.IsChecked) { dateValue += "3"; }
                    if ((bool)dateValuePage_default.tb_th.IsChecked) { dateValue += "4"; }
                    if ((bool)dateValuePage_default.tb_fri.IsChecked) { dateValue += "5"; }
                    if ((bool)dateValuePage_default.tb_sat.IsChecked) { dateValue += "6"; }
                    break;
                case "specificDate":
                    // tested
                    DateTime pickedDate = (DateTime)dateValuePage_specificDate.pickDate.SelectedDate;
                    dateValue = pickedDate.Month + "/" + pickedDate.Day + "/" + pickedDate.Year;
                    break;
                case "specificDates":

                    int specificDateCount = dateValuePage_specificDates.lb_dates.Items.Count;
                    int lastIndex = specificDateCount - 1;
                    // loop through each specific date
                    for (int i = 0; i < specificDateCount; i++)
                    {
                        // get listbox item
                        ListBoxItem lbItem = dateValuePage_specificDates.lb_dates.Items.GetItemAt(i) as ListBoxItem;

                        // get listbox text
                        string __str_date = lbItem.Content as string;

                        // if i is the last index
                        if (i == lastIndex)
                        {
                            // then add date
                            dateValue += __str_date;
                        }
                        else
                        {
                            // add the seperator if it's not the last date
                            dateValue += __str_date + "@";
                        }

                    }
                    //
                    break;
                default:
                    break;
            }

            DateTime sel_alarmTime = (DateTime)selected_alarmTime;

            // if no updating alarm index is assigned, then add a new alarm
            if (updatingAlarmIndex == -1)
            {
                Defines.controller.AddAlarm(selected_alarmTitle, sel_alarmTime.ToString("hh:mm tt"), selected_alarmType, dateValue, true, "", alarmSound, selected_commands);
            }
            else
            {
                AlarmItemDef alarmDef = ClockLib.ClockLib.alarmDefs[updatingAlarmIndex];

                alarmDef.str_current_alarmPeriod = "";
                alarmDef.assigned_alarmPeriod = new DateTime(1, 1, 1);

                alarmDef.title = selected_alarmTitle;
                alarmDef.alarmTime = sel_alarmTime.ToString("hh:mm tt");
                alarmDef.dateType = selected_alarmType;
                alarmDef.dateValue = dateValue;
                alarmDef.alarmSound = alarmSound;
                alarmDef.commands = selected_commands;


                
            }

            ClockLib.ClockLib.Update();
            this.Close();
        }

      

    }
}
