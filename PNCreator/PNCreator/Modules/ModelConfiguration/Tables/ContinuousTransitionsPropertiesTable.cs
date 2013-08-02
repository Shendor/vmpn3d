using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace PNCreator.Modules.ModelConfiguration.Tables
{
    public class ContinuousTransitionsPropertiesTable : TransitionsPropertiesTable
    {
        public ContinuousTransitionsPropertiesTable()
        {
            var expectanceColumn = new GridViewDataColumn
            {
                DataMemberBinding = new Binding("Expectance"),
                Header = "Expectance",
                UniqueName = "Expectance",
            };

            Columns.Insert(3, expectanceColumn);
        }
    }
}
