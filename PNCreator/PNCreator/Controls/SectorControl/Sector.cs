using System;
using System.Windows.Media.Media3D;
using PNCreator.ManagerClasses;
using System.Windows.Media;

namespace PNCreator.Controls.SectorControl
{
    public class Sector : Meshes3D.Mesh3D
    {
        public Sector(Point3D point1, Point3D point2, Point3D point3)
        {
            scale.CenterX = point1.X;
            scale.CenterY = point1.Y;
            scale.CenterZ = point1.Z;

            Mesh = Meshes3D.MeshFactory.GetTriangle3D(point1, point2, point3);

        }

        public Sector(double startAngle, double endAngle, Point3D center = new Point3D())
        {
            scale.CenterX = center.X;
            scale.CenterY = center.Y;
            scale.CenterZ = center.Z;

            Mesh = Meshes3D.MeshFactory.GetSector3D(startAngle, endAngle, center);
        }

        protected override void SetMaterial(Color color)
        {
            base.SetMaterial(color);
            Material = PNObjectMaterial.GetPlainMaterial(color);
        }
    }
}
