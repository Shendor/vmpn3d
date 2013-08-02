using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace PNCreator.Converters
{
       [ValueConversion(typeof(Vector), typeof(String))]
    public class Vector2DConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Vector vector = (Vector)value;
            DoubleConverter converter = new DoubleConverter();

            return converter.Convert(vector.X, typeof(double), null, null) +
                    ";  " + converter.Convert(vector.Y, typeof(double), null, null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
