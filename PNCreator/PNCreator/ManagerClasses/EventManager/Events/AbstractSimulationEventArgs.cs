
using PNCreator.ManagerClasses.Simulation;

namespace PNCreator.ManagerClasses.EventManager.Events
{
    public abstract class AbstractSimulationEventArgs : AbstractEventArgs
    {
        protected AbstractSimulationEventArgs(SimulationArgs simulationArgs)
        {
            SimulationArgs = simulationArgs;
        }

        public SimulationArgs SimulationArgs
        {
            get; set;
        }
    }
}
