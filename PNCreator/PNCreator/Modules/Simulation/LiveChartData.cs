using System;

namespace PNCreator.Modules.Simulation
{
    public class LiveChartData : ChartData
    {
        private DateTime time;

        public LiveChartData()
        {
        }

        public LiveChartData(double? value, DateTime time)
        {
            this.value = value;
            this.time = time;
        }

        public DateTime Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
            }
        }
    }
}
