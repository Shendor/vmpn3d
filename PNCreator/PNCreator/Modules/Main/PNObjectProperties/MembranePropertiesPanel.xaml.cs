using System.Windows;
using System.Windows.Controls;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Modules.Main.PNObjectProperties
{
    public partial class MembranePropertiesPanel : IPNObjectProperties
    {
        public MembranePropertiesPanel()
        {
            InitializeComponent();
        }

        private void CoveredObjectsComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
//              if (coveredObjectsCB.SelectedItem != null)
//            {
//                PNObject pnObject = (PNObject)coveredObjectsCB.SelectedItem;
//                pnObjectPicker.SelectedObject = pnObject;
//                ShowSelectedObjectProperties(pnObject);
//            }
        }

        public void SetPNObject(PNObject pnObject)
        {
            if (pnObject.Type == PNObjectTypes.Membrane)
            {
                SetMembraneSpeedDataVisibility(Visibility.Collapsed);
            }
            else
            {
                SetMembraneSpeedDataVisibility(Visibility.Visible);
            }
        }

        private void SetMembraneSpeedDataVisibility(Visibility visibility)
        {
            speedLabel.Visibility = visibility;
            speedTB.Visibility = visibility;
            formulaBtn.Visibility = visibility;
        }
    }
}
