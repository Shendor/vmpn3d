using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace PNCreator.Modules.ModelConfiguration.Tables
{
    public class ContinuousLocationsPropertiesTable : FormulaPNObjectsPropertiesTable
    {
        public ContinuousLocationsPropertiesTable()
        {
            var levelColumn = new GridViewDataColumn
            {
                DataMemberBinding = new Binding("Level"),
                Header = "Level",
                UniqueName = "Level",
            };

            Columns.Insert(3, levelColumn);
        }
    }
}
