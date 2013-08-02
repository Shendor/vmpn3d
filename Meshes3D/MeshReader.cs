using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;

namespace Meshes3D
{
    public class MeshReader
    {
        private static Reader3ds reader3ds = null;

        /// <summary>
        /// Get only one mesh from .3ds file
        /// </summary>
        /// <param name="filename">Full path to .3ds file</param>
        /// <returns>Return one mesh</returns>
        public static MeshGeometry3D GetMesh(string filename)
        {
            if (filename != null)
            {
                reader3ds = new Reader3ds();
                Model3DGroup models = reader3ds.ReadFile(filename);

                foreach (Model3D m in models.Children)
                {
                    ModelVisual3D model = new ModelVisual3D();
                    model.Content = m;

                    if (m.GetType().Equals(typeof(GeometryModel3D)))
                    {
                        return ((MeshGeometry3D)((GeometryModel3D)m).Geometry).Clone();
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Get all meshes from .3ds file
        /// </summary>
        /// <param name="filename">Full path to .3ds file</param>
        /// <param name="defaultMaterial">Default material for model. Can be null</param>
        /// <returns>Returns group of meshes</returns>
        public static Model3DGroup GetModel3DGroup(string filename, MaterialGroup defaultMaterial)
        {
            TranslateTransform3D position = new TranslateTransform3D(0, 0, -100);

            if (filename != null)
            {
                reader3ds = new Reader3ds();

                Model3DGroup models = reader3ds.ReadFile(filename);

                foreach (Model3D m in models.Children)
                {
                    ModelVisual3D model = new ModelVisual3D();
                    model.Content = m;

                    model.Transform = position;
                    if (defaultMaterial != null) ((GeometryModel3D)model.Content).Material = defaultMaterial;
                }
                return models;
            }
            return null;
        }
    }
}
