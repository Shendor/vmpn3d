using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Telerik.Windows.Controls;

namespace PNCreator.Modules.ModelConfiguration.Tables
{
    public class TransitionsPropertiesTable : FormulaPNObjectsPropertiesTable
    {
        public TransitionsPropertiesTable()
        {
            var formulaColumn = new GridViewDataColumn
            {
                Header = "Guard Formula",
                UniqueName = "GuardFormula",
                CellTemplate = (DataTemplate)FindResource("guardFormulaButtonTemplate"),
            };

            Columns.Insert(Columns.Count - 2, formulaColumn);
        }
    }
}
