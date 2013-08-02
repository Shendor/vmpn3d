using System.Collections.Generic;
using System.Windows;
using PNCreator.ManagerClasses;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.ManagerClasses.Simulation;
using PNCreator.Modules.Simulation;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Modules.Main.Toolbar
{
    public partial class PNDocumentToolBar
    {
        public PNDocumentToolBar()
        {
            InitializeComponent();
        }

        private void SimulationButtonClick(object sender, RoutedEventArgs e)
        {
            App.GetObject<WindowsFactory>().GetWindow(typeof(SimulationChooserWindow)).ShowDialog();
        }
    }
}
