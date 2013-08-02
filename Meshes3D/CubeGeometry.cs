using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Meshes3D
{
    public class CubeGeometry
    {
        // Define private fields:
        private double length;
        private double width;
        private double height;
        private Point3D center = new Point3D();


        public CubeGeometry(double length = 0.7, double width = 0.7, double height = 0.7)
        {
            this.length = length;
            this.width = width;
            this.height = height;
        }

        // Define public properties:
        public double Length
        {
            get { return length; }
            set { length = value; }
        }

        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
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
            MeshGeometry3D mesh = new MeshGeometry3D();
            Point3D[] pts = new Point3D[8];

            double hl = 0.5 * Length;
            double hw = 0.5 * Width;
            double hh = 0.5 * Height;

            pts[0] = new Point3D(hl, hh, hw);
            pts[1] = new Point3D(hl, hh, -hw);
            pts[2] = new Point3D(-hl, hh, -hw);
            pts[3] = new Point3D(-hl, hh, hw);
            pts[4] = new Point3D(-hl, -hh, hw);
            pts[5] = new Point3D(-hl, -hh, -hw);
            pts[6] = new Point3D(hl, -hh, -hw);
            pts[7] = new Point3D(hl, -hh, hw);

            for (int i = 0; i < 8; i++)
            {
                pts[i] += (Vector3D)Center;
            }

            // Top surface (0-3):
            for (int i = 0; i < 4; i++)
                mesh.Positions.Add(pts[i]);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);

            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(0);

            //Bottom surface (4-7):
            for (int i = 4; i < 8; i++)
                mesh.Positions.Add(pts[i]);

            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(6);

            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(7);
            mesh.TriangleIndices.Add(4);

            // Front surface (8-11):
            mesh.Positions.Add(pts[0]);
            mesh.Positions.Add(pts[3]);
            mesh.Positions.Add(pts[4]);
            mesh.Positions.Add(pts[7]);

            mesh.TriangleIndices.Add(8);
            mesh.TriangleIndices.Add(9);
            mesh.TriangleIndices.Add(10);

            mesh.TriangleIndices.Add(10);
            mesh.TriangleIndices.Add(11);
            mesh.TriangleIndices.Add(8);

            // Back surface (12-15):
            mesh.Positions.Add(pts[1]);
            mesh.Positions.Add(pts[2]);
            mesh.Positions.Add(pts[5]);
            mesh.Positions.Add(pts[6]);

            mesh.TriangleIndices.Add(12);
            mesh.TriangleIndices.Add(15);
            mesh.TriangleIndices.Add(14);

            mesh.TriangleIndices.Add(14);
            mesh.TriangleIndices.Add(13);
            mesh.TriangleIndices.Add(12);

            // Left surface (16-19):
            mesh.Positions.Add(pts[2]);
            mesh.Positions.Add(pts[3]);
            mesh.Positions.Add(pts[4]);
            mesh.Positions.Add(pts[5]);

            mesh.TriangleIndices.Add(16);
            mesh.TriangleIndices.Add(19);
            mesh.TriangleIndices.Add(18);

            mesh.TriangleIndices.Add(18);
            mesh.TriangleIndices.Add(17);
            mesh.TriangleIndices.Add(16);

            // Right surface (20-23):
            mesh.Positions.Add(pts[0]);
            mesh.Positions.Add(pts[1]);
            mesh.Positions.Add(pts[6]);
            mesh.Positions.Add(pts[7]);

            mesh.TriangleIndices.Add(20);
            mesh.TriangleIndices.Add(23);
            mesh.TriangleIndices.Add(22);

            mesh.TriangleIndices.Add(22);
            mesh.TriangleIndices.Add(21);
            mesh.TriangleIndices.Add(20);

            mesh.Freeze();
            return mesh;
        }
    }
}

