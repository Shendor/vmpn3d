using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Media3D;

namespace PNCreator.Converters
{
    [ValueConversion(typeof(Vector3D),typeof(String))]
    public class Vector3DConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Vector3D vector = (Vector3D)value;
            DoubleConverter converter = new DoubleConverter();

            return "X = " + converter.Convert(vector.X, typeof(double), null, null) +
                    "; Y = " + converter.Convert(vector.Y, typeof(double), null, null) +
                    "; Z = " + converter.Convert(vector.Z, typeof(double), null, null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
