using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using PNCreator.ManagerClasses;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.ManagerClasses.Simulation;
using PNCreator.PNObjectsIerarchy;
using PNCreator.Properties;
using WindowsControl;

namespace PNCreator.Modules.Simulation
{
    public partial class AnimationSimulationWindow
    {
        private LiveChartConfiguration configuration;
        private AnimationSimulationController animationSimulationController;
        private PNObject selectedObject;
        private Thread chartThread;
        private readonly LiveChartDataViewModel<LiveChartData> chartModel;
        private bool? isGoodAdded;

        public AnimationSimulationWindow()
        {
            InitializeComponent();
            chartModel = new LiveChartDataViewModel<LiveChartData>();
            liveChart.DataContext = chartModel;
            LoadObjectsNameWithHistory();

            eventPublisher.Register((HistoryDataAddedEventArgs args) => 
                Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                                (ThreadStart) delegate
                                    {
                                        if (args.Source.Equals(selectedObject))
                                        {
                                            var liveChartData = new LiveChartData(args.Value, args.Time);
                                            if (configuration != null && args.Value >= configuration.MinimumValue &&
                                                args.Value <= configuration.MaximumValue)
                                            {
                                                if (isGoodAdded == true)
                                                {
                                                    chartModel.AddGoodData(liveChartData);
                                                }
                                                chartModel.AddBadData(liveChartData);
                                                chartModel.AddGoodData(new LiveChartData(null, args.Time));
                                                isGoodAdded = false;
                                            }
                                            else
                                            {
                                                if (isGoodAdded == false)
                                                {
                                                    chartModel.AddBadData(liveChartData);
                                                }
                                                chartModel.AddGoodData(liveChartData);
                                                chartModel.AddBadData(new LiveChartData(null, args.Time));
                                                isGoodAdded = true;
                                            }
                                        }
                                    }));

            eventPublisher.Register((SimulationProgressEventArgs args) =>
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                        (ThreadStart)delegate
                        {
                            timerTextBlock.Text = args.SimulationArgs.Timer.ToString();
                        });
                });

            eventPublisher.Register((SimulationFinishedEventArgs args) =>
                Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    (ThreadStart)delegate
                        {
                            foreach (Arc3D arc in args.SimulationArgs.Arcs)
                                arc.Thickness = Properties.PNProperties.ArcsThickness;
                            //ClearDelayValue();
                            StopSimulation();
                            DialogWindow.Alert(Messages.Default.SimulationFinished);
                        }));

            Unloaded += (sender, args) => eventPublisher.UnRegister(typeof (HistoryDataAddedEventArgs));
        }

        public void LoadObjectsNameWithHistory()
        {
            objectsList.Items.Clear();
            foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
                if (pnObject.AllowSaveHistory)
                    objectsList.Items.Add(pnObject);
        }

        private void ObjectsListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedObject = objectsList.SelectedItem as PNObject;
            chartModel.ClearData();
        }

        private void SpeedValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (simulationController != null)
                ((AnimationSimulationController)simulationController).AnimationSpeed = speedSlider.Value;
        }

        protected override void StartSimulation()
        {
            editChartBtn.IsEnabled = false;
            objectsList.IsEnabled = true;
            simulationController = new AnimationSimulationController(Dispatcher, new AnimationSimulator())
                {
                    SimulationName = base.SimulationName
                };
            animationSimulationController = (AnimationSimulationController)simulationController;
            animationSimulationController.AnimationSpeed = speedSlider.Value;

            simulationController.Start();
            progressBar.IsIndeterminate = true;
        }

        protected override void StopSimulation()
        {
            editChartBtn.IsEnabled = true;
            objectsList.IsEnabled = false;
            progressBar.IsIndeterminate = false;
            if (chartThread != null)
                chartThread.Abort();
            if (simulationController != null)
                simulationController.Stop();
        }

        private void EditButtonClicked(object sender, RoutedEventArgs e)
        {
            EditLiveChartWindow editChartWindow = App.GetObject<WindowsFactory>().GetWindow<EditLiveChartWindow>();
            if (configuration != null)
            {
                editChartWindow.LiveChartConfiguration = configuration;
            }
            editChartWindow.ShowDialog();

            configuration = editChartWindow.LiveChartConfiguration;
        }
    }
}
