using System;
using PNCreator.ManagerClasses;
using PNCreator.ManagerClasses.Exception;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Commands
{
    class SetFormulaCommand
    {
        public void SetFormulaForPNObject(FormulaTypes formulaType, PNObject pnObject)
        {
            try
            {
                App.GetObject<WindowsFactory>().GetFormulaBuilderWindow(formulaType, pnObject).ShowDialog();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
    }
}
