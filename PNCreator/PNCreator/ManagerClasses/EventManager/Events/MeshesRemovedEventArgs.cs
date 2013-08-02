using System.Collections.Generic;

namespace PNCreator.ManagerClasses.EventManager.Events
{
    public class MeshesRemovedEventArgs<T> : AbstractEventArgs
    {
        public MeshesRemovedEventArgs(List<T> meshes)
        {
            Meshes = meshes;
        }

        public List<T> Meshes
        {
            get; set;
        }
    }
}
