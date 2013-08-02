using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PNCreator.Commands;
using PNCreator.ManagerClasses;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Modules.Main.PNObjectProperties
{
    public class FormulaPropertiesPanel : Grid
    {
        protected virtual void FormulaButtonClick(object sender, RoutedEventArgs e)
        {
            SetFormulaForObject(sender, FormulaTypes.Value);
        }

        protected void SetFormulaForObject(object sender, FormulaTypes formulaType)
        {
            var pnObject = ((Button) sender).Tag as PNObject;
            if (pnObject != null)
            {
                new SetFormulaCommand().SetFormulaForPNObject(formulaType, pnObject);
            }
        }

        protected void ValueChangedTextBox(object sender, KeyEventArgs e)
        {
            var texBox = (TextBox) sender;
            var pnObject = texBox.Tag as PNObject;
            if (pnObject != null)
            {
                pnObject.ValueInCanvas.Text = texBox.Text;
            }
        }
    }
}
