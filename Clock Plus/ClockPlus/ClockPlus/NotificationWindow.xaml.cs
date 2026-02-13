using ClockLib;
using System;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClockPlus
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        public AlarmItemDef alarmItem;

        public NotificationWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            OnClose();
        }


        void OnClose(bool close = false)
        {
            var ntf = NotificationSystem.notifications.Find(x => x.window == this);

            if (ntf != null)
            {
                NotificationSystem.notifications.Remove(ntf);

                // stop timer if there aren't any notifications
                if (NotificationSystem.notifications.Count == 0)
                {
                    NotificationSystem.displayTimer.Stop();
                    ClockLib.ClockLib.StopSound();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            HwndTarget hwndTarget = hwndSource.CompositionTarget;
            hwndTarget.RenderMode = RenderMode.SoftwareOnly;

            cb_snoozeTime.SelectionChanged += Cb_snoozeTime_SelectionChanged;
            /*
             * <ComboBoxItem Content=""/>
            <ComboBoxItem Content=""/>
            <ComboBoxItem Content=""/>
            <ComboBoxItem Content=""/>
            <ComboBoxItem Content=""/>
            <ComboBoxItem Content=""/>
            <ComboBoxItem Content=""/>
            <ComboBoxItem Content=""/>
             * */
        }

        private void Cb_snoozeTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ComboBoxItem)cb_snoozeTime.SelectedValue;

            if (item != null)
            {
                string? content = item.Content as string;

                DateTime snooze_time = DateTime.Now;
                bool snoozing = true;

                switch (content)
                {
                    case "None": { snoozing = false; break; }
                    case "5 Minutes": { snooze_time = snooze_time.AddMinutes(5); break; }
                    case "10 Minutes": { snooze_time = snooze_time.AddMinutes(10); break; }
                    case "20 Minutes": { snooze_time = snooze_time.AddMinutes(20); break; }
                    case "30 Minutes": { snooze_time = snooze_time.AddMinutes(30); break; }
                    case "1 Hour": { snooze_time = snooze_time.AddHours(1); break; }
                    case "1 Hour 30 Minutes": { snooze_time = snooze_time.AddHours(1).AddMinutes(30); break; }
                    case "2 Hours": { snooze_time = snooze_time.AddHours(2); break; }
                    default: { snoozing = false; break; }
                }

                if (snoozing)
                {
                    alarmItem.snoozed = true;
                    alarmItem.str_current_alarmPeriod = snooze_time.ToString();
                    alarmItem.assigned_alarmPeriod = snooze_time;
                    alarmItem.enabled = true;

                    ClockLib.ClockLib.snoozeUpdateTimer.Start();
                    ClockLib.ClockLib.stopSoundTimer.Start();
                    this.Close();
                }

                
            }
        }
    }
}
