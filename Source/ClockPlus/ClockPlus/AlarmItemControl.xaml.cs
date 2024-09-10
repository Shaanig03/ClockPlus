using ClockLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClockPlus
{
    /// <summary>
    /// Interaction logic for AlarmItemControl.xaml
    /// </summary>
    public partial class AlarmItemControl : UserControl
    {
        [Description("Test text displayed in the textbox"), Category("Brush")]
        public Brush hoverColor
        {
            get { return _hoverColor; }
            set
            {
                _hoverColor = value;
            }
        }
        private Brush _hoverColor;
        private Brush _defaultColor;

        public void Update()
        {
            // delete mode
            if(Defines.deleteModeEnabled)
            {
                // hide toggle switch (in delete mode)
                ts_enabled.IsEnabled = false;
                ts_enabled.Visibility = Visibility.Hidden;

                // show delete button
                delete_btn.IsEnabled = true;
                delete_btn.Visibility = Visibility.Visible;
            }
            else
            {
                // show toggle switch
                ts_enabled.IsEnabled = true;
                ts_enabled.Visibility = Visibility.Visible;

                // hide delete button
                delete_btn.IsEnabled = false;
                delete_btn.Visibility = Visibility.Hidden;
            }

            // update command count
            int index = (int)Tag;
            AlarmItemDef alarmDef = ClockLib.ClockLib.alarmDefs[index];
            List<ClockCommand> clockCommands = alarmDef.commands;
            int clockCmdCount = clockCommands.Count;
            if (clockCmdCount > 0 )
            {
                l_cmdCount.Visibility = Visibility.Visible;
                l_cmdCount.Content = clockCmdCount + " cmd";
            }
            else
            {
                l_cmdCount.Visibility = Visibility.Hidden;
            }

            // update remaining time
            UpdateRemainingTime();
        }

        // updates remaining time
        public void UpdateRemainingTime()
        {
            
            int index = (int)Tag;
            // get alarm def
            AlarmItemDef alarmDef = ClockLib.ClockLib.alarmDefs[index];

            // get current date & time
            DateTime now = DateTime.Now;

            // if an alarm period is assigned
            if(alarmDef.str_current_alarmPeriod != "")
            {
                // set the remaining time
                l_remainingTime.Content = alarmDef.assigned_alarmPeriod.GetRemainingTime(now);
            }
            else
            {
                // set the remaining time by parsing a DateTime if no alarm period is assigned
                DateTime alarmTime = DateTime.Parse(alarmDef.alarmTime);
                l_remainingTime.Content = alarmTime.GetRemainingTime(now);
            }
           
        }
        public AlarmItemControl()
        {
            InitializeComponent();
            Loaded += AlarmItemControl_Loaded;
        }

        private void AlarmItemControl_Loaded(object sender, RoutedEventArgs e)
        {
            _defaultColor = border1.BorderBrush;
            _hoverColor = new SolidColorBrush(Color.FromArgb((byte)255, (byte)0, (byte)139, (byte)139));

            this.MouseEnter += AlarmItemControl_MouseEnter;
            this.MouseLeave += AlarmItemControl_MouseLeave;

            
        }
        private void AlarmItemControl_MouseEnter(object sender, MouseEventArgs e)
        {
            border1.BorderBrush = _hoverColor;
        }

        private void AlarmItemControl_MouseLeave(object sender, MouseEventArgs e)
        {
            border1.BorderBrush = _defaultColor;
        }


        private void ripple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            object tag = Tag;

            if (tag != null)
            {
                int index = (int)tag;
                Defines.EditAlarm(ClockLib.ClockLib.alarmDefs[index], index);
            }
            else
            {
                MessageBox.Show("tag null");
            }
        }
    }
}
