using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockLib
{
    public class ClockControllerDef
    {
        public virtual void AddAlarm(string _title, string _alarmTime, string _dateType = "default", string _dateValue = "", bool _enabled = true, string _current_alarmPeriod = "", string _alarmSound = "default", List<ClockCommand> clockCommands = null)
        {
            ClockLib.AddAlarm(_title, _alarmTime, _dateType, _dateValue, _enabled, _current_alarmPeriod, _alarmSound, clockCommands);

            ClockLib.Update();

            //UpdateDisplay();
           
        }
        public virtual void RemoveAlarm(int alarm_index)
        {
            ClockLib.alarmDefs.Remove(alarm_index);
            ClockLib.Update();
        }
        public virtual void RemoveTimer(int timer_index)
        {
            ClockLib.timerDefs.Remove(timer_index);
            ClockLib.Update();
        }

        public virtual void AddTimer(string _title, string _timeSpan, string _alarmSound, List<ClockCommand> clockCommands = null)
        {
            ClockLib.AddTimer(_title, _timeSpan, _alarmSound, clockCommands);
            ClockLib.Update();
        }
        public virtual void UpdateDisplay() { }
        public virtual void UpdateDisplayTime() { }

    }
}
