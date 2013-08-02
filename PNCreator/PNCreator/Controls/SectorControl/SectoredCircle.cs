using System.Linq;
using System.Windows.Media.Media3D;
using System.Windows;
using PNCreator.ManagerClasses;
using _3DTools;

namespace PNCreator.Controls.SectorControl
{
    public class SectoredCircle : Meshes3D.Mesh3D
    {
        public static readonly int DEFAULT_RADIUS = 40;

        private double[] angles;

        public SectoredCircle(Point3D position = new Point3D(),
                                        Vector3D rotationVector = new Vector3D(), double scale = 0.8)
            
        {
            ReBuild();
            this.Transform = this.Geometry.Transform;
           
            this.Position = position;
            this.AngleX = rotationVector.X;
            this.AngleY = rotationVector.Y;
            this.AngleZ = rotationVector.Z;

            this.Size = scale;
        }

        #region SectorCountProperty

        public static readonly DependencyProperty SectorCountProperty = DependencyProperty.Register("SectorCount", typeof(int), typeof(SectoredCircle),
                                                                new PropertyMetadata(4, OnSectorCountChanged, OnSectorCountCoerce));

        private static void OnSectorCountChanged(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            SectoredCircle sectorCircle = (SectoredCircle)sender;
            sectorCircle.SectorCount = (int)arg.NewValue; // Do I really need it ?
            sectorCircle.ReBuild();
            //sectorCircle.RaiseEvent(new RoutedEventArgs() { RoutedEvent = SectorCountChangedEvent });
        }

        private static object OnSectorCountCoerce(DependencyObject source, object value)
        {
            if ((int)value >= 0)
                return value;
            else return source.GetValue(SectorCountProperty);
        }

        public int SectorCount
        {
            get
            {
                return (int)GetValue(SectorCountProperty);
            }
            set
            {
                SetValue(SectorCountProperty, value);
            }
        }

        public double[] Angles
        {
            get
            {
                return angles;
            }
            set
            {
                angles = value;
            }
        }

        #endregion

        //#region SectorCountChangedEvent

        //public static readonly RoutedEvent SectorCountChangedEvent = EventManager.RegisterRoutedEvent("SectorCountChanged", RoutingStrategy.Direct,
        //                                                                    typeof(RoutedEventHandler), typeof(SectoredCircleController));
        //public event RoutedEventHandler SectorCountChanged
        //{
        //    add { AddHandler(SectorCountChangedEvent, value); }
        //    remove { RemoveHandler(SectorCountChangedEvent, value); }
        //}

        //#endregion

        private void ReBuild()
        {
            //this.Children.Clear();

            //Point3D[] circlePoints = MathUtils.GetCirclePoints(SectorCount, new Point3D(), DEFAULT_RADIUS);
            //for (int i = 0; i < SectorCount - 1; i++)
            //{
            //    AddSector(circlePoints, i, i + 1);
            //}
            //AddSector(circlePoints, SectorCount - 1, 0);

            this.Children.Clear();

            angles = new double[SectorCount];

            double step = 360 / SectorCount;
            double angle = 0;
            Point3D center = new Point3D();
            for (int i = 0; i < SectorCount; i++, angle += step)
            {
                AddSector(angle, (i == SectorCount - 1) ? 360 - angle : step, center);
                angles[i] = angle;
            }
            //AddSector(angle, 360 - angle, center);
        }

        private void AddSector(double startAngle, double endAngle, Point3D center)
        {
            var sector = new Sector(startAngle, endAngle, center) {MaterialColor = PNColors.RandomColor};

            Children.Add(sector);
        }

        private void AddSector(Point3D[] circlePoints, int startPointIndex, int endPointIndex)
        {
            var sector = new Sector(new Point3D(), circlePoints[startPointIndex], circlePoints[endPointIndex])
                {
                    MaterialColor = PNColors.RandomColor
                };

            Children.Add(sector);
        }

        //public Sector SelectedSector
        //{
        //    get { return (Sector)this.Children[0]; }
        //}

        public Sector GetSectorByMesh(GeometryModel3D mesh)
        {
            if (mesh == null) return null;
            Sector sector = null;
            try
            {
                sector = (Sector)this.Children.First(model =>
                {
                    if (((GeometryModel3D)((ModelVisual3D)model).Content) == mesh) return true;
                    return false;
                });
            }
            catch
            {
            }
            return sector;
        }

        public double GetAngle(Sector sector)
        {
            Point3DCollection points = GetPoints(sector);
            return MathUtils.AngleBetweenVectors((Vector3D)points[1], (Vector3D)points[points.Count - 1]);
        }

        public double GetAngle(int index)
        {
            return GetAngle((Sector)this.Children[index]);
        }

        public void SetAngle(Sector sector, double angle)
        {
            int index = this.Children.IndexOf(sector);
            if (index == -1) return;
           
            int count = this.Children.Count;
            if (angle <= 0) return;

            double startAngle = angles[index];

            sector.Mesh = Meshes3D.MeshFactory.GetSector3D(startAngle, angle, new Point3D());
            
            index = (index == count - 1) ? -1 : index;
            
            if (index < count)
            {

                Sector neighborSector = (Sector)this.Children[index + 1];
                Point3DCollection sectorPoints = GetPoints(sector);
                Point3DCollection neighborSectorPoints = GetPoints(neighborSector);
                startAngle =  startAngle + angle;
                double endAngle = MathUtils.AngleBetweenVectors((Vector3D)sectorPoints[sectorPoints.Count - 1],
                                        (Vector3D)neighborSectorPoints[neighborSectorPoints.Count - 1]);
                startAngle = (startAngle > 360) ? startAngle - 360 : startAngle;

                neighborSector.Mesh = Meshes3D.MeshFactory.GetSector3D(startAngle, endAngle, new Point3D());
                angles[index + 1] = startAngle;
            }

        }

        public void SetAngle(int index, double angle)
        {
            SetAngle((Sector)this.Children[index], angle);
        }

        //private void SetAngle(double angle, int sectorIndex, int pointIndex, int direction)
        //{
        //    Sector sector = (Sector)this.Children[sectorIndex];
        //    double currentAngle = GetAngle(sector);
        //    angle -= currentAngle;
        //    if (angle == 0) return;

        //    Point3DCollection points = GetPoints(sector);
        //    points[pointIndex] = MathUtils.RotatePoint3D(direction * angle, points[pointIndex]);
        //}

        private Point3DCollection GetPoints(Sector sector)
        {
            return ((MeshGeometry3D)sector.Geometry.Geometry).Positions;
        }
    }
}
