using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using PNCreator.PNObjectsIerarchy;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PNCreator.ManagerClasses
{
    class PNObjectMaterial
    {
        #region Texture

        public static MaterialGroup GetTexture(string filename)
        {
            ImageSource img = new BitmapImage(new Uri(filename));
            ImageBrush iBrush = new ImageBrush(img);
            iBrush.AlignmentX = AlignmentX.Right;
            iBrush.AlignmentY = AlignmentY.Center;
            iBrush.TileMode = TileMode.Tile;

            MaterialGroup material = new MaterialGroup();
            material.Children.Add(new DiffuseMaterial(new SolidColorBrush(Colors.Black)));
            material.Children.Add(new EmissiveMaterial(iBrush));

            return material;
        }
        #endregion

        #region Materials

        public static MaterialGroup GetMaterial(Color color)
        {
            MaterialGroup material = new MaterialGroup();
            material.Children.Add(new DiffuseMaterial(new SolidColorBrush(color)));
            material.Children.Add(new SpecularMaterial(Brushes.White, 34));
            return material;
        }

        public static DiffuseMaterial GetPlainMaterial(Color color)
        {
            return new DiffuseMaterial(new SolidColorBrush(color));
        }

        public static MaterialGroup GetSemiTransparentMaterial(Color color)
        {
            var material = new MaterialGroup();
            material.Children.Add(new DiffuseMaterial(new SolidColorBrush(color)
                {
                    Opacity = PNCreator.Modules.Properties.PNProperties.OpacityLevel
                }));

            return material;
        }


        #endregion
    }
}
