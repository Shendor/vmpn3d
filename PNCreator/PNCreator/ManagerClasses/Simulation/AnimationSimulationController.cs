using System.Windows.Threading;

namespace PNCreator.ManagerClasses.Simulation
{
    public class AnimationSimulationController : SimulationController
    {
        public AnimationSimulationController(Dispatcher mainDispatcher, ISimulator simulator) : base(mainDispatcher, simulator)
        {
            args = new AnimationSimulationArgs
            {
                Dispatcher = mainDispatcher
            };
        }

        public double AnimationSpeed
        {
            get
            {
                return ((AnimationSimulationArgs)args).AnimationSpeed;
            }
            set
            {
                ((AnimationSimulationArgs)args).AnimationSpeed = value;
            }
        }
    }
}
