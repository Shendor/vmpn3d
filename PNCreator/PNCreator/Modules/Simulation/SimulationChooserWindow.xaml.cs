using System.Windows;
using System.Windows.Controls;
using PNCreator.ManagerClasses;

namespace PNCreator.Modules.Simulation
{
    public partial class SimulationChooserWindow
    {
        private readonly WindowsFactory windowsFactory;

        public SimulationChooserWindow()
        {
            InitializeComponent();

            windowsFactory = App.GetObject<WindowsFactory>();
        }

        private void ButtonsClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button) e.OriginalSource;

            BaseSimulationWindow window = null;
            if (btn.Equals(animationBtn))
            {
                window = windowsFactory.GetWindow<AnimationSimulationWindow>();
                
            }
            else if (btn.Equals(noAnimationBtn))
            {
                window = windowsFactory.GetWindow <SimulationWindow>();
            }

            if (window != null)
            {
                window.SimulationName = simulationNameTextBox.Text;
                window.Show();
                Close();
            }

        }
    }
}
