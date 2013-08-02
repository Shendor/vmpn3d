using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Data;
using PNCreator.ManagerClasses;
using WindowsControl;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Modules.Analizing
{
    public partial class Chart
    {
        public Chart()
        {
            InitializeComponent();

            LoadObjectsToList();
        }

        #region Events

        private void ObjectsListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = e.OriginalSource as ComboBox;

            if (cb.Equals(axisXCB))
            {
                LoadSimulationNames(cb, simulationXList);
            } else if (cb.Equals(axisYCB))
            {
                LoadSimulationNames(cb, simulationYList);
            }

        }

        private void LoadSimulationNames(ComboBox pnObjectsCB, ComboBox simulationNamesCB)
        {
            var selectedPNObject = (PNObject)pnObjectsCB.SelectedItem;
            if (selectedPNObject != null)
            {
                simulationNamesCB.Items.Clear();

                foreach (string name in selectedPNObject.ObjectHistory.Keys)
                    simulationNamesCB.Items.Add(name);

                simulationNamesCB.SelectedIndex = 0;
            }
        }

        private void ButtonsClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.OriginalSource;

            if (btn.Equals(addChartBtn))
            {
                BuildChart();
            } else if (btn.Equals(printBtn))
            {
                PrintChart();
            } else if (btn.Equals(exportBtn))
            {
                ExportChart();
            } 
        }

        #endregion

        #region Methods

        private void LoadObjectsToList()
        {
            foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
            {
                if (pnObject.AllowSaveHistory)
                {
                    axisXCB.Items.Add(pnObject);
                    axisYCB.Items.Add(pnObject);
                }
            }
            if (axisXCB.Items.Count == 0 && axisYCB.Items.Count == 0)
            {
                addChartBtn.IsEnabled = false;
            }

        }

        private void BuildChart()
        {
            if (simulationXList.SelectedItem != null && simulationYList.SelectedItem != null &&
                axisXCB.SelectedItem != null && axisYCB.SelectedItem != null)
            {
                var objectAxisX = (PNObject) axisXCB.SelectedItem;
                var objectAxisY = (PNObject) axisYCB.SelectedItem;

                string simulationNameX = simulationXList.SelectedItem.ToString();
                string simulationNameY = simulationYList.SelectedItem.ToString();

                if (objectAxisX.ObjectHistory[simulationNameX].Rows.Count !=
                    objectAxisY.ObjectHistory[simulationNameY].Rows.Count)
                {
                    DialogWindow.Alert("Rows count are not equal");
                    return;
                }

                var chartPoints = new PointCollection();

                double[] xArray = GetObjectData(objectAxisX.ObjectHistory[simulationNameX].Rows,
                                                valueXList.SelectedIndex);
                double[] yArray = GetObjectData(objectAxisY.ObjectHistory[simulationNameY].Rows,
                                                valueYList.SelectedIndex);

                for (int i = 0; i < xArray.Length; i++)
                {
                    chartPoints.Add(new Point(xArray[i], yArray[i]));
                }

                chart.BuildChart(chartPoints);
                chart.AxisXLabel = axisXCB.SelectedItem.ToString();
                chart.AxisYLabel = axisYCB.SelectedItem.ToString();
            }
        }

        private double[] GetObjectData(DataRowCollection rows, int column)
        {
            var array = new double[rows.Count];
            for (int i = 0; i < rows.Count; i++)
            {
                array[i] = Math.Round((double)rows[i][column], 4, MidpointRounding.AwayFromZero);
            }
            return array;
        }

        private void PrintChart()
        {
            DialogBoxes.PrintContent(chart);
        }

        private void ExportChart()
        {
            string filename = DialogBoxes.SaveDialog(null, DocumentFormat.Image);
            if (filename != null)
                ContentExporter.ExportVisualContent(filename, (int)ActualWidth, (int)ActualHeight, chart);
        }
        #endregion


        #region Commands
        private void OnWindowClose(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Windows.Shell.SystemCommands.CloseWindow(this);
        }

        private void OnWindowMaximize(object sender, ExecutedRoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                Microsoft.Windows.Shell.SystemCommands.MaximizeWindow(this);
        }

        private void OnWindowMinimize(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Windows.Shell.SystemCommands.MinimizeWindow(this);
        }
        #endregion
    }
}
