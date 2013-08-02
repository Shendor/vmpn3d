using PNCreator.ManagerClasses.Simulation;

namespace PNCreator.ManagerClasses.EventManager.Events
{
    public class SimulationFinishedEventArgs : AbstractSimulationEventArgs
    {
        public SimulationFinishedEventArgs(SimulationArgs simulationArgs) : base(simulationArgs)
        {
        }
    }
}
