using System.Threading;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;

namespace PNCreator.Modules.Simulation
{
    public partial class SimulationPlayerToolbar {

        public SimulationPlayerToolbar()
        {
            InitializeComponent();
            App.GetObject<EventPublisher>().Register((SimulationFinishedEventArgs args) =>
              Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)(() => EnablePlay(true))));
        }

        public StartSimulationDelegate StartSimulation
        {
            get; set;
        }

        public StopSimulationDelegate StopSimulation
        {
            get;
            set;
        }

        public void EnablePlay()
        {
            playBtn.IsEnabled = true;
        }

        public void DisablePlay()
        {
            playBtn.IsEnabled = false;
        }

        public void EnableStop()
        {
            stopBtn.IsEnabled = true;
        }

        public void DisableStop()
        {
            stopBtn.IsEnabled = false;
        }

        private void ButtonsClick(object sender, RoutedEventArgs e)
        {
            var btn = (ButtonBase)e.OriginalSource;

            if (btn.Equals(playBtn))
            {
                StartSimulation();
                EnablePlay(false);
            }
            else if (btn.Equals(stopBtn))
            {
                StopSimulation();
                EnablePlay(true);
            }
        }

        private void EnablePlay(bool isEnabled)
        {
            playBtn.IsEnabled = isEnabled;
            stopBtn.IsEnabled = !isEnabled;
        }

        public delegate void StartSimulationDelegate();
        public delegate void StopSimulationDelegate();
    }
}
