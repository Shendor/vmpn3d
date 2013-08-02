
using PNCreator.ManagerClasses.Simulation;

namespace PNCreator.ManagerClasses.EventManager.Events
{
    public class SimulationProgressEventArgs : AbstractSimulationEventArgs
    {
        public SimulationProgressEventArgs(SimulationArgs simulationArgs) : base(simulationArgs)
        {
        }
    }
}
