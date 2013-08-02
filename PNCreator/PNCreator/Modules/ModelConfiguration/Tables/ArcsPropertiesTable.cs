using System.Windows.Data;
using Telerik.Windows.Controls;

namespace PNCreator.Modules.ModelConfiguration.Tables
{
    public class ArcsPropertiesTable : FormulaPNObjectsPropertiesTable
    {
        public ArcsPropertiesTable()
        {
            Columns.Remove(colorColumn);

            var weightColumn = new GridViewDataColumn
            {
                DataMemberBinding = new Binding("Weight"),
                Header = "Weight",
                UniqueName = "Weight",
            };

            Columns.Insert(2, weightColumn);
        }
    }
}
