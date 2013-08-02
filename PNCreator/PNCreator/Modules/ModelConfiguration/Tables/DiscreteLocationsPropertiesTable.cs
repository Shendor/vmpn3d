using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace PNCreator.Modules.ModelConfiguration.Tables
{
    public class DiscreteLocationsPropertiesTable : FormulaPNObjectsPropertiesTable
    {
        public DiscreteLocationsPropertiesTable()
        {
            var tokensColumn = new GridViewDataColumn
            {
                DataMemberBinding = new Binding("Tokens"),
                Header = "Tokens",
                UniqueName = "Tokens",
            };

            Columns.Insert(3, tokensColumn);
        }
    }
}
