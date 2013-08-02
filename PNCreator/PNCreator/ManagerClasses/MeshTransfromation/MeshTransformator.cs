using System.Collections.Generic;
using Meshes3D;
using System.Windows.Media.Media3D;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.ManagerClasses.MeshTransfromation
{
    public class MeshTransformator : IMeshTransformation
    {
        private readonly EventPublisher eventPublisher;

        public MeshTransformator()
        {
            eventPublisher = App.GetObject<EventPublisher>();
        }

        /// <summary>
        /// Change arc position
        /// </summary>
        /// <param name="meshes"></param>
        private void ChangeArcsPosition(List<Mesh3D> meshes)
        {

            foreach (Arc3D arc in PNObjectRepository.GetPNObjects<Arc3D>())
            {
                foreach (Mesh3D mesh in meshes)
                {
                    var pnObject = (PNObject)mesh;
                    if (!(pnObject is Arc3D))
                    {
                        if (pnObject.ID == arc.StartID)
                            arc.ChangeStartPoint(pnObject.Position);
                        else if (pnObject.ID == arc.EndID)
                            arc.ChangeEndPoint(pnObject.Position);
                    }
                }
            }
        }

        #region IMeshTransformation Members

        public void Translate(List<Mesh3D> meshes, double x, double y, double z)
        {
            if (meshes.Count == 1)
            {
                meshes[0].Position = new Point3D(x, y, z);
            }
            else
            {
                foreach (Mesh3D mesh in meshes)
                {
                    Point3D position = mesh.Position;
                    mesh.Position = new Point3D(position.X + x, position.Y + y, position.Z + z);
                }
            }

            ChangeArcsPosition(meshes);
            eventPublisher.ExecuteEvents(new MeshTransformChangedEventArgs(meshes));
        }

        public void Rotate(Mesh3D mesh, double angleX, double angleY, double angleZ)
        {
            if (mesh != null)
            {
                mesh.RotateX.Angle = angleX;
                mesh.RotateY.Angle = angleY;
                mesh.RotateZ.Angle = angleZ;

                eventPublisher.ExecuteEvents(new MeshTransformChangedEventArgs(mesh));
            }
        }

        public void Scale(Mesh3D mesh, double size)
        {
            if (mesh != null)
            {
                mesh.Size = size;
                eventPublisher.ExecuteEvents(new MeshTransformChangedEventArgs(mesh));
            }
        }

        #endregion
    }
}
