using System.Collections.Generic;
using Meshes3D;
using PNCreator.ManagerClasses;
using PNCreator.Modules.Properties;
using PNCreator.Properties;
using WindowsControl;

namespace PNCreator.Commands
{
    public class DeletePNObjectsCommand
    {
        public void Delete(List<Mesh3D> pnObjects)
        {
            if (PNProperties.IsConfirmRemoving)
            {
                if (DialogWindow.Confirm(Messages.Default.DeleteObjects) == ButtonPressed.No)
                {
                    return;
                }
            }
            var objectManager = (PNObjectManager) App.GetObject(typeof (PNObjectManager));
            objectManager.DeleteObjects(pnObjects); 
        }
    }
}
