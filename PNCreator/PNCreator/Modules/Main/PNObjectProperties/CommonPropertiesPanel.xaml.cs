using System.Windows;
using System.Windows.Controls;
using PNCreator.Commands;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Modules.Main.PNObjectProperties
{
    public partial class CommonPropertiesPanel
    {
        public CommonPropertiesPanel()
        {
            InitializeComponent();
        }

        private void ShapeButtonClick(object sender, RoutedEventArgs e)
        {
            var pnObject = ((Button) sender).Tag as PNObject;
            if (pnObject != null)
            {
                new SetMeshCommand().SetMeshForPNObject(pnObject);
            }
        }
    }
}
