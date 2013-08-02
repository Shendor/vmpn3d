using System.Windows;
using System.Windows.Controls;
using PNCreator.Commands;
using PNCreator.ManagerClasses;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Modules.Properties
{
    public partial class Properties
    {
        private PNProperties properties;

        public Properties()
        {
            InitializeComponent();
            LoadProperties();
        }

        private void OtherButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.OriginalSource;
            switch (btn.Name)
            {
                case "okBtn":
                    SaveProperties();
                    break;
                case "cancelBtn":
                    this.Close();
                    break;
            }
        }

        #region Loading forms lists

        private void LoadProperties()
        {
            tabControl.DataContext = properties = PNProperties.LoadProperties();
        }

        #endregion

        #region Modify options

        private void SaveProperties()
        {
            //PNProperties.DiscreteLocationsForm = dLocFormTB.Text;
            //PNProperties.DiscreteTransitionsForm = dTransFormTB.Text;

            //PNProperties.ContinuousLocationsForm = cLocFormTB.Text;
            //PNProperties.ContinuousTransitionsForm = cTransFormTB.Text;

            //PNProperties.MembranesForm = membranesFormTB.Text;

            //PNProperties.ArcsColor = arcsColorTB.Text;
            PNProperties.DiscreteObjetcsColor = dObjetcsColor.SelectedColor;
            PNProperties.ContinuousObjetcsColor = cObjetcsColor.SelectedColor;
            PNProperties.MembranesColor = membranesColor.SelectedColor;

            PNProperties.OpacityLevel = opacityLevel.Value;
            PNProperties.ArcsThickness = arcsThickness.Value;

            PNProperties.DiscreteObjectsNamesVisibility = (bool)dObjectsNamesVisibility.IsChecked;
            PNProperties.DiscreteObjectsValuesVisibility = (bool)dObjectsValuesVisibility.IsChecked;

            PNProperties.ContinuousObjectsNamesVisibility = (bool)cObjectsNamesVisibility.IsChecked;
            PNProperties.ContinuousObjectsValuesVisibility = (bool)cObjectsValuesVisibility.IsChecked;

            PNProperties.IsConfirmRemoving = (bool)isConfirmRemoving.IsChecked;
            PNProperties.IsConfirmExit = (bool)isConfirmExit.IsChecked;

            PNProperties.ArcsNamesVisibility = (bool)arcsNamesVisibility.IsChecked;
            PNProperties.ArcsValuesVisibility = (bool)arcsValuesVisibility.IsChecked;

            PNProperties.SavePropeties();

            ConfigurePNObjectsProperties();

            Close();
        }

        private void ConfigurePNObjectsProperties()
        {
            foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
            {
                if (pnObject.Type == PNObjectTypes.DiscreteLocation ||
                    pnObject.Type == PNObjectTypes.DiscreteTransition)
                {
                    pnObject.IsNameVisible(PNProperties.DiscreteObjectsNamesVisibility);
                    pnObject.IsValueVisible(PNProperties.DiscreteObjectsValuesVisibility);
                }
                else if (pnObject.Type == PNObjectTypes.ContinuousLocation ||
                    pnObject.Type == PNObjectTypes.ContinuousTransition)
                {
                    pnObject.IsNameVisible(PNProperties.ContinuousObjectsNamesVisibility);
                    pnObject.IsValueVisible(PNProperties.ContinuousObjectsValuesVisibility);
                }
                else if (pnObject.Type == PNObjectTypes.Membrane ||
                    pnObject.Type == PNObjectTypes.StructuralMembrane)
                {
                    pnObject.ResetMaterial();
                }
                else
                {
                    ((Arc3D)pnObject).Thickness = arcsThickness.Value;
                    pnObject.IsNameVisible(PNProperties.ArcsNamesVisibility);
                    pnObject.IsValueVisible(PNProperties.ArcsValuesVisibility);
                }
            }
        }

        #endregion

        private void SetMeshButtonsClick(object sender, RoutedEventArgs e)
        {
            var button = ((Button)e.OriginalSource);
            var documentFormat = DocumentFormat.Mesh;
            if (tabControl.SelectedIndex == 2)
            {
                documentFormat = DocumentFormat.Texture;
            }
            string relativeMeshPath = new SetMeshCommand().GetRelativeMeshPath(documentFormat);
            if (relativeMeshPath != null)
            {
                button.Tag = relativeMeshPath;
                tabControl.DataContext = properties;
            }
        }
    }
}
