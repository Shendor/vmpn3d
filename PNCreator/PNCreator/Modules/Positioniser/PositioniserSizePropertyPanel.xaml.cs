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
using PNCreator.Controls.Positioniser;

namespace PNCreator.Modules.Positioniser
{
    /// <summary>
    /// Interaction logic for PositioniserSizePropertyPanel.xaml
    /// </summary>
    public partial class PositioniserSizePropertyPanel : IPositioniserProperty
    {
        public PositioniserSizePropertyPanel()
        {
            InitializeComponent();
        }

        public void SetParameters(Grid3DParameters parameters)
        {
            this.DataContext = parameters;
        }
    }
}
