using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace NumericBox.Converters
{
    [ValueConversion(typeof(double), typeof(String))]
    public class DoubleConverter : IValueConverter
    {
        public const string FORMAT = "F2";

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((double)value).ToString(FORMAT);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Double.Parse(value.ToString());
        }
    }
}
