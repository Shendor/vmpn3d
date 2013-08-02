using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PNCreator.ManagerClasses;
using PNCreator.PNObjectsIerarchy;
using System.Threading;
using Telerik.Windows.Controls.Charting;
using PNCreator.ManagerClasses.Simulation;

namespace PNCreator.Modules.Simulation
{
    public partial class Simulation 
    {
        private PNSimulator pnSimulator;
        private Thread thread;
        private PNObject selectedObject;

        public Simulation()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Load objects names which are able to save history
        /// </summary>
        public void LoadObjectsNameWithHistory()
        {
            objectsList.Items.Clear();
            foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
                if (pnObject.AllowSaveHistory == true)
                    objectsList.Items.Add(pnObject);
        }

        private void Buttons_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.OriginalSource as Button;
            switch (btn.Name)
            {
                case "minimizeBtn":
                    this.WindowState = System.Windows.WindowState.Minimized;
                    break;
                case "playBtn":
                    RunSimulation();
                    break;
                case "stopBtn":
                    StopSimulation();
                    break;
                case "backBtn":
                    ReturnPanelsState();
                    break;
                case "closeBtn":
                    Close();
                    break;
            }
        }

        private void RunSimulation()
        {
            objectsList.IsEnabled = true;
            if (aRBtn.IsChecked == true)
                pnSimulator = new PNSimulator(new AnimationSimulator());

            if (naRBtn.IsChecked == true)
                pnSimulator = new PNSimulator(new Simulator());

            pnSimulator.AnimationSpeed = timeSpeedSlider.Value;
            pnSimulator.SimulationLauncher(simulationNameTB.Text, Player.Play);
        }

        private void StopSimulation()
        {
            objectsList.IsEnabled = false;
            if (thread != null)
                thread.Abort();
            if (pnSimulator != null)
                pnSimulator.SimulationLauncher(simulationNameTB.Text, Player.Stop);
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }

        private void naRBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButtons_Click(object sender, RoutedEventArgs e)
        {
            toolbar1.Visibility = System.Windows.Visibility.Collapsed;
            toolbar2.Visibility = System.Windows.Visibility.Visible;

            RadioButton rBtn = e.OriginalSource as RadioButton;

            switch (rBtn.Name)
            {
                case "aRBtn":
                    ShowAnimationPanels();
                    break;
                case "naRBtn":
                    ShowNoAnimationPanels();
                    break;
            }
        }
       
        private void ShowAnimationPanels()
        {
            textPanel.Visibility = System.Windows.Visibility.Collapsed;
            aPanel.Visibility = System.Windows.Visibility.Visible;
            naPanel.Visibility = System.Windows.Visibility.Collapsed;
            objectsList.Visibility = System.Windows.Visibility.Visible;
        }
        private void ShowNoAnimationPanels()
        {
            textPanel.Visibility = System.Windows.Visibility.Collapsed;
            aPanel.Visibility = System.Windows.Visibility.Collapsed;
            naPanel.Visibility = System.Windows.Visibility.Visible;
            objectsList.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void ReturnPanelsState()
        {
            textPanel.Visibility = System.Windows.Visibility.Visible;

            aPanel.Visibility = System.Windows.Visibility.Collapsed;
            naPanel.Visibility = System.Windows.Visibility.Collapsed;

            toolbar1.Visibility = System.Windows.Visibility.Visible;
            toolbar2.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void simulationWnd_Loaded(object sender, RoutedEventArgs e)
        {
            LoadObjectsNameWithHistory();
            ConfigureChart();
        }

        private void ConfigureChart()
        {
            LineSeriesDefinition lineSeries = new LineSeriesDefinition();
            lineSeries.ShowItemLabels = false;
            lineSeries.ShowPointMarks = false;
            lineSeries.Appearance.Stroke = Brushes.Orange;

            SeriesMapping mapping1 = new SeriesMapping();
            mapping1.SeriesDefinition = lineSeries;
            ItemMapping im1 = new ItemMapping("Value", DataPointMember.YValue);
            im1.FieldType = typeof(double);
            mapping1.ItemMappings.Add(im1);

            ItemMapping im2 = new ItemMapping("Time", DataPointMember.XValue);
            im2.FieldType = typeof(double);
            mapping1.ItemMappings.Add(im2);
            mapping1.LegendLabel = "Live chart";

            liveChart.SeriesMappings.Add(mapping1);
            liveChart.DefaultView.ChartArea.ZoomScrollSettingsX.ScrollMode = ScrollMode.ScrollAndZoom;
            liveChart.DefaultView.ChartArea.ZoomScrollSettingsY.ScrollMode = ScrollMode.ScrollAndZoom;
            liveChart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeEnd = 1;

            liveChart.DefaultView.ChartArea.AxisX.AutoRange = false;
            liveChart.DefaultView.ChartArea.AxisX.AddRange(0, 1, 1);
            //RadChart1.SamplingSettings.SamplingThreshold = 100;
            liveChart.DefaultView.ChartArea.EnableAnimations = false;

            liveChart.DefaultView.ChartArea.AxisX.Title = "Time";
            liveChart.DefaultView.ChartArea.AxisY.Title = "Values";
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (pnSimulator != null)
                pnSimulator.AnimationSpeed = timeSpeedSlider.Value;
        }

        private void objectsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (objectsList.SelectedItem == null)
                return;
            selectedObject = (PNObject)objectsList.SelectedItem;

            if (selectedObject != null && selectedObject.ObjectHistory.Count > 0)
            {
                if (thread != null)
                    thread.Abort();

                thread = new Thread(LoadDataToChartInLive);
                thread.Start();
            }
        }

        private void LoadDataToChartInLive()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2 * pnSimulator.AnimationSpeed));
            string lastTable = pnSimulator.SimulationName;
            int rowsCount = 0;
            double maxXValue = 1;
            int step = 50;

            while (true)
            {
                //Exception: The given key was not present in the dictionary.
                rowsCount = selectedObject.ObjectHistory[lastTable].Rows.Count;
                maxXValue = Convert.ToDouble(selectedObject.ObjectHistory[lastTable].Rows[rowsCount - 1][0]);

                Thread.Sleep(TimeSpan.FromSeconds(pnSimulator.AnimationSpeed));
                this.Dispatcher.BeginInvoke
                   (System.Windows.Threading.DispatcherPriority.ApplicationIdle,
                   (ThreadStart)delegate()
                   {
                       liveChart.ItemsSource = null;
                       liveChart.ItemsSource = selectedObject.ObjectHistory[lastTable]; // Exception ?

                       liveChart.DefaultView.ChartArea.AxisX.AddRange(0, maxXValue + 0.1, maxXValue * 0.1);
                       if (rowsCount > step)
                       {
                           if (!(liveChart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeEnd <= 0.1))
                               liveChart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeEnd -= 0.05;
                           step += 50;
                       }
                       liveChart.SamplingSettings.SamplingThreshold = (int)maxXValue;

                       //liveChart.DefaultView.ChartArea.AxisX.AddRange(0, rowsCount, rowsCount * 0.1);
                       //if (rowsCount > step)
                       //{
                       //    if (!(liveChart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeEnd <= 0.1))
                       //        liveChart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeEnd -= 0.05;
                       //    step += 50;
                       //}
                       //liveChart.SamplingSettings.SamplingThreshold = rowsCount;
                   });
                Thread.Sleep(TimeSpan.FromSeconds(pnSimulator.AnimationSpeed));
            }

        }

        private void simulationWnd_Unloaded(object sender, RoutedEventArgs e)
        {
            StopSimulation();
            if (pnSimulator != null) // Hack
                pnSimulator.ReInitNet();
        }

    }
}
