using PNCreator.ManagerClasses;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.ManagerClasses.FormulaManager;
using PNCreator.Properties;
using WindowsControl;

namespace PNCreator.Commands
{
    public class NewNetCommand
    {
        public void NewNet()
        {
            if (DialogWindow.Confirm(Messages.Default.NewNet) == ButtonPressed.Yes)
            {
                App.GetObject<PNDocument>().NewNet();
                App.GetObject<EventPublisher>().ExecuteEvents(new NewNetEventArgs());
                App.GetObject<IFormulaManager>().IsNeedToCompile = true;
            }
        }
    }
}
