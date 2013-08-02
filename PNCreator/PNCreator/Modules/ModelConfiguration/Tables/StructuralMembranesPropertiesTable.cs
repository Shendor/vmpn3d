using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace PNCreator.Modules.ModelConfiguration.Tables
{
    public class StructuralMembranesPropertiesTable : FormulaPNObjectsPropertiesTable
    {
        public StructuralMembranesPropertiesTable()
        {
            var speedColumn = new GridViewDataColumn
            {
                DataMemberBinding = new Binding("Speed"),
                Header = "Speed",
                UniqueName = "Speed",
            };

            Columns.Insert(3, speedColumn);
        }
    }
}
