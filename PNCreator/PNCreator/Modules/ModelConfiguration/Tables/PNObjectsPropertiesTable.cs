using System.Windows;
using Telerik.Windows.Controls;

namespace PNCreator.Modules.ModelConfiguration.Tables
{
    public class PNObjectsPropertiesTable : PNPropertiesTable
    {
        protected readonly GridViewDataColumn colorColumn;

        public PNObjectsPropertiesTable()
        {
            colorColumn = new GridViewDataColumn
                {
                    Header = "Color",
                    UniqueName = "MaterialColor",
                    CellTemplate = (DataTemplate)FindResource("colorTemplate"),
                    CellEditTemplate = (DataTemplate)FindResource("colorEditTemplate")
                };

            var saveHistoryColumn = new GridViewDataColumn
                {
                    Header = "Save History",
                    UniqueName = "AllowSaveHistory",
                    CellTemplate = (DataTemplate)FindResource("saveHistoryTemplate")
                };

            var shapeColumn = new GridViewDataColumn
                {
                    Header = "Mesh",
                    UniqueName = "Mesh",
                    IsReadOnly = false,
                    CellTemplate = (DataTemplate)FindResource("shapeButtonTemplate")
                };

            Columns.Insert(2, colorColumn);
            Columns.Insert(3, saveHistoryColumn);
            Columns.Insert(4, shapeColumn);
        }

    }
}
