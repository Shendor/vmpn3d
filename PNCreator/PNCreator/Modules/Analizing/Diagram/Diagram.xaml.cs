using System;
using System.Data;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Charting;
using PNCreator.ManagerClasses;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Modules.Analizing
{
    public partial class Diagram
    {
        private Chart2D chart2D;
        private int totalColumns;
        private HistoryExport expWnd;
        private DataTable briefHistoryDataTable;
        private PNObject selectedObject;
        private List<string> simulationNames; 

        public Diagram()
        {
            InitializeComponent();
         
            simulationNames = PNProgramStorage.GetSimulationNames(PNObjectRepository.PNObjects.Values);
            totalColumns = simulationNames.Count;
            InitializeBriefTable();
        }

        #region Chart configuration

        private void ConfigureLineChart()
        {
            chart2D = new Chart2D();
            chartPanel.Children.Clear();
            chartPanel.Children.Add(chart2D);
            int rowsCount = historyTable.SelectedItems.Count;

            LineSeriesDefinition lineSeries = new LineSeriesDefinition();
            lineSeries.ShowItemLabels = false;
            lineSeries.ShowPointMarks = false;
           
            SeriesMapping dataMapping = new SeriesMapping();
            dataMapping.SeriesDefinition = lineSeries;
            dataMapping.ItemMappings.Add(new ItemMapping("XValue", DataPointMember.XValue));
            dataMapping.ItemMappings.Add(new ItemMapping("YValue", DataPointMember.YValue));
            dataMapping.ItemMappings[1].SamplingFunction = ChartSamplingFunction.KeepExtremes;

            chart2D.SeriesMappings.Add(dataMapping);

            chart2D.DefaultView.ChartArea.ZoomScrollSettingsX.ScrollMode = ScrollMode.ScrollAndZoom;
            chart2D.DefaultView.ChartArea.ZoomScrollSettingsY.ScrollMode = ScrollMode.ScrollAndZoom;

            //chart2D.DefaultView.ChartArea.AxisY.AutoRange = false;
            //chart2D.DefaultView.ChartArea.AxisY.AddRange(0, 100, 10);
            chart2D.DefaultView.ChartArea.AxisX.DefaultLabelFormat = "#VAL{0.}";

            chart2D.DefaultView.ChartArea.AxisX.AutoRange = false;
            chart2D.DefaultView.ChartArea.AxisX.AddRange(0, rowsCount, rowsCount * 0.1);
            //chart2D.DefaultView.ChartArea.AxisX.AddRange(0, rowsCount, rowsCount*0.1);

            chart2D.DefaultView.ChartArea.AxisY.MajorGridLinesVisibility = Visibility.Visible;
            chart2D.DefaultView.ChartArea.AxisY.MinorGridLinesVisibility = Visibility.Visible;
            chart2D.DefaultView.ChartArea.AxisX.MajorGridLinesVisibility = Visibility.Visible;
            chart2D.DefaultView.ChartLegend.Margin = new Thickness(15, 0, 0, 0);

            chart2D.DefaultView.ChartArea.LabelFormatBehavior = LabelFormatBehavior.None;
            chart2D.SamplingSettings.SamplingThreshold = rowsCount;
            chart2D.DefaultView.ChartArea.EnableAnimations = false;
        }

    
        private void ConfigureMultiLineChart()
        {
            chart2D = new Chart2D();
            chartPanel.Children.Clear();
            chartPanel.Children.Add(chart2D);
            int tickDistance = 0;
            int index = 0;
            foreach (DataTable table in selectedObject.ObjectHistory.Values)
            {
                if (tickDistance < table.Rows.Count)
                    tickDistance = table.Rows.Count;

                SeriesMapping sm1 = new SeriesMapping();
                sm1.SeriesDefinition = new LineSeriesDefinition();
                sm1.LegendLabel = table.TableName;
                sm1.SeriesDefinition.ShowItemLabels = false;
                ((LineSeriesDefinition)sm1.SeriesDefinition).ShowPointMarks = false;
                sm1.CollectionIndex = index++;

                ItemMapping im1 = new ItemMapping();
                im1.DataPointMember = DataPointMember.YValue;

                sm1.ItemMappings.Add(im1);

                chart2D.SeriesMappings.Add(sm1);
            }

            chart2D.DefaultView.ChartArea.ZoomScrollSettingsX.ScrollMode = ScrollMode.None;
            chart2D.DefaultView.ChartArea.ZoomScrollSettingsY.ScrollMode = ScrollMode.None;

            chart2D.DefaultView.ChartArea.AxisX.AutoRange = false;
            chart2D.DefaultView.ChartArea.AxisX.AddRange(0, tickDistance + 1, tickDistance*0.1);

            chart2D.DefaultView.ChartArea.AxisY.MajorGridLinesVisibility = Visibility.Visible;
            chart2D.DefaultView.ChartArea.AxisY.MinorGridLinesVisibility = Visibility.Visible;
            chart2D.DefaultView.ChartArea.AxisX.MajorGridLinesVisibility = Visibility.Visible;
            chart2D.DefaultView.ChartLegend.Margin = new Thickness(15, 0, 0, 0);

            chart2D.DefaultView.ChartArea.LabelFormatBehavior = LabelFormatBehavior.None;
            chart2D.SamplingSettings.SamplingThreshold = tickDistance+1;
            chart2D.DefaultView.ChartArea.EnableAnimations = false;

            chart2D.DefaultView.ChartArea.AxisX.Title = "Objects";
            chart2D.DefaultView.ChartArea.AxisY.Title = "Values";
        }

       
        private void ConfigureStackBarChart()
        {
            chart2D = new Chart2D();
            chartPanel.Children.Clear();
            chartPanel.Children.Add(chart2D);

            for (int i = 0; i < totalColumns; i++)
            {
                SeriesMapping sm1 = new SeriesMapping();
                StackedBarSeriesDefinition d1 = new StackedBarSeriesDefinition();
                d1.StackGroupName = "Stack1";
                sm1.SeriesDefinition = d1;
                sm1.LegendLabel = simulationNames[i];
                sm1.CollectionIndex = i;

                ItemMapping im1 = new ItemMapping();
                im1.DataPointMember = DataPointMember.YValue;

                sm1.ItemMappings.Add(im1);

                chart2D.SeriesMappings.Add(sm1);
            }
            chart2D.DefaultView.ChartArea.AxisY.MajorGridLinesVisibility = Visibility.Hidden;
            chart2D.DefaultView.ChartArea.AxisY.MinorGridLinesVisibility = Visibility.Hidden;
            chart2D.DefaultView.ChartArea.AxisX.MajorGridLinesVisibility = Visibility.Hidden;
            chart2D.DefaultView.ChartArea.ZoomScrollSettingsX.ScrollMode = ScrollMode.None;
            chart2D.DefaultView.ChartArea.ZoomScrollSettingsY.ScrollMode = ScrollMode.None;
            chart2D.DefaultView.ChartArea.AxisX.AutoRange = true;
            chart2D.DefaultView.ChartArea.AxisY.AutoRange = true;

            chart2D.DefaultView.ChartArea.AxisX.Title = "Objects";
            chart2D.DefaultView.ChartArea.AxisY.Title = "Values";    
        }

        #endregion

        #region GridView manipulations

        #region GridView initialization
        /// <summary>
        /// Initialize brief DataTable
        /// </summary>
        private void InitializeBriefTable()
        {
            briefHistoryDataTable = new DataTable();
            briefHistoryDataTable.Columns.Add("Object", typeof(PNObject));

            foreach (string name in simulationNames)
            {
                if (briefHistoryDataTable.Columns.Contains(name))
                    briefHistoryDataTable.Columns.Add(name + simulationNames.Count, typeof(double));
                else briefHistoryDataTable.Columns.Add(name, typeof(double));
            }

            int rowIndex = 0;

            // Fragile code
            foreach(PNObject pnObject in PNObjectRepository.PNObjects.Values)
            {
                if (pnObject.AllowSaveHistory)
                {
                    IDictionary<string, double> maxValues = pnObject.GetMaxValuesFromHistory();

                    briefHistoryDataTable.Rows.Add(pnObject);
                    foreach (var maxValue in maxValues)
                    {
                        briefHistoryDataTable.Rows[rowIndex][maxValue.Key] = maxValue.Value;
                    }
                    rowIndex += 1;
                }

            }
          
            briefHistoryTable.ItemsSource = briefHistoryDataTable;
        }

        #endregion

        #region Data loaders
        /// <summary>
        /// Initialize Detailed GridView with data of selected object
        /// </summary>
        private void ShowDetailedTable()
        {
            if (tableNames.SelectedIndex.Equals(-1)) return;
            historyTable.Height = chart2D.ActualHeight - 30;
            historyTable.ItemsSource = selectedObject.ObjectHistory[tableNames.SelectedItem.ToString()];
            exportBtn.IsEnabled = historyTable.ItemsSource != null;
        }
        #endregion

        #region Load data from GridViews to chart
        /// <summary>
        /// Show brief data from Brief GridView at chart
        /// </summary>
        private void ShowBriefDataInChart()
        {
            ConfigureStackBarChart();

            List<double[]> itemsSource = new List<double[]>();
            int rowSelectedCount = briefHistoryTable.SelectedItems.Count;

            for (int i = 0; i < totalColumns; i++)
            {
                double[] valueArray = new double[rowSelectedCount];
                for (int j = 0; j < rowSelectedCount; j++)
                {
                    DataRow row = briefHistoryTable.SelectedItems[j] as DataRow;
                    if(i.Equals(0)) chart2D.DefaultView.ChartArea.AxisX.TickPoints.Add(new TickPoint() { Label = row[0].ToString() });

                    if (row[i + 1] != DBNull.Value)
                        valueArray[j] = Convert.ToDouble(row[i + 1]);
                    else valueArray[j] = 0;
                }
                
                itemsSource.Add(valueArray);
            }
            chart2D.ItemsSource = itemsSource;
        }

        /// <summary>
        /// Show subtable from grid in chart
        /// </summary>
        private void ShowDetailedDataInChart()
        {
            ConfigureLineChart();

            int rowsCount = historyTable.SelectedItems.Count;

            List<ChartData> chartData = new List<ChartData>();

            for (int i = 0; i < rowsCount; i++)
            {
                DataRow row = historyTable.SelectedItems[i] as DataRow;
                chartData.Add(new ChartData(Convert.ToInt32(row[0]), Convert.ToDouble(row[1])));
            }
            chart2D.DefaultView.ChartArea.AxisX.AddRange(0, chartData[rowsCount - 1].XValue, chartData[rowsCount - 1].XValue * 0.1);
            chart2D.SamplingSettings.SamplingThreshold = (int)chartData[rowsCount - 1].XValue;
            chart2D.ItemsSource = chartData;
        }

        /// <summary>
        /// Show subtable from grid in chart
        /// </summary>
        private void ShowFullDetailedDataInChart()
        {
            ConfigureMultiLineChart();

            List<double[]> itemsSource = new List<double[]>();
            int tableCount = selectedObject.ObjectHistory.Count;

            foreach(string tableName in selectedObject.ObjectHistory.Keys)
            {
                int rowCount = selectedObject.ObjectHistory[tableName].Rows.Count;
                double[] valueArray = new double[rowCount];

                for (int j = 0; j < rowCount; j++)
                {
                    valueArray[j] = Convert.ToDouble(selectedObject.ObjectHistory[tableName].Rows[j][1]);
                }
                itemsSource.Add(valueArray);
            }
            chart2D.ItemsSource = itemsSource;
            itemsSource = null;
            //foreach (TickPoint point in chart2D.DefaultView.ChartArea.AxisX.TickPoints)
            //    point.Label = "Item" + k.ToString(); //ticks[k++];
        }
        #endregion

        #region GridView Events
        private void briefHistoryTable_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            try
            {
                ShowBriefDataInChart();
            }
            catch (Exception)
            {
            }

        }
        /// <summary>
        /// Load data from location to chart
        /// </summary>
        private void historyTable_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            /*try
            {
                ShowDetailedDataInChart();
            }
            catch (Exception)
            {
            }*/
        }
        #endregion

        #endregion  

        #region Miscelaneous methods

        private void ExportChart()
        {
            string fileName = DialogBoxes.SaveDialog(null, DocumentFormat.Chart);
            if (fileName != null)
            {
                string extension = fileName.Substring(fileName.Length - 3, 3);

                ContentExporter.ExportChart(fileName, chart2D, extension);        
            }
        }

        private void GetHistoryTableFromSelectedObject()
        {
            selectedObject = (PNObject)((DataRow)briefHistoryTable.SelectedItem)[0];
            if (selectedObject != null)
            {
                tableNames.ItemsSource = selectedObject.GetTableNames();
                tableNames.SelectedIndex = 0;
                ShowDetailedTable();
                // historyTable.ItemsSource = selectedObject.ObjectHistory[0];
            }
        }
     
        private void ShowDetails()
        {
            GetHistoryTableFromSelectedObject();
            ShowFullDetailedDataInChart();
        }
        #endregion

        #region Events
       
        private void tableNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox list = (ComboBox)e.OriginalSource;
            switch (list.Name)
            {
                case "tableNames":
                    {
                        ShowDetailedTable();
                    } break;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = e.OriginalSource as MenuItem;
            switch (mi.Header.ToString())
            {
                case "Show data in chart": ShowDetailedDataInChart(); break;
                case "Show details": ShowDetails(); break;
                case "Show in brief":  ; break;
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            // objManager.ClearHistory();
            historyTable.SelectionChanged -= new EventHandler<SelectionChangeEventArgs>(historyTable_SelectionChanged);
            exportBtn.Click -= new RoutedEventHandler(Buttons_Click);
            printBtn.Click -= new RoutedEventHandler(Buttons_Click);
            chart2D = null;
            expWnd = null;
            GC.Collect();
        }

        private void Buttons_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.OriginalSource;
            switch (button.Name)
            {
                case "exportBtn":
                    {
                        expWnd = new HistoryExport(historyTable);
                        expWnd.Show();
                    } break;
                case "printBtn":
                    {
                        ExportChart();
                    } break;
            }
        }

       
        #endregion

        #region Commands
        private void OnWindowClose(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Windows.Shell.SystemCommands.CloseWindow(this);
        }

        private void OnWindowMaximize(object sender, ExecutedRoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized) this.WindowState = System.Windows.WindowState.Normal;
            else Microsoft.Windows.Shell.SystemCommands.MaximizeWindow(this);
        }

        private void OnWindowMinimize(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Windows.Shell.SystemCommands.MinimizeWindow(this);
        }
        #endregion

        #region Predicates
        private bool GetPNObjectByName(PNObject pnObject)
        {
            return pnObject.Name.Equals(((DataRow)historyTable.SelectedItem)[0].ToString());
        }
        #endregion
    }
    
}
