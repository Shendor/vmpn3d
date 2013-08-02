using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Media3D;

namespace PNCreator.Converters
{
    [ValueConversion(typeof(Point3D),typeof(String))]
    public class PointConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Point3D point = (Point3D)value;
            DoubleConverter converter = new DoubleConverter();

            return "X = " + converter.Convert(point.X, typeof(double), null, null) +
                    "; Y = " + converter.Convert(point.Y, typeof(double), null, null) +
                    "; Z = " + converter.Convert(point.Z, typeof(double), null, null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
