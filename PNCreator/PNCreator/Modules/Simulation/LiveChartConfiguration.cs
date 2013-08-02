
using System.Windows.Media;

namespace PNCreator.Modules.Simulation
{
    public class LiveChartConfiguration
    {
        public LiveChartConfiguration()
        {
            DataColor = Colors.OrangeRed;
            MinimumValue = -1;
            MaximumValue = 1;
        }

        public Color DataColor
        {
            get; set;
        }

        public double MinimumValue
        {
            get; set;
        }

        public double MaximumValue
        {
            get;
            set;
        }
    }
}
