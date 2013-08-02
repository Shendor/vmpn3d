using System.Collections.Generic;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.ManagerClasses.Simulation
{
    public class AnimationSimulationArgs : SimulationArgs
    {
        public AnimationSimulationArgs()
        {
            Tokens = new List<Token>();
        }

        /// <summary>
        /// Get or set Animation speed
        /// </summary>
        public double AnimationSpeed
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set Tokens collection
        /// </summary>
        public List<Token> Tokens
        {
            get;
            set;
        }
    }
}
