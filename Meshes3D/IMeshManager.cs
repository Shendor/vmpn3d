using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meshes3D
{
    public interface IMeshManager
    {
        void AddMesh(Mesh3D mesh);
        void RemoveMesh(Mesh3D mesh);
    }
}
