using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace PNCreator.Controls.CarcassControl
{
    public class CustomMesh : Meshes3D.Mesh3D
    {
        protected string meshFilename;

        public CustomMesh(Point3D point)
        {
            Position = new Point3D(point.X, point.Y, point.Z);

            Material = ManagerClasses.PNObjectMaterial.GetMaterial(PNCreator.ManagerClasses.PNColors.CarcassPoint);

            MeshFilename = Properties.Resources.DefaultMeshFilename;
            Mesh = ManagerClasses.PNObjectMesh.GetMesh(PNCreator.ManagerClasses.PNDocument.ApplicationPath + MeshFilename);
            Geometry.BackMaterial = Material;
        }

        public CustomMesh(Point3D point, Geometry3D mesh, string meshFilename)
        {
            Position = new Point3D(point.X, point.Y, point.Z);

            Material = ManagerClasses.PNObjectMaterial.GetMaterial(PNCreator.ManagerClasses.PNColors.CarcassPoint);

            MeshFilename = meshFilename;
            Mesh = mesh as MeshGeometry3D;
            Geometry.BackMaterial = Material;
        }

        public string MeshFilename
        {
            get { return meshFilename; }
            set { meshFilename = value; }
        }

//        public Color MaterialColor
//        {
//            get;
//            set;
//        }

        public override void ResetMaterial()
        {
            Material = ManagerClasses.PNObjectMaterial.GetMaterial(MaterialColor);
        }

        protected override void SetMaterial(Color color)
        {
            Material = ManagerClasses.PNObjectMaterial.GetMaterial(color);
            materialColor = color;
        }
    }
}
