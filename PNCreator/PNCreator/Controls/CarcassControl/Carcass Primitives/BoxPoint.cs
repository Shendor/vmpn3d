using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.Modules.FormulaBuilder;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using Meshes3D;

namespace PNCreator.Controls.CarcassControl
{
    class BoxPoint : CarcassPoint
    {
        public BoxPoint(Point3D point)
            : base(point)
        {

            Mesh = new CubeGeometry().Mesh3D;

          //  this.Geometry = new GeometryModel3D(new CubeGeometry().Mesh3D, Material);

        }

        public BoxPoint()
            : this(new Point3D())
        {

        }
    }
}
