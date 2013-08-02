using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using PNCreator.ManagerClasses;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Modules.Main.PNObjectProperties
{
    public partial class TransitionPropertiesPanel : IPNObjectProperties
    {
        public TransitionPropertiesPanel()
        {
            InitializeComponent();
        }

        protected override void FormulaButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(guardFormulaBtn))
            {
                SetFormulaForObject(sender, FormulaTypes.Guard);
            }
            else
            {
                base.FormulaButtonClick(sender, e);
            }
        }

        public void SetPNObject(PNObject pnObject)
        {
            var binding = new Binding
            {
                Mode = BindingMode.TwoWay
            };
            switch (pnObject.Type)
            {
                case PNObjectTypes.DiscreteTransition:
                    valueLabel.Text = "Delay";
                    binding.Path = new PropertyPath("Delay");
                    break;
                case PNObjectTypes.ContinuousTransition:
                    valueLabel.Text = "Expectance";
                    binding.Path = new PropertyPath("Expectance");
                    break;
            }
            valueTB.SetBinding(TextBox.TextProperty, binding);
        }
    }
}
