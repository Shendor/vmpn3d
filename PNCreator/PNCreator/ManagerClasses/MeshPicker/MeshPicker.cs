using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNCreator.ManagerClasses.MeshPicker
{
    public class MeshPicker : AbstractMeshPicker
    {
        public override Meshes3D.Mesh3D SelectMesh(System.Windows.Point point, System.Windows.Controls.Viewport3D viewport)
        {
            throw new NotImplementedException();
        }

        public override List<Meshes3D.Mesh3D> SelectMultipleMeshes(System.Windows.Media.RectangleGeometry bounds, System.Windows.Controls.Viewport3D viewport)
        {
            throw new NotImplementedException();
        }
    }
}
