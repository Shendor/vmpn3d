using System.Windows.Media.Media3D;

namespace PNCreator.ManagerClasses.EventManager.Events
{
    public class MeshAddedEventArgs<T> : AbstractEventArgs
    {
        public MeshAddedEventArgs(T mesh)
        {
            Mesh = mesh;
        }

        public T Mesh
        {
            get; set;
        }
    }
}
