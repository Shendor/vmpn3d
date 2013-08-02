using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.PNObjectsIerarchy;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using PNCreator.ManagerClasses.FormulaManager;
using PNCreator.ManagerClasses.Exception;
using PNCreator.ManagerClasses.Simulation;

namespace PNCreator.ManagerClasses
{
    public class PNSimulator
    {
        private EventPublisher eventPublisher;
        private readonly ISimulator simulator;
        private Thread thread;
        private readonly AnimationSimulationArgs args;
        private Dictionary<long, double> initialObjectValues;

        public PNSimulator(ISimulator simulator)
        {
            eventPublisher = App.GetObject<EventPublisher>();
            this.simulator = simulator;
            args = new AnimationSimulationArgs();
            initialObjectValues = new Dictionary<long, double>();
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
        /// Get or set is thread need to be interrupted
        /// </summary>
        public bool IsInterrupt
        {
            get;
            set;
        }

        /// <summary>
        /// Animation speed
        /// </summary>
        public double AnimationSpeed
        {
            get
            {
                return args.AnimationSpeed;
            }
            set
            {
                args.AnimationSpeed = value;
            }
        }

        public string SimulationName
        {
            get;
            set;
        }

        public double EndSimulationTime
        {
            get;
            set;
        }

        public void Start()
        {
            if (thread != null && thread.IsAlive.Equals(true))
                return;
            //mainWnd.showAnimPanelBtn.IsEnabled = false;

            if (SimulationName == null || SimulationName.Equals(""))
                SimulationName = "Table" +
                                 PNProgramStorage.GetSimulationNames(PNObjectRepository.PNObjects.Values).Count;

            thread = new Thread(RunSimulation);
            thread.Start();
            IsInterrupt = false;
        }

        public void Stop()
        {
            if (thread != null)
                thread.Abort();
        }

        /// <summary>
        /// Start or stop simulation
        /// </summary>
        public void SimulationLauncher(string simulationName, Player player)
        {
            switch (player)
            {
                case Player.Play:
                    {
                        if (thread != null && thread.IsAlive.Equals(true))
                            return;
//                        mainWnd.showAnimPanelBtn.IsEnabled = false;

                        if (simulationName.Equals(""))
                        {
                            simulationName = "Table" +
                                             PNProgramStorage.GetSimulationNames(PNObjectRepository.PNObjects.Values)
                                                             .Count;
                        }
                        SimulationName = simulationName;

                        thread = new Thread(RunSimulation);
                        thread.Start();
                        IsInterrupt = false;
                    }
                    break;
                case Player.Stop:
                    {
//                        mainWnd.showAnimPanelBtn.IsEnabled = true;
                        if (thread != null)
                            thread.Abort();
                    }
                    break;
            }

        }

        #region Configuration

        /// <summary>
        /// Restore objects values after finishing of simulation
        /// </summary>
        public void ReInitNet()
        {
            if (initialObjectValues == null || initialObjectValues.Count == 0)
                return;

            foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
            {
                long id = pnObject.ID;
                if (pnObject.Type == PNObjectTypes.DiscreteLocation)
                {
                    ((DiscreteLocation)pnObject).IncomeArcsID.Clear();
                    ((DiscreteLocation)pnObject).Tokens = (int)initialObjectValues[id];
                }
                else if (pnObject.Type == PNObjectTypes.ContinuousLocation)
                {
                    ((ContinuousLocation)pnObject).IncomeArcsID.Clear();
                    ((ContinuousLocation)pnObject).Level = initialObjectValues[id];
                }
                else if (pnObject.Type == PNObjectTypes.DiscreteTransition)
                {
                    ((DiscreteTransition)pnObject).IncomeArcsID.Clear();
                    ((DiscreteTransition)pnObject).DelayCounter = initialObjectValues[id];
                    ((DiscreteTransition)pnObject).Delay = initialObjectValues[id];
                }
                else if (pnObject.Type == PNObjectTypes.ContinuousTransition)
                {
                    ((ContinuousTransition)pnObject).IncomeArcsID.Clear();
                    ((ContinuousTransition)pnObject).Expectance = initialObjectValues[id];
                }
                else if (pnObject is Arc3D)
                {
                    ((Arc3D)pnObject).Weight = initialObjectValues[id];
                    ((Arc3D)pnObject).Thickness = Modules.Properties.PNProperties.ArcsThickness;
                }
                pnObject.ValueInCanvas.Text = initialObjectValues[id].ToString();
                pnObject.ResetMaterial();
                //updateUiHandler(this, null);
            }
        }

        /// <summary>
        /// Configure net before beginning of simulation process.
        /// 1) Make up arrays with objects' id which have formula
        /// 2) Save initial values of all objects
        /// 3) Check connections between locations and transitions (and backward)
        /// </summary>
        private void ConfigureObjectsBeforeStart(string simulationName)
        {
            //PNProgramStorage.IsNeedToCompile = false;  // !!! MUST BE REMOVED !!!
            IFormulaManager formulaManager = App.GetObject<IFormulaManager>();

            int progress = 0;
            IEnumerable<Arc3D> arcs = PNObjectRepository.GetPNObjects<Arc3D>();
            initialObjectValues = new Dictionary<long, double>();

//            PNProgramStorage.SimulationNumber += 1;
//            PNProgramStorage.SimulationNames.Add(simulationName);

//            mainWnd.Dispatcher.BeginInvoke
//               (DispatcherPriority.Normal,
//               (ThreadStart)(() =>
//                   {
//                   mainWnd.simWnd.configurePanel.Visibility = System.Windows.Visibility.Visible;
//                   mainWnd.simWnd.configuringProgress.Minimum = 0;
//                   mainWnd.simWnd.configuringProgress.Maximum = PNObjectRepository.PNObjects.Count;
//                   }));

            eventPublisher.ExecuteEvents(new ProgressEventArgs(0)
                {
                    MaximumProgress = PNObjectRepository.Count
                });

            try
            {
                formulaManager.GetObjectsWithFormula();
            }
            catch (FormatException e)
            {
//                mainWnd.Dispatcher.BeginInvoke
//                    (DispatcherPriority.Normal,
//                    (ThreadStart)(() => ExceptionHandler.HandleException(e)));
                thread.Abort();
            }

            foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
            {
                long id = pnObject.ID;
                initialObjectValues[id] = PNObjectRepository.PNObjects.DoubleValues[id];

                if (pnObject.AllowSaveHistory)
                {
                    pnObject.AddNewHistoryTable(simulationName);
                }

                if (pnObject.Type == PNObjectTypes.DiscreteLocation || pnObject.Type == PNObjectTypes.ContinuousLocation)
                {
                    Location loc = (Location)pnObject;
                    foreach (Arc3D arc in arcs)
                    {
                        if (arc.EndID.Equals(id))
                            loc.IncomeArcsID.Add(arc.StartID, arc.ID);
                    }
                }
                else if (pnObject.Type == PNObjectTypes.DiscreteTransition || pnObject.Type == PNObjectTypes.ContinuousTransition)
                {
                    Transition trans = (Transition)pnObject;
                    if (trans.Type == PNObjectTypes.DiscreteTransition)
                        ((DiscreteTransition)trans).DelayCounter = ((DiscreteTransition)trans).Delay;

                    foreach (Arc3D arc in arcs)
                    {
                        if (arc.EndID.Equals(id))
                            trans.IncomeArcsID.Add(arc.StartID, arc.ID);
                    }
                }

//                mainWnd.Dispatcher.BeginInvoke
//                   (DispatcherPriority.ApplicationIdle,
//                   (ThreadStart)(() =>
//                       {
//                       if (!progress.Equals(id))
//                           mainWnd.simWnd.configuringProgress.Value = progress;
//                       }));
                if (!progress.Equals(id))
                {
                    eventPublisher.ExecuteEvents(new ProgressEventArgs(progress));
                }

                progress++;

            }

            formulaManager.IsNeedToCompile = false;
        }

        /// <summary>
        /// Clear some arrays which were filled in the method Window1.ConfigureObjectsBeforeStart(). 
        /// Must be run after finishing of the simulation process 
        /// </summary>
        private void ClearTemporaryData()
        {
            foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
            {
                if (pnObject.Type == PNObjectTypes.DiscreteLocation ||
                    pnObject.Type == PNObjectTypes.ContinuousLocation)
                {
                    ((Location)pnObject).IncomeArcsID.Clear();
                }
                else if (pnObject.Type == PNObjectTypes.DiscreteTransition ||
                    pnObject.Type == PNObjectTypes.ContinuousTransition)
                {
                    ((Transition)pnObject).IncomeArcsID.Clear();
                }
            }
        }

        #endregion

        #region Simulator

        /// <summary>
        /// Run simulation
        /// </summary>
        private void RunSimulation()
        {
            ConfigureObjectsBeforeStart(SimulationName);
//            args.WindowOwner = mainWnd;
            args.Timer = Timer;
            args.EndSimulationTime = EndSimulationTime;
            args.IsInterrupted = IsInterrupt;
            args.Shapes = PNObjectRepository.GetPNObjects<Shape3D>();
            args.Arcs = PNObjectRepository.GetPNObjects<Arc3D>();
            args.HasDiscreteTransitions = PNObjectRepository.GetPNObjectsByType(PNObjectTypes.DiscreteTransition).Any();
            args.SimulationName = SimulationName;
            args.AnimationSpeed = AnimationSpeed;
            args.Thread = thread;

            simulator.Run(args);

            ClearTemporaryData();
        }

        #endregion

    }

}
