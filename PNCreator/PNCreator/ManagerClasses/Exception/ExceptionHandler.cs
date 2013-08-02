using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using WindowsControl;

namespace PNCreator.ManagerClasses.Exception
{
    public static class ExceptionHandler
    {
        public static void HandleException(_Exception e)
        {
            if (e is FormatException) HandleWarning(e);
            else HandleError(e);
        }

        private static void HandleError(_Exception e)
        {
            HandleError(e.Message);
        }

        private static void HandleWarning(_Exception e)
        {
            DialogWindow.Alert(e.Message);
        }

        private static void HandleError(String message)
        {
            DialogWindow.Error(message);
        }

        private static void HandleWarning(String message)
        {
            DialogWindow.Alert(message);
        }
    }
}
