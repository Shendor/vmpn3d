using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using PNCreator.ManagerClasses;

namespace PNCreator.Controls.CarcassControl
{
    class CustomPoint : CarcassPoint
    {
        public CustomPoint(Point3D point)
            : base(point)
        {
            MeshFilename = "/Content/Meshes/Location/HPSphere.3ds";
            Mesh = PNObjectMesh.GetMesh(PNDocument.ApplicationPath + MeshFilename);
            this.Geometry.BackMaterial = Material;
        }

        public CustomPoint(Point3D point, Geometry3D mesh)
            : base(point)
        {
            Mesh = mesh as MeshGeometry3D;
            this.Geometry.BackMaterial = Material;
        }
    }
}
