using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Meshes3D;
using PNCreator.PNObjectsIerarchy;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Input;

namespace PNCreator.ManagerClasses.MeshPicker
{
    public class PNObjectPicker : AbstractMeshPicker
    {
        private List<Mesh3D> selectedObjects = new List<Mesh3D>();

        /// <summary>
        /// 
        /// </summary>
        public PNObject SelectedObject
        {
            get
            {
                return (selectedObjects.Count == 1) ? (PNObject)selectedObjects[0] : null;
            }
            set
            {
                if (selectedObjects.Count == 1)
                {
                    selectedObjects[0] = value;
                }
                else
                {
                    selectedObjects = new List<Mesh3D>();
                    selectedObjects.Add(value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Mesh3D> SelectedObjects
        {
            get
            {
                return selectedObjects;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="viewport"></param>
        /// <returns></returns>
        public override Mesh3D SelectMesh(Point point, Viewport3D viewport)
        {
            selectedObjects = new List<Mesh3D>();
            GeometryModel3D geometry = GetSelectedObject(point, viewport);
            if (geometry == null)
                return null;

            foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
            {
                if (pnObject.Geometry.Equals(geometry))
                {
                    selectedObjects.Add(pnObject);
                    return pnObject;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="viewport"></param>
        /// <returns></returns>
        public override List<Mesh3D> SelectMultipleMeshes(RectangleGeometry bounds, Viewport3D viewport)
        {
            selectedObjects = new List<Mesh3D>();

            foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
            {
                if (bounds.Rect.Contains(_3DTools.MathUtils.Convert3DPoint((Point3D)pnObject.Position, viewport)))
                {
                    if (!pnObject.IsReadOnly)
                    {
                        selectedObjects.Add(pnObject);
                    }
                }
            }

            return selectedObjects;
        }
    }
}
