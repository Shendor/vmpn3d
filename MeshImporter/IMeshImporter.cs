using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimatedGraph;

namespace MeshImporter
{
    interface IMeshImporter
    {
        void AddMesh(string name, MeshNode path);
        void RemoveMesh(MeshNode name);
        void RenameMesh(string oldName, string newName);
        void GetMeshes();
    }
}
