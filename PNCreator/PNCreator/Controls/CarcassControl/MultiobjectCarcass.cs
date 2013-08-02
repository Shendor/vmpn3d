using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using _3DTools;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using PNCreator.ManagerClasses;
using Meshes3D;

namespace PNCreator.Controls.CarcassControl
{
    public class MultiobjectCarcass : AbstractMultiobjectCarcass, IMeshManager
    {
        private List<CustomMesh> meshes;
        private List<CustomMesh> primitives;
        private int selectedMeshIndex;
        private int selectedPrimitiveIndex;

        public MultiobjectCarcass()
            : base()
        {
            selectedMeshIndex = -1;
            selectedPrimitiveIndex = -1;
            carcass = new SphereCarcass();
            meshes = new List<CustomMesh>();
            primitives = new List<CustomMesh>();
        }
        //==================================================================================================
        protected override void SelectObject(GeometryModel3D selectedGeometry)
        {
            UnselectAllObjects();

            //if (selectedPointIndex != -1)
            //    carcass.CarcassPoints[selectedPointIndex].ResetMaterial();
            //if (selectedMeshIndex != -1)
            //    meshes[selectedMeshIndex].ResetMaterial();
            //if (selectedPrimitiveIndex != -1)
            //    primitives[selectedPrimitiveIndex].ResetMaterial();

            if (selectedGeometry != null)
            {
                selectedObject = selectedGeometry;
                if (carcass != null)
                {
                    selectedPointIndex = carcass.CarcassPoints.FindIndex(FindModel);
                }

                if (selectedPointIndex != -1)
                {
                    selectedObject.Material = PNObjectMaterial.GetMaterial(PNColors.Selection);
                }
                else
                {
                    selectedMeshIndex = meshes.FindIndex(FindModel);
                    if (selectedMeshIndex != -1)
                    {
                        selectedObject.Material = PNObjectMaterial.GetMaterial(PNColors.Selection);
                    }
                    else
                    {
                        selectedPrimitiveIndex = primitives.FindIndex(FindModel);
                        if (selectedPrimitiveIndex != -1)
                        {
                            selectedObject.Material = PNObjectMaterial.GetMaterial(PNColors.Selection);
                        }
                    }
                }

                //selectedObject = null;
            }
        }

        public void SelectMesh(int index)
        {
            if (SelectedMesh != null)
            {
                selectedMeshIndex = index;
                SelectedMesh.Material = PNObjectMaterial.GetMaterial(PNColors.Selection);
            }
        }

        public CustomMesh SelectedMesh
        {
            get
            {
                if (selectedMeshIndex != -1 && 
                    meshes.Count != 0 &&
                    meshes.Count != selectedMeshIndex) return meshes[selectedMeshIndex];
                else return null;
            }
        }

        public List<CustomMesh> Meshes
        {
            get { return meshes; }
            set { meshes = value; }
        }

        public CustomMesh SelectedPrimitive
        {
            get
            {
                if (selectedPrimitiveIndex != -1) return primitives[selectedPrimitiveIndex];
                else return null;
            }
        }

        public List<CustomMesh> Primitives
        {
            get { return primitives; }
            set { primitives = value; }
        }

        public CarcassPoint SelectedPoint
        {
            get
            {
                if (carcass == null) return null;
                else
                    if (selectedPointIndex != -1)
                        return carcass.CarcassPoints[selectedPointIndex];
                    else return null;
            }
            set
            {
                selectedPointIndex = carcass.CarcassPoints.IndexOf(value);
            }
        }

        public virtual void UnselectAllObjects()
        {
            if (selectedPointIndex != -1)
                carcass.CarcassPoints[selectedPointIndex].ResetMaterial();
            if (selectedMeshIndex != -1)
                meshes[selectedMeshIndex].ResetMaterial();
            if (selectedPrimitiveIndex != -1)
                primitives[selectedPrimitiveIndex].ResetMaterial();

            selectedPointIndex = -1;
            selectedMeshIndex = -1;
            selectedPrimitiveIndex = -1;
        }

        public void AddPrimitive(CustomMesh primitive)
        {
            //MeshGeometry3D m = mesh.Geometry.Geometry as MeshGeometry3D;
            //for (int i = 0; i < m.Positions.Count; i++)
            //{
            //    m.Positions[i] = new Point3D(m.Positions[i].X * ViewportOrientation.X,
            //                                 m.Positions[i].Y * ViewportOrientation.Y,
            //                                 m.Positions[i].Z * ViewportOrientation.Z);
            //}

            //carcass.CarcassPoints.Add((CarcassPoint)mesh);
            //Viewport3D.Children.Add(mesh);

            primitive.Position = new Point3D(primitive.Position.X * ViewportOrientation.X,
                                             primitive.Position.Y * ViewportOrientation.Y,
                                             primitive.Position.Z * ViewportOrientation.Z);

            primitives.Add(primitive);
            Viewport3D.Children.Add(primitive);
        }

        public void AddMesh(Mesh3D mesh)
        {
            //MeshGeometry3D m = mesh.Geometry.Geometry as MeshGeometry3D;
            //for (int i = 0; i < m.Positions.Count; i++)
            //{
            //    m.Positions[i] = new Point3D(m.Positions[i].X * ViewportOrientation.X,
            //                                 m.Positions[i].Y * ViewportOrientation.Y,
            //                                 m.Positions[i].Z * ViewportOrientation.Z);
            //}

            //carcass.CarcassPoints.Add((CarcassPoint)mesh);
            //Viewport3D.Children.Add(mesh);

            mesh.Position = new Point3D(mesh.Position.X * ViewportOrientation.X,
                                             mesh.Position.Y * ViewportOrientation.Y,
                                             mesh.Position.Z * ViewportOrientation.Z);
            mesh.Size = 3;
            meshes.Add((CustomMesh)mesh);  // Why I cast without verification ?
           // AddChildrenMeshes((CustomMesh)mesh);
            Viewport3D.Children.Add(mesh);
        }

        private void AddChildrenMeshes(CustomMesh childObject)
        {
            foreach (Visual3D child in childObject.Children)
            {
                if (child is CustomMesh)
                {
                    AddChildrenMeshes((CustomMesh)child);
                    meshes.Add((CustomMesh)child);
                }
            }
        }

        public void RemoveSelectedMesh()
        {
            meshes.Remove(SelectedMesh);
            Viewport3D.Children.Remove(SelectedMesh);
            selectedMeshIndex = -1;
        }

        public void RemoveMesh(Mesh3D mesh)
        {
            //carcass.CarcassPoints.Remove((CarcassPoint)mesh);
            //Viewport3D.Children.Remove(mesh);

            meshes.Remove((CustomMesh)mesh);
            Viewport3D.Children.Remove(mesh);
        }

        public void RemoveSelectedPrimitive()
        {
            primitives.Remove(SelectedPrimitive);
            Viewport3D.Children.Remove(SelectedPrimitive);
            selectedPrimitiveIndex = -1;
        }

        #region Carcass Manager

        //public void ShowSphereCarcass()
        //{
        //    if (carcass == null) return;
        //    carcass.BuildCarcass(this.Viewport3D);
        //}

        #endregion

    }
}
