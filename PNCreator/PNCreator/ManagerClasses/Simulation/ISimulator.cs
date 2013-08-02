using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNCreator.ManagerClasses.Simulation
{
    public interface ISimulator
    {
        /// <summary>
        /// Run simulation
        /// </summary>
        void Run(SimulationArgs args);

        /// <summary>
        /// SimulationArgsChanged event
        /// </summary>
//        event SimulationArgsChangedHandler SimulationArgsChanged;
    }
}
