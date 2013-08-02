using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using PNCreator.Controls.Progress;
using PNCreator.ManagerClasses;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.ManagerClasses.Exception;
using PNCreator.Modules.Simulation.Service;

namespace PNCreator.Modules.Simulation.History
{
    public class ImportSimulationHistoryCommand
    {
        public void ImportSimulationHistory()
        {
            
            ISimulationHistoryDao simulationHistoryDao = new SimulationHistoryDao();
            string fileName = DialogBoxes.OpenDialog(PNDocument.ApplicationPath, DocumentFormat.XML);
            if (fileName != null)
            {
                try
                {
                    /*ProgressWindow progressWindow = null;

                    App.GetObject<EventPublisher>().Register((ProgressEventArgs progressArgs) =>
                                              Application.Current.MainWindow.Dispatcher.BeginInvoke
                                                  (DispatcherPriority.Normal,
                                                   (ThreadStart)(() =>
                                                   {
                                                       if (progressWindow == null || !progressWindow.IsVisible)
                                                       {
                                                           progressWindow = new ProgressWindow();
                                                           progressWindow.ShowDialog();
                                                       }
                                                       progressWindow.Maximum = PNObjectRepository.Count;
                                                       progressWindow.Progress = progressArgs.Progress;
                                                   })));*/

                    simulationHistoryDao.LoadSimulationData(fileName, PNObjectRepository.PNObjects.Values);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                }
                /*finally
                {
                    App.GetObject<EventPublisher>().UnRegister(typeof(ProgressEventArgs));
                }*/
            }
        }
    }
}
