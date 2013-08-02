using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Meshes3D;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace PNCreator.Controls.CarcassControl
{
    class SpherePoint : CarcassPoint
    {
        public SpherePoint(Point3D point):base(point)
        {
            Mesh = new EllipsoidGeometry().Mesh3D;

            //this.Content = new GeometryModel3D(mesh, material);
            
        }

    }
}
