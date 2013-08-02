using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Meshes3D;

namespace PNCreator.ManagerClasses.MeshTransfromation
{
    public interface IMeshTransformation
    {
        /// <summary>
        /// Translate meshes
        /// </summary>
        /// <param name="meshes"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        void Translate(List<Mesh3D> meshes, double x, double y, double z);

        /// <summary>
        /// Rotate mesh
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="angleX"></param>
        /// <param name="angleY"></param>
        /// <param name="angleZ"></param>
        void Rotate(Mesh3D mesh, double angleX, double angleY, double angleZ);

        /// <summary>
        /// Scale mesh
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="size"></param>
        void Scale(Mesh3D mesh, double size);
        
    }
}
