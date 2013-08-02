using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Media.Media3D;
using System.IO;
using System.Globalization;
using Meshes3D;

namespace PNCreator.ManagerClasses
{
    public class PNObjectMesh
    {

        private static string GetMeshFilename(string meshName)
        {
            using (XmlTextReader reader = new XmlTextReader(PNDocument.ApplicationPath + "/Content/MeshesDB/MeshesNames.xml"))
            {
                string filename = "";
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "Mesh")
                        {
                            if (reader.GetAttribute(0).Equals(meshName))
                            {
                                filename = reader.GetAttribute(1);
                                return filename;
                            }
                        }
                    }
                }
                return filename;
            }
        }

        /// <summary>
        /// Get names of meshes
        /// </summary>
        /// <returns></returns>
        public static ICollection<string> GetMeshesNames()
        {
            using (XmlTextReader reader = new XmlTextReader(PNDocument.ApplicationPath + "/Content/MeshesDB/MeshesNames.xml"))
            {
                
                ICollection<string> filename = new List<string>();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "Mesh")
                        {
                            filename.Add(reader.GetAttribute(0));
                        }
                    }
                }
                return filename;
            }
        }

        public static MeshGeometry3D GetMesh(string filename)
        {
            try
            {
                return MeshReader.GetMesh(filename);
            }
            catch
            {
                return null; // probably better return default mesh
            }
        }

        public static void AddMesh(string meshName, string path)
        {
        }
    }
}
