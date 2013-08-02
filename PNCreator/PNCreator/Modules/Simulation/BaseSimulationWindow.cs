using System.Windows;
using PNCreator.ManagerClasses;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.ManagerClasses.Simulation;
using PNCreator.Modules.Main;

namespace PNCreator.Modules.Simulation
{
    public abstract class BaseSimulationWindow : BaseWindow
    {
        protected SimulationController simulationController;
        protected EventPublisher eventPublisher;

        protected BaseSimulationWindow()
        {
            eventPublisher = App.GetObject<EventPublisher>();
            Owner = Application.Current.MainWindow;
            App.GetObject<PNObjectManager>().IsNotReadonly = false;
            Unloaded += WindowUnloaded;
        }

        private void WindowUnloaded(object sender, RoutedEventArgs e)
        {
            App.GetObject<PNObjectManager>().IsNotReadonly = true;
            StopSimulation();
            if (simulationController != null) // Hack
                simulationController.ReInitNet();

            eventPublisher.UnRegister(typeof(SimulationProgressEventArgs));
            eventPublisher.UnRegister(typeof(SimulationFinishedEventArgs));
        }


        public string SimulationName
        {
            get; set;
        }

        protected abstract void StartSimulation();

        protected abstract void StopSimulation();
    }
}
