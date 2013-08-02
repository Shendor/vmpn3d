using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using PNCreator.PNObjectsIerarchy;
using System.Threading;

namespace PNCreator.ManagerClasses.Simulation
{
    public class SimulationArgs
    {

        public SimulationArgs()
        {
            ActiveTransitionQuantity = 0;
            Timer = 0;
            MinimumTimeInterval = 1;
            HasDiscreteTransitions = true;
        }

        public SimulationArgs(Window windowOwner)
        {
            WindowOwner = windowOwner;
        }

        /// <summary>
        /// Window owner
        /// </summary>
        public Window WindowOwner
        {
            get;
            set;
        }

        public Dispatcher Dispatcher
        {
            get; set;
        }
        /// <summary>
        /// Get or set how many transitions are active
        /// </summary>
        public int ActiveTransitionQuantity
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set timer
        /// </summary>
        public double Timer
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set end simulation time
        /// </summary>
        public double EndSimulationTime
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set minimum time interval
        /// </summary>
        public double MinimumTimeInterval
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set arcs collection
        /// </summary>
        public IEnumerable<Arc3D> Arcs
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set shapes collection
        /// </summary>
        public IEnumerable<Shape3D> Shapes
        {
            get;
            set;
        }

        /// <summary>
        /// Simulation name
        /// </summary>
        public string SimulationName
        {
            get;
            set;
        }

        /// <summary>
        /// Active thread
        /// </summary>
        public Thread Thread
        {
            get;
            set;
        }

        /// <summary>
        /// Is thred interrupted
        /// </summary>
        public bool IsInterrupted
        {
            get;
            set;
        }

        /// <summary>
        /// Defines whether there are discrete transitions in the model.
        /// It's used in simulation to skip\run Third step
        /// </summary>
        public bool HasDiscreteTransitions
        {
            get;
            set;
        }
    }
}
