using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows;
using System.Windows.Controls;
using Meshes3D;
using System.Windows.Media;
using System.Diagnostics;

namespace PNCreator.ManagerClasses.MeshPicker
{
    public abstract class AbstractMeshPicker
    {
        /// <summary>
        /// Check hit to mesh by cursor and return mesh
        /// </summary>
        /// <param name="point">Point of intersecting with mesh</param>
        /// <param name="viewport">Viewport where all objects are situated</param>
        /// <returns>Returns mesh</returns>
        protected GeometryModel3D GetSelectedObject(Point point, Viewport3D viewport)
        {
            var meshHitResult = (RayMeshGeometry3DHitTestResult)VisualTreeHelper.HitTest(viewport, point);
            if (meshHitResult != null)
            {
                HitPoint = meshHitResult.PointHit;
                return meshHitResult.ModelHit as GeometryModel3D;
            }
            HitPoint = null;
            return null;
        }

        public Point3D? HitPoint
        {
            get;
            set;
        }

        /// <summary>
        /// Select petri net object when you click on it
        /// </summary>
        /// <param name="selectedGeometry">Geometry of object which was selected</param>
        public abstract Mesh3D SelectMesh(Point point, Viewport3D viewport);

        /// <summary>
        /// Select some petri net objects
        /// </summary>
        /// <param name="bounds">A bound which contains these objects</param>
        public abstract List<Mesh3D> SelectMultipleMeshes(RectangleGeometry bounds, Viewport3D viewport);
        
    }
}
