using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Meshes3D
{
    public class Ellipsoid : UIElement3D
    {
        protected override void OnUpdateModel()
        {
            GeometryModel3D model = new GeometryModel3D();
            EllipsoidGeometry geometry = new EllipsoidGeometry();
            geometry.XLength = XLength;
            geometry.YLength = YLength;
            geometry.ZLength = ZLength;
            geometry.ThetaDiv = ThetaDiv;
            geometry.PhiDiv = PhiDiv;
            geometry.Center = Center;
            model.Geometry = geometry.Mesh3D;
            model.Material = Material;
            Model = model;
        }

        // The Model property for the Ellipsoid:
        private static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model",
                                        typeof(Model3D),
                                        typeof(Ellipsoid),
                                        new PropertyMetadata(ModelPropertyChanged));

        private static void ModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Ellipsoid ellipsoid = (Ellipsoid)d;
            ellipsoid.Visual3DModel = (Model3D)e.NewValue;
        }

        private Model3D Model
        {
            get { return (Model3D)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        // The material of the Ellipsoid:
        public static readonly DependencyProperty MaterialProperty =
            DependencyProperty.Register("Material",
                                        typeof(Material),
                                        typeof(Ellipsoid),
                                        new PropertyMetadata(new DiffuseMaterial(Brushes.Blue), PropertyChanged));

        public Material Material
        {
            get { return (Material)GetValue(MaterialProperty); }
            set { SetValue(MaterialProperty, value); }
        }

        // The x length of the Ellipsoid:
        public static readonly DependencyProperty XLengthProperty =
            DependencyProperty.Register("XLength",
                                        typeof(double),
                                        typeof(Ellipsoid),
                                        new PropertyMetadata(1.0, PropertyChanged));

        public double XLength
        {
            get { return (double)GetValue(XLengthProperty); }
            set { SetValue(XLengthProperty, value); }
        }

        // The y length of the Ellipsoid:
        public static readonly DependencyProperty YLengthProperty =
            DependencyProperty.Register("YLength",
                                        typeof(double),
                                        typeof(Ellipsoid),
                                        new PropertyMetadata(1.0, PropertyChanged));

        public double YLength
        {
            get { return (double)GetValue(YLengthProperty); }
            set { SetValue(YLengthProperty, value); }
        }

        // The z length of the Ellipsoid:
        public static readonly DependencyProperty ZLengthProperty =
            DependencyProperty.Register("ZLength",
                                        typeof(double),
                                        typeof(Ellipsoid),
                                        new PropertyMetadata(1.0, PropertyChanged));

        public double ZLength
        {
            get { return (double)GetValue(ZLengthProperty); }
            set { SetValue(ZLengthProperty, value); }
        }

        // The ThetaDiv of the Ellipsoid:
        public static readonly DependencyProperty ThetaDivProperty =
            DependencyProperty.Register("ThetaDiv",
                                        typeof(int),
                                        typeof(Ellipsoid),
                                        new PropertyMetadata(20, PropertyChanged));

        public int ThetaDiv
        {
            get { return (int)GetValue(ThetaDivProperty); }
            set { SetValue(ThetaDivProperty, value); }
        }

        // The PhiDiv of the Ellipsoid:
        public static readonly DependencyProperty PhiDivProperty =
            DependencyProperty.Register("PhiDiv",
                                        typeof(int),
                                        typeof(Ellipsoid),
                                        new PropertyMetadata(20, PropertyChanged));

        public int PhiDiv
        {
            get { return (int)GetValue(PhiDivProperty); }
            set { SetValue(PhiDivProperty, value); }
        }

        // The center of the Ellipsoid:
        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register("Center",
                                        typeof(Point3D),
                                        typeof(Ellipsoid),
                                        new PropertyMetadata(new Point3D(0.0, 0.0, 0.0), PropertyChanged));

        public Point3D Center
        {
            get { return (Point3D)GetValue(CenterProperty); }
            set { SetValue(CenterProperty, value); }
        }

        private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Ellipsoid ellipsoid = (Ellipsoid)d;
            ellipsoid.InvalidateModel();
        }
    }
}

