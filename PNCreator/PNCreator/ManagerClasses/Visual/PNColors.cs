using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace PNCreator.ManagerClasses
{
    public class PNColors
    {
        private static Random colorGenerator = new Random(255);

        private static byte GetColorValue()
        {
            return (byte)colorGenerator.Next(255);
        }

        private static Brush SELECTION_BOX_BRUSH = new SolidColorBrush(Color.FromArgb(0x55, 0xFF, 0xFF, 0xFF));

        public static Color Selection { get { return Colors.WhiteSmoke; } }
        public static Color DiscreteObjects { get { return PNCreator.Modules.Properties.PNProperties.DiscreteObjetcsColor; } }
        public static Color ContinuousObjects { get { return PNCreator.Modules.Properties.PNProperties.ContinuousObjetcsColor; } }
        public static Color Membrane { get { return PNCreator.Modules.Properties.PNProperties.MembranesColor; } }

        public static Brush Canvas { get { return Brushes.Black; } }
        public static Brush TextBrush { get { return (SolidColorBrush)Application.Current.Resources["DarkTextBrush"]; } }
        public static Color CarcassPoint { get { return Colors.DodgerBlue; } }
        public static Brush Transparent { get { return Brushes.Transparent; } }

        public static Brush SelectionBox { get { return SELECTION_BOX_BRUSH; } }

        public static Color RandomColor { get { return Color.FromRgb(GetColorValue(), GetColorValue(), GetColorValue()); } }
    }
}
