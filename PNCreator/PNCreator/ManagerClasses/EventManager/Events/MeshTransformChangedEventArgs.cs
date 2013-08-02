using System.Collections.Generic;
using Meshes3D;

namespace PNCreator.ManagerClasses.EventManager.Events
{
    public class MeshTransformChangedEventArgs : AbstractEventArgs
    {
        public MeshTransformChangedEventArgs(List<Mesh3D> meshes)
        {
            Meshes = meshes;
        }

        public MeshTransformChangedEventArgs(Mesh3D mesh)
        {
            Meshes = new List<Mesh3D> {mesh};
        }

        public List<Mesh3D> Meshes
        {
            get; set;
        }
    }
}
