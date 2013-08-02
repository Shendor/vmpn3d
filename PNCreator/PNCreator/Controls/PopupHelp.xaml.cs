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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PNCreator.Controls
{
    /// <summary>
    /// Interaction logic for PopupHelp.xaml
    /// </summary>
    public partial class PopupHelp : UserControl
    {
        public PopupHelp()
        {
            InitializeComponent();
        }

        public void Move(Point point)
        {
            Canvas.SetLeft(this, point.X);
            Canvas.SetTop(this, point.Y);
        }
    }
}
