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
using System.CodeDom.Compiler;


namespace ClockPlus
{
    public static class NotificationSystem
    {
        public static List<NotificationItem> notifications = new List<NotificationItem>();

        public static int screenWidth, screenHeight;

        public static DispatcherTimer displayTimer;

        public static int ntfWindowWidth = 325;
        public static int ntfWindowHeight = 120;
        public static double leftOffset = 10;
        public static double topOffset = 5;
        public static double topStartOffset = 20;

        public static List<NotificationWindow> inactive_ntfWindows = new List<NotificationWindow>();
        public static List<NotificationWindow> active_ntfWindows = new List<NotificationWindow>();

        public static void CreateNotification(AlarmItemDef alarmItem, ClockNotificationType ntfType = ClockNotificationType.Alarm)
        {
            var window = new NotificationWindow();
            window.alarmItem = alarmItem;

            string title = "";
            string desc = "";

            bool hideSnooze = false;
            // if its a timer don't show snooze elements
            if (ntfType == ClockNotificationType.Timer)
            {
                hideSnooze = true;
                title = $"Timer ({alarmItem.title})";
                desc = $"'{alarmItem.alarmTime}' timer, {DateTime.Now.ToString("hh:mm tt")}";
            }
            else
            {
                title = $"Alarm ({alarmItem.title})";


                var dateType = alarmItem.dateType;
                if(dateType == "everyMonth")
                {
                    desc = $"It's a new month 🙂, {DateTime.Now.ToString("MMMM")}";
                    hideSnooze = true;
                } else if(dateType == "everyYear")
                {
                    desc = $"Happy new year 🙂, {DateTime.Now.ToString("yyyy")}";
                    hideSnooze = true;
                } else
                {
                    desc = $"{DateTime.Now.ToString("hh:mm:ss tt")}";
                    if(dateType == "specificDates" || dateType == "specificDate")
                    {
                        hideSnooze = true;
                    }
                }
                //specificDate
                //specificDates
            }

            if (hideSnooze)
            {
                window.cb_snoozeTime.Visibility = Visibility.Hidden;
                window.label_snooze.Visibility = Visibility.Hidden;
                window.cb_snoozeTime.IsEnabled = false;
                window.label_snooze.IsEnabled = false;

            }
            window.title.Content = title;
            window.desc.Content = desc;





                NotificationItem ntfItem = new NotificationItem
                {
                    window = window,
                    title = title,
                    desc = desc
                };

            window.Top = screenHeight;
            window.Left = screenWidth - ntfWindowWidth - leftOffset;
            window.Show();

            notifications.Add(ntfItem);


            // start timer
            if (!NotificationSystem.displayTimer.IsEnabled)
            {
                NotificationSystem.displayTimer.Start();
            }

            ClockLib.ClockLib.PlaySound(alarmItem.alarmSound);
        }
        public static void Initialize()
        {
            for(int i=2; i<=10; i += 3)
            {
                Debug.WriteLine(i.ToString());
            }

            // get screen bounds
            var bounds = Screen.PrimaryScreen.Bounds;

            // get screen width & height
            screenWidth = bounds.Width;
            screenHeight = bounds.Height;

            // setup display timer to update notification visual
            displayTimer = new DispatcherTimer() { Interval = new TimeSpan(0,0,0,0,10)};
            displayTimer.Tick += DisplayTimer_Tick;
     

        }


        private static void DisplayTimer_Tick(object? sender, EventArgs e)
        {
            
            int ntfCount = notifications.Count;

            // exit if there aren't any notifications
            if (ntfCount == 0) { return; }
            for (int i = 0; i < ntfCount; i++)
            {


                NotificationItem item = notifications[i];
                var window = item.window;
                if (window != null)
                {
                    int posIndex = i + 1;


                    double leftValue = screenWidth - ntfWindowWidth - leftOffset;
                    double topValue = screenHeight - topStartOffset - (ntfWindowHeight * posIndex) - (topOffset * posIndex);


                    double leftLerp = Lerp(window.Left, leftValue, 0.05);
                    double topLerp = Lerp(window.Top, topValue, 0.05);
                    window.Left = leftLerp;
                    window.Top = topLerp;
                   // window.Left = leftValue;
                    //window.Top = topValue;
                }
            }


        }

        public static double Lerp(double firstFloat, double secondFloat, double by)
        {
            return firstFloat * (1 - by) + secondFloat * by;


        }


        public class NotificationItem
        {
            public NotificationWindow window;
            public string title;
            public string desc;
        }
    }
}
