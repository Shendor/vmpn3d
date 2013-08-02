
using System.Windows;
using Meshes3D;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;

namespace PNCreator.Modules.Main.PNObjectProperties.TransformationProperties
{
    public partial class SizePropertiesPanel
    {
        public SizePropertiesPanel()
        {
            InitializeComponent();
        }

        private void SizeValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            App.GetObject<EventPublisher>().ExecuteEvents(new MeshTransformChangedEventArgs((Mesh3D)((FrameworkElement)sender).Tag));
        }
    }
}
