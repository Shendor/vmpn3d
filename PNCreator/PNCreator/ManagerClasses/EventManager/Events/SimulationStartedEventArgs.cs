using PNCreator.ManagerClasses.Simulation;

namespace PNCreator.ManagerClasses.EventManager.Events
{
    public class SimulationStartedEventArgs : AbstractSimulationEventArgs
    {
        public SimulationStartedEventArgs(SimulationArgs simulationArgs) : base(simulationArgs)
        {
        }
    }
}
