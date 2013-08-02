
using System.Windows;
using System.Windows.Controls;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Modules.Main.PNObjectProperties.Panels
{
    public class LocationPropertiesSetPanel : ShapePropertiesSetPanel
    {
        private readonly LocationPropertiesPanel locationPropertiesPanel;

        public LocationPropertiesSetPanel()
        {
            locationPropertiesPanel = new LocationPropertiesPanel();
            var expander = new Expander
            {
                Header = "Location",
                IsExpanded = true,
                Content = locationPropertiesPanel,
                Margin = new Thickness(3)
            };

            Children.Insert(0, expander);
        }

        public override void SetPNObject(PNObject pnObject)
        {
            base.SetPNObject(pnObject);
            locationPropertiesPanel.SetPNObject(pnObject);
        }
    }
}
