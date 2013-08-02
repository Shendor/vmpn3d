using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace PNCreator.Modules.ModelConfiguration.Tables
{
    public class DiscreteTransitionsPropertiesTable : TransitionsPropertiesTable
    {
        public DiscreteTransitionsPropertiesTable()
        {
            var delayColumn = new GridViewDataColumn
            {
                DataMemberBinding = new Binding("Delay"),
                Header = "Delay",
                UniqueName = "Delay",
            };

            Columns.Insert(3, delayColumn);
        }
    }
}
