namespace Meshes3D
{
    public interface IMesh
    {
        System.Windows.Media.Media3D.Material Material { get; set; }
        System.Windows.Media.Media3D.MeshGeometry3D Mesh { get; set; }

        System.Windows.Media.Media3D.Point3D Position { get; set; }
        double AngleX { get; set; }
        double AngleY { get; set; }
        double AngleZ { get; set; }
        double Size { get; set; }
    }
}
