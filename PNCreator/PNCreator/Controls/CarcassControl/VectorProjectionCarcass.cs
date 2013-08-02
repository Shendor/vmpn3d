using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using PNCreator.ManagerClasses;
using System.Windows;
using _3DTools;
using System.Windows.Controls;
using System.Windows.Media;
using PNCreator.Controls.VectorControl;

namespace PNCreator.Controls.CarcassControl
{
    public class VectorProjectionCarcass : MultiobjectCarcass, ICustomVectorManager
    {
        //protected List<CustomVector> vectors;
        protected Dictionary<VectorType, List<CustomVector>> vectors;
        protected List<CustomVector> selectedVectors;
        protected int selectedVectorIndex;
        protected CustomVector selectedVector;

        public VectorProjectionCarcass()
            : base()
        {

            vectors = new Dictionary<VectorType, List<CustomVector>>();
            vectors.Add(VectorType.Vector, new List<CustomVector>());
            vectors.Add(VectorType.Axis, new List<CustomVector>());

            selectedVectors = new List<CustomVector>();
            selectedVectorIndex = -1;
        }

        public ProjectionType Projection
        {
            get;
            set;
        }

        public Dictionary<VectorType, List<CustomVector>> Vectors
        {
            get { return vectors; }
            set
            {
                this.RemoveAllVectors();
                foreach (var vector in value[VectorType.Vector])
                {
                    AddVector(VectorType.Vector, vector);
                }
                foreach (var vector in value[VectorType.Axis])
                {
                    AddVector(VectorType.Axis, vector);
                }
            }
        }

        public CustomVector SelectedVector
        {
            get
            {
                //if (selectedVectorIndex == -1) return null;
                //else return vectors[VectorType.Vector][selectedVectorIndex];
                return selectedVector;
            }
        }

        public String SelectedVectorName
        {
            get
            {
                if (selectedVectorIndex == -1) return null;
                else return vectors[VectorType.Vector][selectedVectorIndex].Name.Text;
            }
        }

        public List<CustomVector> SelectedVectors
        {
            get { return selectedVectors; }
        }

        public int GetIndexOfSelectedVector(VectorType vectorType)
        {
            if (selectedVector == null) return -1;
            else
            {
                return this.Vectors[vectorType].IndexOf(selectedVector);
            }
        }

        public void SelectVector()
        {
            selectedVectors.Clear();

            for (int i = 0; i < vectors.Count; i++)
            {
                //TODO: implement vectors[i].StartPoint.GetHashCode().Equals(this.SelectedPoint.GetHashCode())
                //if (vectors[i].EndPoint.ID.Equals(this.SelectedPoint.ID))
                //{
                //    selectedVectors.Add(vectors[i]);
                //}
            }
        }

        public void HideUnusedPoints()
        {
            List<int> indexes = new List<int>();
            for (int i = 0; i < vectors.Count; i++)
            {
                int index = -1;

                //for (int j = 0; j < Carcass.CarcassPoints.Count; j++)
                //    if (this.Carcass.CarcassPoints[j].ID.Equals(vectors[i].EndPoint.ID))
                //    {
                //        index = j;
                //        j = Carcass.CarcassPoints.Count;
                //    }

                if (index != -1)
                {
                    indexes.Add(index);
                }
            }
            for (int i = 0; i < this.Carcass.CarcassPoints.Count; i++)
            {
                if (!indexes.Contains(i))
                    if (this.Carcass.CarcassPoints[i].GetType().Equals(typeof(CarcassPoint)))
                        ((CarcassPoint)this.Carcass.CarcassPoints[i]).Hide();
            }
            indexes.Clear();
        }

        public void ShowAllPoints()
        {
            for (int i = 0; i < this.Carcass.CarcassPoints.Count; i++)
            {
                if (this.Carcass.CarcassPoints[i].GetType().Equals(typeof(CarcassPoint)))
                    ((CarcassPoint)this.Carcass.CarcassPoints[i]).Show();
            }
        }

        public void AddVector(VectorType vectorType, string name, Point3D startPoint, Point3D endPoint)
        {
            CustomVector vector =
               new CustomVector(name, new Point3D(startPoint.X * ViewportOrientation.X,
                                                  startPoint.Y * ViewportOrientation.Y,
                                                  startPoint.Z * ViewportOrientation.Z),
                                      new Point3D(endPoint.X * ViewportOrientation.X,
                                                  endPoint.Y * ViewportOrientation.Y,
                                                  endPoint.Z * ViewportOrientation.Z));

            if (vectorType == VectorType.Axis) vector.Color = Colors.LightGray;

            AddVector(vectorType, vector);

            //if (this.SelectedPoint != null)
            //    vector.EndPoint.ID = this.SelectedPoint.ID;
        }

        public void AddVector(VectorType vectorType, CustomVector vector)
        {
            vectors[vectorType].Add(vector);
            this.Viewport3D.Children.Add(vector);
            if (this.Content2D != null)
            {
                this.Content2D.Children.Add(vector.Name);
                vector.ChangeNamePosition(MathUtils.Convert3DPoint(vector.EndPoint, this.Viewport3D));
            }
        }

        [Obsolete()]
        public void AddVector(VectorType vectorType, string name, Color color, Point3D startPoint, Point3D endPoint)
        {
            //TODO: possibility to connect vector with two points
            CustomVector vector = new CustomVector(name, startPoint, endPoint);

            vector.Points[1] = new Point3D(endPoint.X * ViewportOrientation.X,
                                            endPoint.Y * ViewportOrientation.Y,
                                            endPoint.Z * ViewportOrientation.Z);

            vector.SetMaterial(color);

            AddVector(vectorType,vector);
        }

        [Obsolete]
        public void RemoveAllVectorsAt(List<int> vectors)
        {
            foreach (int i in vectors)
            {
                this.Content2D.Children.RemoveAt(i);
                this.Viewport3D.Children.RemoveAt(i);
                vectors.RemoveAt(i);
            }
        }

        public void RemoveAllVectors(VectorType vectorType)
        {
            for (int i = 0; i < vectors[vectorType].Count; i++)
            {
                this.Content2D.Children.Remove(vectors[vectorType][i].Name);
                this.Viewport3D.Children.Remove(vectors[vectorType][i]);
            }
            vectors[vectorType].Clear();
        }

        public void RemoveAllVectors()
        {
            RemoveAllVectors(VectorType.Axis);
            RemoveAllVectors(VectorType.Vector);
        }

        [Obsolete]
        public void RemoveVector(CustomVector vector)
        {
            this.Viewport3D.Children.Remove(vector);
            this.Content2D.Children.Remove(vector.Name);
            vectors[VectorType.Vector].Remove(vector);
            selectedVectorIndex = -1;
            selectedPointIndex = -1;
        }

        [Obsolete]
        public void RemoveVector(int index)
        {
            this.Viewport3D.Children.Remove(Vectors[VectorType.Vector][index]);
            this.Content2D.Children.Remove(Vectors[VectorType.Vector][index].Name);
            vectors[VectorType.Vector].RemoveAt(index);
            selectedVectorIndex = -1;
            selectedPointIndex = -1;
        }

        public virtual void RemoveAll()
        {
            if (Content2D != null) Content2D.Children.Clear();
            RemoveAllVectors();
            selectedVectorIndex = -1;
            selectedPointIndex = -1;
        }

        public override void UnselectAllObjects()
        {
            base.UnselectAllObjects();

            if (selectedVector != null)
                selectedVector.ResetMaterial();

            selectedVector = null;
        }

    }

    public enum ProjectionType
    {
        Front,
        Top,
        Perspective,
        Left
    }

    public enum VectorType
    {
        Vector,
        Axis
    }
}
