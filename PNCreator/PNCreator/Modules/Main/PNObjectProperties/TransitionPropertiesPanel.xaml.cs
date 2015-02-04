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
            DataContext = null;
            DataContext = pnObject;
            var bindingValue = new Binding
            {
                Mode = BindingMode.TwoWay
            };
            switch (pnObject.Type)
            {
                case PNObjectTypes.DiscreteTransition:
                    valueLabel.Text = "Delay";
                    bindingValue.Path = new PropertyPath("Delay");
                    break;
                case PNObjectTypes.ContinuousTransition:
                    valueLabel.Text = "Expectance";
                    bindingValue.Path = new PropertyPath("Expectance");
                    break;
            }
            valueTB.SetBinding(TextBox.TextProperty, bindingValue);
        }
    }
}
