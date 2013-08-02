using System.Windows;
using Telerik.Windows.Controls;
using PNCreator.ManagerClasses;

namespace PNCreator.Modules.Analizing
{

    public partial class HistoryExport
    {
        private RadGridView gridView;
 
        public HistoryExport(RadGridView grid)
        {
            InitializeComponent();

            gridView = grid;

            gridView.ElementExporting += RadGridViewExporting;
        }
    
        void RadGridViewExporting(object sender, GridViewElementExportingEventArgs e)
        {
            if (e.Element == ExportElement.HeaderRow)
            {
                e.Height = 50;
                e.Background = HeaderBackgroundPicker.SelectedColor;
                e.Foreground = HeaderForegroundPicker.SelectedColor;
                e.FontSize = 20;
                e.FontWeight = FontWeights.Bold;
            }
            else if (e.Element == ExportElement.Row)
            {
                e.Background = RowBackgroundPicker.SelectedColor;
                e.Foreground = RowForegroundPicker.SelectedColor;
            }
        }
       
        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            gridView = null;
            Close();
        }
       
        private void exportBtn_Click(object sender, RoutedEventArgs e)
        {
            var docType = DocumentFormat.MSWord;
            if (comboBox1.SelectedIndex.Equals(0)) docType = DocumentFormat.MSWord;
            if (comboBox1.SelectedIndex.Equals(1)) docType = DocumentFormat.MSExcel;
            if (comboBox1.SelectedIndex.Equals(2)) docType = DocumentFormat.MSExcelML;
            if (comboBox1.SelectedIndex.Equals(3)) docType = DocumentFormat.MSExcelCsv;
            if (comboBox1.SelectedIndex.Equals(4)) docType = DocumentFormat.Txt;
            
            ContentExporter.ExportTable(gridView, (bool)checkBox1.IsChecked, docType);
        }
    }
}
