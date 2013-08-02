using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using PNCreator.ManagerClasses;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.Modules.ModelConfiguration.Tables;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Modules.ModelConfiguration
{
    public partial class ModelConfiguration
    {

        public ModelConfiguration()
        {
            InitializeComponent();

            InitializeTables();

            DataContext = App.GetObject<PNObjectManager>();
//            App.GetObject<EventPublisher>().Register((PNObjectChangedEventArgs args) =>
//                InitializeTables());
            App.GetObject<EventPublisher>().Register((PNObjectsDeletedEventArgs args) =>
                InitializeTables());
        }

        private void InitializeTables()
        {
            membranesTable.ItemsSource = PNObjectRepository.GetPNObjectsByType(PNObjectTypes.Membrane);
            structuralMembranesTable.ItemsSource =
                PNObjectRepository.GetPNObjectsByType(PNObjectTypes.StructuralMembrane);
            discreteLocationsTable.ItemsSource =
                PNObjectRepository.GetPNObjectsByType(PNObjectTypes.DiscreteLocation);
            continuousLocationsTable.ItemsSource =
                PNObjectRepository.GetPNObjectsByType(PNObjectTypes.ContinuousLocation);
            discreteTransitionsTable.ItemsSource =
                PNObjectRepository.GetPNObjectsByType(PNObjectTypes.DiscreteTransition);
            continuousTransitionsTable.ItemsSource =
                PNObjectRepository.GetPNObjectsByType(PNObjectTypes.ContinuousTransition);
            arcsTable.ItemsSource = PNObjectRepository.GetArcs();
        }


        private void AcceptButtonsClick(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }

    }
}
