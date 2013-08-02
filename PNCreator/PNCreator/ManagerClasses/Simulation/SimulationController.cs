using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Threading;
using PNCreator.Controls.Progress;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.ManagerClasses.FormulaManager;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.ManagerClasses.Simulation
{
    public class SimulationController
    {
        private readonly EventPublisher eventPublisher;
        private readonly ISimulator simulator;
        private ProgressWindow progressWindow;
        private Dictionary<long, double> initialObjectValues;
        protected SimulationArgs args;

        public SimulationController(Dispatcher mainDispatcher, ISimulator simulator)
        {
            eventPublisher = App.GetObject<EventPublisher>();
            this.simulator = simulator;
            args = new SimulationArgs
            {
                Dispatcher = mainDispatcher
            };
            initialObjectValues = new Dictionary<long, double>();
        }

        public double Timer
        {
            get
            {
                return args.Timer;
            }
            set
            {
                args.Timer = value;
            }
        }

        /// <summary>
        /// Get or set is thread need to be interrupted
        /// </summary>
        public bool IsInterrupt
        {
            get
            {
                return args.IsInterrupted;
            }
            set
            {
                args.IsInterrupted = value;
            }
        }

        public string SimulationName
        {
            get
            {
                return args.SimulationName;
            }
            set
            {
                args.SimulationName = value;
            }
        }

        public double EndSimulationTime
        {
            get
            {
                return args.EndSimulationTime;
            }
            set
            {
                args.EndSimulationTime = value;
            }
        }

        public void Start()
        {
            if (args.Thread != null && args.Thread.IsAlive)
                return;

            if (SimulationName == null || SimulationName.Equals(""))
                SimulationName = "Table" +
                                 PNProgramStorage.GetSimulationNames(PNObjectRepository.PNObjects.Values).Count;

            args.Thread = new Thread(RunSimulation);
            args.Thread.Start();
            IsInterrupt = false;
        }

        public void Stop()
        {
            if (args.Thread != null)
            {
                args.Thread.Abort();
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
            var formulaManager = App.GetObject<IFormulaManager>();

            var arcs = PNObjectRepository.GetPNObjects<Arc3D>();
            initialObjectValues = new Dictionary<long, double>();

//            PNProgramStorage.SimulationNumber++;
//            PNProgramStorage.SimulationNames.Add(simulationName);

            if (formulaManager.IsNeedToCompile)
            {
                eventPublisher.Register((ProgressEventArgs progressArgs) =>
                                        args.Dispatcher.BeginInvoke
                                            (DispatcherPriority.Normal,
                                             (ThreadStart) (() =>
                                                 {
                                                     if (progressWindow == null || !progressWindow.IsVisible)
                                                     {
                                                         progressWindow = new ProgressWindow
                                                             {
                                                                 Thread = args.Thread
                                                             };
                                                         progressWindow.ShowDialog();
                                                     }
                                                     progressWindow.Maximum = PNObjectRepository.Count;
                                                     progressWindow.Progress = progressArgs.Progress;
                                                 })));
            }
            try
            {
                formulaManager.GetObjectsWithFormula();
            }
            catch (FormatException)
            {
                args.Thread.Abort();
                throw;
            }

            foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
            {
                if (pnObject.Type != PNObjectTypes.Membrane)
                {
                    long id = pnObject.ID;
                    initialObjectValues[id] = PNObjectRepository.PNObjects.DoubleValues[id];

                    if (pnObject.AllowSaveHistory)
                    {
                        pnObject.AddNewHistoryTable(simulationName);
                    }

                    if (pnObject.Type == PNObjectTypes.DiscreteLocation ||
                        pnObject.Type == PNObjectTypes.ContinuousLocation)
                    {
                        var loc = (Location)pnObject;
                        foreach (Arc3D arc in arcs)
                        {
                            if (arc.EndID.Equals(id))
                                loc.IncomeArcsID.Add(arc.StartID, arc.ID);
                        }
                    }
                    else if (pnObject.Type == PNObjectTypes.DiscreteTransition ||
                             pnObject.Type == PNObjectTypes.ContinuousTransition)
                    {
                        var trans = (Transition)pnObject;
                        if (trans.Type == PNObjectTypes.DiscreteTransition)
                            ((DiscreteTransition)trans).DelayCounter = ((DiscreteTransition)trans).Delay;

                        foreach (Arc3D arc in arcs)
                        {
                            if (arc.EndID.Equals(id))
                                trans.IncomeArcsID.Add(arc.StartID, arc.ID);
                        }
                    }
                }
            }
            eventPublisher.ExecuteEvents(new ProgressEventArgs(PNObjectRepository.Count));
            formulaManager.IsNeedToCompile = false;
            eventPublisher.UnRegister(typeof(ProgressEventArgs));

            // Bad code. Refactor this method
//            args.Dispatcher.BeginInvoke
//                    (DispatcherPriority.Normal,
//                        (ThreadStart)(() =>
//                            {
//                                if (progressWindow != null)
//                                {
//                                    progressWindow.Close();
//                                    progressWindow = null;
//                                }
//                            }));
           
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

        protected virtual void RunSimulation()
        {
            ConfigureObjectsBeforeStart(SimulationName);
            args.Shapes = PNObjectRepository.GetPNObjects<Shape3D>();
            args.Arcs = PNObjectRepository.GetPNObjects<Arc3D>();
            args.HasDiscreteTransitions = PNObjectRepository.GetPNObjectsByType(PNObjectTypes.DiscreteTransition).Any();

            simulator.Run(args);

            ClearTemporaryData();

            /*   Random r = new Random();
               for (int i = 0; i <= 10000; i++)
               {
                   Thread.Sleep(2000);
                   int value = r.Next(0, 200);
                   eventPublisher.ExecuteEvents(new HistoryDataAddedEventArgs(value, i, value >= 100));
               }*/
        }

        #endregion
    }
}
