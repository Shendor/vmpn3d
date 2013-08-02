using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Meshes3D
{
    public class EllipsoidGeometry
    {
        // Define private fields:
        private double xLength = 0.05;
        private double yLength = 0.05;
        private double zLength = 0.05;
        private int thetaDiv = 12;
        private int phiDiv = 12;
        private Point3D center = new Point3D();

        // Define public properties:
        public double XLength
        {
            get { return xLength; }
            set { xLength = value; }
        }

        public double YLength
        {
            get { return yLength; }
            set { yLength = value; }
        }

        public double ZLength
        {
            get { return zLength; }
            set { zLength = value; }
        }

        public int ThetaDiv
        {
            get { return thetaDiv; }
            set { thetaDiv = value; }
        }
        public int PhiDiv
        {
            get { return phiDiv; }
            set { phiDiv = value; }
        }

        public Point3D Center
        {
            get { return center; }
            set { center = value; }
        }

        // Get-only property generates MeshGeometry3D object:
        public MeshGeometry3D Mesh3D
        {
            get { return GetMesh3D(); }
        }

        private MeshGeometry3D GetMesh3D()
        {
            double dt = 360.0 / ThetaDiv;
            double dp = 180.0 / PhiDiv;

            MeshGeometry3D mesh = new MeshGeometry3D();

            for (int i = 0; i <= PhiDiv; i++)
            {
                double phi = i * dp;

                for (int j = 0; j <= ThetaDiv; j++)
                {
                    double theta = j * dt;
                    mesh.Positions.Add(GetPosition(theta, phi));
                }
            }

            for (int i = 0; i < PhiDiv; i++)
            {
                for (int j = 0; j < ThetaDiv; j++)
                {
                    int x0 = j;
                    int x1 = (j + 1);
                    int y0 = i * (ThetaDiv + 1);
                    int y1 = (i + 1) * (ThetaDiv + 1);

                    mesh.TriangleIndices.Add(x0 + y0);
                    mesh.TriangleIndices.Add(x0 + y1);
                    mesh.TriangleIndices.Add(x1 + y0);

                    mesh.TriangleIndices.Add(x1 + y0);
                    mesh.TriangleIndices.Add(x0 + y1);
                    mesh.TriangleIndices.Add(x1 + y1);
                }
            }

            mesh.Freeze();
            return mesh;
        }

        private Point3D GetPosition(double theta, double phi)
        {
            theta *= Math.PI / 180.0;
            phi *= Math.PI / 180.0;

            double x = XLength * Math.Sin(theta) * Math.Sin(phi);
            double y = YLength * Math.Cos(phi);
            double z = ZLength * Math.Cos(theta) * Math.Sin(phi);

            Point3D pt = new Point3D(x, y, z);
            pt += (Vector3D)Center;

            return pt;
        }
    }
}
