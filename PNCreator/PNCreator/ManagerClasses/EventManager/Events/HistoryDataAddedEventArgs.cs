using System;

namespace PNCreator.ManagerClasses.EventManager.Events
{
    public class HistoryDataAddedEventArgs : AbstractEventArgs
    {

        public HistoryDataAddedEventArgs(double value, double time)
        {
            DateTime today = DateTime.Now;

            Value = value;
            Time = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0).Add(TimeSpan.FromSeconds(time));
        }

        public double Value
        {
            get;
            set;
        }

        public DateTime Time
        {
            get;
            set;
        }
    }
}
