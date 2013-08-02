using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PNCreator.Properties;

namespace PNCreator.Controls.TextBox
{
    public partial class TipTextBox
    {

        public TipTextBox()
        {
            InitializeComponent();

            textBox.Text = Messages.Default.SimulationTextBoxTip;
        }

        public string Text
        {
            get
            {
                if (textBox.Text.Equals(Messages.Default.SimulationTextBoxTip))
                {
                    return "";
                }
                return textBox.Text;
            }
            set { textBox.Text = value; }
        }

        private void TextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Equals(Messages.Default.SimulationTextBoxTip))
            {
                textBox.Text = "";
            }
        }

        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Equals(""))
            {
                textBox.Text = Messages.Default.SimulationTextBoxTip;
            }
        }
    }
}
