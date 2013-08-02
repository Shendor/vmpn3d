
using System;
using System.IO;
using PNCreator.ManagerClasses;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.ManagerClasses.Exception;
using PNCreator.Properties;
using WindowsControl;

namespace PNCreator.Commands
{
    public class OpenNetCommand
    {
        public void OpenNet()
        {
            string fileName = DialogBoxes.OpenDialog(PNDocument.ApplicationPath, DocumentFormat.Net);
            if (fileName != null)
            {
                OpenNetFromFile(fileName);               
            }
        }

        public void OpenNetFromFile(string fileName)
        {
            var pnDocument = App.GetObject<PNDocument>();
            try
            {
                pnDocument.NewNet();
                pnDocument.OpenNet(fileName);
                pnDocument.AddRecentOpenedModel(fileName);

                App.GetObject<EventPublisher>().ExecuteEvents(new OpenNetEventArgs()
                {
                    PNObjects = PNObjectRepository.PNObjects
                });
            }
            catch (System.Xml.XmlException)
            {
                DialogWindow.Error(Messages.Default.WrongNetFileFormat);
            }
            catch (FormatException)
            {
                DialogWindow.Error(Messages.Default.WrongNetFileFormat);
            }
            catch (FileNotFoundException)
            {
                DialogWindow.Error(Messages.Default.FileNotFound);
            }
            catch (DirectoryNotFoundException)
            {
                DialogWindow.Error(Messages.Default.DirNotFound);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
        }
    }
}
