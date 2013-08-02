using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Meshes3D;
using PNCreator.Commands;
using PNCreator.ManagerClasses;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.PNObjectsIerarchy;
using Telerik.Windows.Controls;

namespace PNCreator.Modules.ModelConfiguration.Tables
{
    /// <summary>
    /// Interaction logic for PNPropertiesTable.xaml
    /// </summary>
    public partial class PNPropertiesTable
    {
        protected readonly PNObjectManager pnObjectManager;
        protected readonly EventPublisher eventPublisher;

        public PNPropertiesTable()
        {
            InitializeComponent();

            pnObjectManager = App.GetObject<PNObjectManager>();
            eventPublisher = App.GetObject<EventPublisher>();
                
            var zoomColumn = new GridViewDataColumn
            {
                DataMemberBinding = new Binding(),
                UniqueName = "Zoom"
            };
            zoomColumn.CellTemplate = (DataTemplate)FindResource("zoomButtonTemplate");

            var nameColumn = new GridViewDataColumn
            {
                DataMemberBinding = new Binding("Name"),
                Header = "Name",
                UniqueName = "Name"
            };

            var deleteColumn = new GridViewDataColumn
            {
                DataMemberBinding = new Binding(),
                Header = "Delete",
                UniqueName = "Delete"
            };
            deleteColumn.CellTemplate = (DataTemplate) FindResource("deleteButtonTemplate");

            Columns.Add(zoomColumn);
            Columns.Add(nameColumn);
            Columns.Add(deleteColumn);
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var pnObject = ((Button) sender).Tag as PNObject;
            if (pnObject != null)
            {
                pnObjectManager.DeleteObjects(new List<Mesh3D> { pnObject });
            }
        }

        private void ShapeButtonClick(object sender, RoutedEventArgs e)
        {
            var pnObject = ((Button)sender).Tag as PNObject;
            if (pnObject != null)
            {
                new SetMeshCommand().SetMeshForPNObject(pnObject);
            }
        }

        private void FormulaButtonClick(object sender, RoutedEventArgs e)
        {
            SetFormulaForPNObject(FormulaTypes.Value, sender);
        }

        private void GuardFormulaButtonClick(object sender, RoutedEventArgs e)
        {
            SetFormulaForPNObject(FormulaTypes.Guard, sender);
        }

        private void GridViewCellEditEnded(object sender, GridViewCellEditEndedEventArgs e)
        {
            PNObjectChanged(((RadGridView) sender).SelectedItem);
        }

        private static void SetFormulaForPNObject(FormulaTypes formulaType, object sender)
        {
            var pnObject = ((Button)sender).Tag as PNObject;
            if (pnObject != null)
            {
                new SetFormulaCommand().SetFormulaForPNObject(formulaType, pnObject);
            }
        }

        private void ZoomButtonClick(object sender, RoutedEventArgs e)
        {
            var pnObject = ((Button)sender).Tag as PNObject;
            if (pnObject != null)
            {

            }
        }

        private void SaveHistoryCheckedClick(object sender, RoutedEventArgs e)
        {
            PNObjectChanged(((CheckBox)sender).Tag);
        }

        private void ColorPickerColorChanged(object sender, EventArgs e)
        {
            var pnObject = ((RadColorPicker)sender).Tag as PNObject;
            if (pnObject != null && !(pnObject is Arc3D))
                pnObject.MaterialColor = ((RadColorPicker)sender).SelectedColor;
            PNObjectChanged(pnObject);
        }

        protected void PNObjectChanged(object pnObject)
        {
            if(pnObject != null)
                eventPublisher.ExecuteEvents(new PNObjectChangedEventArgs((PNObject)pnObject));
        }

       
    }
}
