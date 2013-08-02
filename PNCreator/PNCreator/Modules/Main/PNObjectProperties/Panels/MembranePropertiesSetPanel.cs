using System.Windows;
using System.Windows.Controls;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Modules.Main.PNObjectProperties.Panels
{
    public class MembranePropertiesSetPanel : ShapePropertiesSetPanel
    {
        private readonly MembranePropertiesPanel membranePanel;

        public MembranePropertiesSetPanel()
        {
            membranePanel = new MembranePropertiesPanel();
            var expander = new Expander
            {
                Header = "Membrane",
                IsExpanded = true,
                Content = membranePanel,
                Margin = new Thickness(3)
            };

            Children.Insert(0, expander);
        }

        public override void SetPNObject(PNObject pnObject)
        {
            base.SetPNObject(pnObject);
            membranePanel.SetPNObject(pnObject);
        }
    }
}
