using System.Windows;

namespace PNCreator.Modules.Simulation
{
    public partial class EditLiveChartWindow
    {
        private LiveChartConfiguration liveChartConfiguration;

        public EditLiveChartWindow()
        {
            InitializeComponent();
            liveChartConfiguration = new LiveChartConfiguration();
            DataContext = liveChartConfiguration;
        }

        public LiveChartConfiguration LiveChartConfiguration
        {
            get { return liveChartConfiguration; }
            set 
            { 
                liveChartConfiguration = value;
                DataContext = liveChartConfiguration;
            }
        }

        private void OtherButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
