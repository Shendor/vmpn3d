using System;
using System.Threading;
using System.Windows.Threading;
using PNCreator.ManagerClasses;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.ManagerClasses.Simulation;
using PNCreator.PNObjectsIerarchy;
using PNCreator.Properties;
using WindowsControl;

namespace PNCreator.Modules.Simulation
{
    public partial class SimulationWindow
    {
        public SimulationWindow()
        {
            InitializeComponent();

            eventPublisher.Register((SimulationProgressEventArgs args) => 
                Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, 
                (ThreadStart)(() =>
                {
                    try
                    {
                        args.SimulationArgs.EndSimulationTime = endTimeNumericBox.Value;
                        simulationProgressBar.Maximum = args.SimulationArgs.EndSimulationTime;
                        simulationProgressBar.Value = args.SimulationArgs.Timer;
                    }
                    catch (FormatException)
                    {
                        args.SimulationArgs.Thread.Abort();
                    }
                })));

            eventPublisher.Register((SimulationFinishedEventArgs args) =>
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    {
                        simulationProgressBar.Value = endTimeNumericBox.Value;

                        foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
                        {
                            string valueInCanvas = PNObjectRepository.PNObjects.DoubleValues[pnObject.ID].ToString();

                            if (pnObject.Type == PNObjectTypes.DiscreteLocation ||
                                pnObject.Type == PNObjectTypes.DiscreteTransition ||
                                pnObject.Type == PNObjectTypes.DiscreteArc ||
                                pnObject.Type == PNObjectTypes.DiscreteInhibitorArc ||
                                pnObject.Type == PNObjectTypes.DiscreteTestArc)
                            {
                                pnObject.ValueInCanvas.Text = valueInCanvas;
                            }
                            else
                            {
                                pnObject.ValueInCanvas.Text = String.Format("{0:F3}", valueInCanvas);
                            }
                        }
                        StopSimulation();
                        DialogWindow.Alert(Messages.Default.SimulationFinished);
                    });
                });
        }

        protected override void StartSimulation()
        {
            simulationController = new SimulationController(Dispatcher, new Simulator());
            simulationController.EndSimulationTime = endTimeNumericBox.Value;
            simulationController.Start();
        }

        protected override void StopSimulation()
        {
            if (simulationController != null)
                simulationController.Stop();
        }

    }
}
