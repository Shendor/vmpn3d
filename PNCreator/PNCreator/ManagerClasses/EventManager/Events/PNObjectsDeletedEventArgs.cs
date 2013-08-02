using System.Collections.Generic;
using Meshes3D;

namespace PNCreator.ManagerClasses.EventManager.Events
{
    public class PNObjectsDeletedEventArgs : MeshesRemovedEventArgs<Mesh3D>
    {
        public PNObjectsDeletedEventArgs(List<Mesh3D> meshes) : base(meshes)
        {
        }
    }
}
