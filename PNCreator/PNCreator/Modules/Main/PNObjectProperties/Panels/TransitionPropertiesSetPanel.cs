using System.Windows;
using System.Windows.Controls;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Modules.Main.PNObjectProperties.Panels
{
    public class TransitionPropertiesSetPanel : ShapePropertiesSetPanel
    {
        private readonly TransitionPropertiesPanel transitionPanel;

        public TransitionPropertiesSetPanel()
        {
            transitionPanel = new TransitionPropertiesPanel();
            var expander = new Expander
            {
                Header = "Transition",
                IsExpanded = true,
                Content = transitionPanel,
                Margin = new Thickness(3)
            };

            Children.Insert(0, expander);
        }

        public override void SetPNObject(PNObject pnObject)
        {
            base.SetPNObject(pnObject);
            transitionPanel.SetPNObject(pnObject);
        }
    }
}
