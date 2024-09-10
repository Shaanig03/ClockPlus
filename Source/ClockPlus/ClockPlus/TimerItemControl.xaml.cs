using ClockLib;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClockPlus
{
    /// <summary>
    /// Interaction logic for TimerItemControl.xaml
    /// </summary>
    public partial class TimerItemControl : UserControl
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



        public TimerItemControl()
        {
            InitializeComponent();
            Loaded += TimerItemControl_Loaded;
        }

        public void Update()
        {
            // delete mode
            if (Defines.deleteModeEnabled)
            {
                // show delete button
                delete_btn.IsEnabled = true;
                delete_btn.Visibility = Visibility.Visible;
            }
            else
            {
                // hide delete button
                delete_btn.IsEnabled = false;
                delete_btn.Visibility = Visibility.Hidden;
            }

            // update command count
            int index = (int)Tag;
            AlarmItemDef timerDef = ClockLib.ClockLib.timerDefs[index];
            List<ClockCommand> clockCommands = timerDef.commands;
            if (clockCommands != null)
            {
                int clockCmdCount = clockCommands.Count;
                if (clockCmdCount > 0)
                {
                    l_cmdCount.Visibility = Visibility.Visible;
                    l_cmdCount.Content = clockCmdCount + " cmd";
                }
                else
                {
                    l_cmdCount.Visibility = Visibility.Hidden;
                }
            }


            UpdateIcon();

            // update remaining time
            UpdateRemainingTime();
        }

        public void UpdateIcon()
        {
            // update command count
            int index = (int)Tag;
            AlarmItemDef timerDef = ClockLib.ClockLib.timerDefs[index];
            // if timer is playing
            if (timerDef.enabled)
            {
                // show pause icon while timer is stopped
                icon_btnPlay.Kind = MaterialDesignThemes.Wpf.PackIconKind.Pause;
            }
            else
            {
                // show play icon while timer is stopped
                icon_btnPlay.Kind = MaterialDesignThemes.Wpf.PackIconKind.Play;
            }
        }

        // updates remaining time
        public void UpdateRemainingTime()
        {
            int index = (int)Tag;
            AlarmItemDef timerDef = ClockLib.ClockLib.timerDefs[index];

            TimeSpan remainingTime;
            string str_time = "00:00:00";

            DateTime now = DateTime.Now;
            // if timer is enabled
            if(timerDef.enabled)
            {
                remainingTime = timerDef.assigned_alarmPeriod - now;
                str_time = ClockLib.ClockLib.GetFullTimeInString(remainingTime);
            }
            else
            {
                if(timerDef.str_current_alarmPeriod == "")
                {
                    remainingTime = DateTime.Parse(timerDef.alarmTime) - now;
                    str_time = ClockLib.ClockLib.GetFullTimeInString(remainingTime);
                }
                else
                {
                    remainingTime = timerDef.timer_remainingTime;
                    str_time = ClockLib.ClockLib.GetFullTimeInString(remainingTime);
                }
           
            }

            

            // update progress bar
            double remainingTime_totalSeconds = remainingTime.TotalSeconds;
            if (remainingTime_totalSeconds > 0)
            {
                try
                {
                    l_time.Content = str_time;
                    double percentage = (remainingTime.TotalSeconds / timerDef.timer_remainingTime.TotalSeconds) * 100;
                    circularProgressBar.Value = percentage;
                }catch(Exception e) { }
            }
            else
            {
                l_time.Content = timerDef.alarmTime;
            }
            
            /*
             
        // diplays the selected timer item's remaining time
        public static void Test_UpdateTimeDisplay()
        {
            // get selected timer def
            AlarmItemDef sel_timerDef = current_sel_timerDef;

            // remaining time
            string txt = "00:00:00";

            // if a timer item is selected
            if (sel_timerDef != null)
            {
                // get remaining time
                TimeSpan remainingTime = sel_timerDef.assigned_alarmPeriod - DateTime.Now;
                
                // if timer is stopped then get the TimeSpan from 'timer_remainingTime'
                if (!sel_timerDef.enabled)
                {
                    // if an alarm period is assigned
                    if(sel_timerDef.str_current_alarmPeriod != "")
                    {
                        // display the remaining time
                        remainingTime = sel_timerDef.timer_remainingTime;
                    }
                    else
                    {
                        // if no alarm period is assigned then show the full time
                        remainingTime = TimeSpan.Parse(sel_timerDef.alarmTime);
                    }
                    
                }
                
                if(remainingTime < TimeSpan.Zero)
                {
                    remainingTime = TimeSpan.Zero;
                }

                // set text
                //txt = remainingTime.Hours + ":" + remainingTime.Minutes + ":" + remainingTime.Seconds + "." + remainingTime.Milliseconds;
                txt = string.Format("{0}:{1}:{2}", remainingTime.Hours, remainingTime.Minutes, remainingTime.Seconds);
                if(remainingTime.Milliseconds > 0)
                {
                    txt += "." + remainingTime.Milliseconds.ToString();
                }
            }

            // update label
            label_timerTime.Content = txt;
        }
        
            */
            /* ---
            int index = (int)Tag;
            // get alarm def
            AlarmItemDef alarmDef = ClockLib.ClockLib.alarmDefs[index];

            // get current date & time
            DateTime now = DateTime.Now;

            if (alarmDef.str_current_alarmPeriod == "")
            {
                l_title.Content = ClockLib.ClockLib.GetFullTimeInString((alarmDef.assigned_alarmPeriod - now));
            }
            else
            {
                l_title.Content = "00:00:00";
            }*/
            /*
            // if an alarm period is assigned
            if (alarmDef.str_current_alarmPeriod != "")
            {
                // set the remaining time
                //l_remainingTime.Content = alarmDef.assigned_alarmPeriod.GetRemainingTime(now);
            }
            else
            {
                // set the remaining time by parsing a DateTime if no alarm period is assigned
                DateTime alarmTime = DateTime.Parse(alarmDef.alarmTime);
                l_remainingTime.Content = alarmTime.GetRemainingTime(now);
            }
            */
        }

        private void TimerItemControl_Loaded(object sender, RoutedEventArgs e)
        {
            _defaultColor = border1.BorderBrush;
            _hoverColor = new SolidColorBrush(Color.FromArgb((byte)255, (byte)0, (byte)139, (byte)139));

            MouseEnter += TimerItemControl_MouseEnter;
            MouseLeave += TimerItemControl_MouseLeave;
        }

        private void TimerItemControl_MouseLeave(object sender, MouseEventArgs e)
        {
            border1.BorderBrush = _defaultColor;
        }

        private void TimerItemControl_MouseEnter(object sender, MouseEventArgs e)
        {
            border1.BorderBrush = _hoverColor;
            
        }

        private void ripple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            object tag = Tag;

            if (tag != null)
            {
                int index = (int)tag;

                AlarmItemDef alarmItemDef = ClockLib.ClockLib.timerDefs[index];
                if (!alarmItemDef.enabled)
                {
                    Defines.EditTimer(alarmItemDef, index);
                }
                else
                {
                    MessageBox.Show("cannot edit a timer while the timer is playing");
                }
            
            }
            else
            {
                MessageBox.Show("tag null");
            }
        }
    }
}
