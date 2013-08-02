using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using PNCreator.Commands;
using PNCreator.ManagerClasses;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.ManagerClasses.Exception;
using PNCreator.Modules.Analizing;
using PNCreator.Modules.Help;
using PNCreator.Modules.Simulation.History;
using PNCreator.Modules.Simulation.Service;
using PNCreator.PNObjectsIerarchy;
using PNCreator.Properties;
using WindowsControl;

namespace PNCreator.Modules.Main.Menu
{
    public partial class MainMenu
    {
        private readonly WindowsFactory windowsFactory;
        private readonly PNObjectManager objectManager;
        private readonly PNDocument pnDocument;
        private readonly EventPublisher eventPublisher;

        public MainMenu()
        {
            InitializeComponent();

            windowsFactory = App.GetObject<WindowsFactory>();
            objectManager = App.GetObject<PNObjectManager>();
            pnDocument = App.GetObject<PNDocument>();
            eventPublisher = App.GetObject<EventPublisher>();

            LoadListOfRecentOpenedModels();
            eventPublisher.Register<OpenNetEventArgs>((args) => LoadListOfRecentOpenedModels());
        }

        private void MenuItemsClick(object sender, RoutedEventArgs e)
        {
            var cmdMenu = (MenuItem)e.OriginalSource;

            if (cmdMenu.Equals(importSimulationHistoryMI))
            {
                new ImportSimulationHistoryCommand().ImportSimulationHistory();
            }
            else if (cmdMenu.Equals(exportSimulationHistoryMI))
            {
                new ExportSimulationHistoryCommand().ExportSimulationHistory();
            }
            else if (cmdMenu.Equals(coveObjectsMI))
            {
                CoverObjectsByMembranes();
            }
            else if (cmdMenu.Equals(positioniserMI))
            {
                eventPublisher.ExecuteEvents(new BooleanEventArgs(positioniserMI.IsChecked));
            }
            else if (cmdMenu.Equals(modelInfoMI))
            {
                windowsFactory.GetWindow(typeof(ModelInformation.ModelInfo)).Show();
            }
            else if (cmdMenu.Equals(showMembranesMI))
            {
               // ShowMembranes();
            }
            else if (cmdMenu.Equals(diagrammesMI))
            {
                windowsFactory.GetWindow(typeof(Diagram)).Show();
            }
            else if (cmdMenu.Equals(chartsMI))
            {
                windowsFactory.GetWindow(typeof(Chart)).Show();
            }
            else if (cmdMenu.Equals(modelConfigMI))
            {
                windowsFactory.GetWindow(typeof(ModelConfiguration.ModelConfiguration)).Show();
            }
            else if (cmdMenu.Equals(clusterConfigMI))
            {
                windowsFactory.GetWindow(typeof(ServerConnection.ServerProperties)).Show();
            }
            else if (cmdMenu.Equals(helpMI))
            {
                ShowHelp();
            }
            else if (cmdMenu.Equals(aboutMI))
            {
                windowsFactory.GetWindow(typeof(About.About)).ShowDialog();
            }
        }

        private void ShowMembranes()
        {
            foreach (var shape in PNObjectRepository.GetPNObjects<Membrane>())
            {
                shape.IsWired = showMembranesMI.IsChecked;
            }
        }

        private void CoverObjectsByMembranes()
        {
            try
            {
                objectManager.CoverObjectsByAllMembranes();
            }
            catch (IllegalPNObjectException ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        private void LoadListOfRecentOpenedModels()
        {
            try
            {
                pnDocument.LoadListOfRecentOpenedModels();
                openRecentMI.Items.Clear();
                openRecentMI.Visibility = pnDocument.RecentOpenedModels.Length == 0 ? Visibility.Collapsed : Visibility.Visible;

                foreach (var fileName in pnDocument.RecentOpenedModels)
                {
                    if (fileName != null && !fileName.Equals(""))
                    {
                        var menuItem = new MenuItem
                            {
                                Header = fileName
                            };
                        menuItem.Click += (sender, args) =>
                            new OpenNetCommand().OpenNetFromFile(((MenuItem)sender).Header.ToString());
                        openRecentMI.Items.Add(menuItem);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                DialogWindow.Alert(Messages.Default.RecentOpenedModelsNotFound);
            }
            catch (Exception ex)
            {
                DialogWindow.Error(ex.Message);
            }
        }

        private void ShowHelp()
        {
            try
            {
                windowsFactory.GetWindow<HelpWindow>().Show();
            }
            catch (System.ComponentModel.Win32Exception)
            {
                DialogWindow.Alert(Messages.Default.CannotOpenHelp);
            }
        }
    }
}
