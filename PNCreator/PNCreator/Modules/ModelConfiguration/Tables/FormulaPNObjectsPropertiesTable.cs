using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Telerik.Windows.Controls;

namespace PNCreator.Modules.ModelConfiguration.Tables
{
    public class FormulaPNObjectsPropertiesTable : PNObjectsPropertiesTable
    {
        public FormulaPNObjectsPropertiesTable()
        {
            var formulaColumn = new GridViewDataColumn
            {
                //                DataMemberBinding = new Binding("MaterialColor"),
                Header = "Formula",
                UniqueName = "Formula",
                CellTemplate = (DataTemplate)FindResource("formulaButtonTemplate"),
            };

            Columns.Insert(Columns.Count - 2, formulaColumn);
        }
    }
}
