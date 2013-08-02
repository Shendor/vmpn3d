using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace PNCreator.Modules.Analizing
{
    class Chart2D : RadChart
    {
        public Chart2D()
            : base()
        {
            this.Margin = new Thickness(10);
            this.Background = Brushes.White;
        }
    }
}
