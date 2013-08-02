using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WindowsControl
{
    /// <summary>
    /// Interaction logic for Confirm.xaml
    /// </summary>
    public partial class Confirm : Window
    {
        public ButtonPressed ButtonPressed { get; set; }
        public Confirm(string message)
        {
            InitializeComponent();
            this.message.Text = message;
            noBtn.Focus();
            ButtonPressed = WindowsControl.ButtonPressed.Cancel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.OriginalSource as Button;
            switch (btn.Name)
            {
                case "yesBtn": ButtonPressed = WindowsControl.ButtonPressed.Yes; Close(); break;
                case "noBtn": ButtonPressed = WindowsControl.ButtonPressed.No; Close(); break;
            }
        }


        private void RowDefinition_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
    public enum ButtonPressed
    {
        Yes,
        No,
        Cancel
    }
}
