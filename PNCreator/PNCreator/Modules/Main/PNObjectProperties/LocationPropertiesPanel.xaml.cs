using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Modules.Main.PNObjectProperties
{
    public partial class LocationPropertiesPanel : IPNObjectProperties
    {
        public LocationPropertiesPanel()
        {
            InitializeComponent();
        }

        public void SetPNObject(PNObject pnObject)
        {
            var binding = new Binding {Mode = BindingMode.TwoWay};
            switch (pnObject.Type)
            {
                case PNObjectTypes.DiscreteLocation:
                    valueLabel.Text = "Tokens";
                    binding.Path = new PropertyPath("Tokens");
                    break;
                case PNObjectTypes.ContinuousLocation:
                    valueLabel.Text = "Level";
                    binding.Path = new PropertyPath("Level");
                    break;
            }
            valueTB.SetBinding(TextBox.TextProperty, binding);
        }
    }
}
