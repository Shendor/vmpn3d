using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using _3DTools;

namespace Meshes3D
{
    public class DirectArcMesh
    {
        private MeshGeometry3D mesh;
        public DirectArcMesh()
        {
            mesh = new MeshGeometry3D();
        }

        // Get-only property generates MeshGeometry3D object:
        public MeshGeometry3D Mesh3D
        {
            get { return mesh; }
        }

        public MeshGeometry3D GetMesh3D(Point3D startPont, Point3D endPoint)
        {
            /*mesh.Positions.Add(new Point3D(18.7, 3, 0));        // A
            
            mesh.Positions.Add(new Point3D(17.8, 3.3, 0));      // B
            mesh.Positions.Add(new Point3D(18, 3.1, 0));        // C
            
            mesh.Positions.Add(new Point3D(18, 2.9, 0));        // D
            mesh.Positions.Add(new Point3D(17.8, 2.7, 0));      // E

            mesh.Positions.Add(new Point3D(14, 2.9, 0));        // F
            mesh.Positions.Add(new Point3D(14, 3.1, 0));        // G
            
            mesh.Positions.Add(new Point3D(10, 2.9, 0));        // H
            mesh.Positions.Add(new Point3D(10, 3.1, 0));        // I*/

            mesh.Positions.Add(endPoint);        // A

            mesh.Positions.Add(new Point3D(endPoint.X - 0.9, endPoint.Y + 0.3, endPoint.Z));      // B
            mesh.Positions.Add(new Point3D(endPoint.X - 0.7, endPoint.Y + 0.1, endPoint.Z));      // C

            mesh.Positions.Add(new Point3D(endPoint.X - 0.7, endPoint.Y - 0.1, endPoint.Z));      // D
            mesh.Positions.Add(new Point3D(endPoint.X - 0.9, endPoint.Y - 0.3, endPoint.Z));      // E

            mesh.Positions.Add(new Point3D((startPont.X + endPoint.X) / 2, ((startPont.Y + endPoint.Y) / 2) - 0.1, (startPont.Z + endPoint.Z) / 2));   // F
            mesh.Positions.Add(new Point3D((startPont.X + endPoint.X) / 2, ((startPont.Y + endPoint.Y) / 2) + 0.1, (startPont.Z + endPoint.Z) / 2));   // G

            mesh.Positions.Add(new Point3D(startPont.X, startPont.Y - 0.1, startPont.Z));        // H
            mesh.Positions.Add(new Point3D(startPont.X, startPont.Y + 0.1, startPont.Z));        // I

            // triangles  
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(7);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(7);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(8);

            mesh.Freeze();
            return mesh;
        }
    }
}
