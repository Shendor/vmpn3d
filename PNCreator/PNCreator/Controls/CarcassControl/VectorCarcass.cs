using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.Modules.FormulaBuilder;
using System.Windows.Media.Media3D;
using _3DTools;
using System.Windows;
using System.Windows.Media;
using Meshes3D;
using PNCreator.Controls.VectorControl;

namespace PNCreator.Controls.CarcassControl
{
    public class VectorCarcass : VectorProjectionCarcass
    {

        public VectorCarcass()
            : base()
        {
            //new Axises3D().AddToViewport3D(this.Viewport3D);
            //ViewportOrientation = new Point3D(1, 1, 1);
            // BuildCarcass(new SphereCarcass(), false);

            SetDirectionalLight();
        }

        public VectorData GetVectorData(CustomVector vector)
        {
            VectorData = new VectorData();
            VectorData.Name = vector.Name.Text;
            VectorData.Length = MathUtils.VectorLength(vector.Vector);
            VectorData.StartPoint = vector.StartPoint;
            VectorData.EndPoint = vector.EndPoint;
            VectorData.Projections = CalculateVectorProjection(vector);
            VectorData.Angles = CalculateVectorAngles(vector);
            VectorData.Color = vector.Color;
            return VectorData;
        }

        public void UnselectVector()
        {
            if (selectedVectorIndex != -1)
            {
                SelectedVector.ResetMaterial();
                selectedVectorIndex = -1;
            }
        }

        protected override void SelectObject(GeometryModel3D selectedGeometry)
        {
            base.SelectObject(selectedGeometry);

            //if (SelectedPoint != null)
            //{
            //    if (selectedVectorIndex != -1)
            //    {
            //        vectors[selectedVectorIndex].ResetMaterial();
            //        selectedVectorIndex = -1;
            //    }
            //    SelectVector();

            //    return;
            //}

            //if (selectedVectorIndex != -1)
            //    vectors[selectedVectorIndex].ResetMaterial();
            //if (selectedGeometry != null)
            //{
            //    selectedObject = selectedGeometry;
            //    selectedVectorIndex = vectors.FindIndex(FindModel);

            //    if (selectedVectorIndex != -1)
            //    {
            //        GetVectorData(vectors[selectedVectorIndex]);
            //        vectors[selectedVectorIndex].Color = PNCreator.ManagerClasses.PNColors.Selection;
            //    }
            //   // selectedObject = null;
            //}

            //if (selectedVector != null)
            //    selectedVector.ResetMaterial();
            if (selectedGeometry != null)
            {
                selectedVector = null;
                selectedObject = selectedGeometry;
                foreach (List<CustomVector> vectorList in vectors.Values)
                {
                    selectedVector = vectorList.Find(FindModel);
                    if (selectedVector != null)
                    {
                        GetVectorData(selectedVector);
                        selectedVector.Color = PNCreator.ManagerClasses.PNColors.Selection;
                        break;
                    }
                }


                // selectedObject = null;
            }
        }

        public VectorData VectorData
        {
            get;
            set;
        }

        public List<VectorAngle> CalculateVectorAngles(CustomVector vector)
        {
            if (SelectedVector == null)
                return null;

            List<VectorAngle> vectorAngles = new List<VectorAngle>();
            double angle = 0;

            // Bad
            for (int i = 0; i < vectors[VectorType.Axis].Count; i++)
            {
                CustomVector axis = vectors[VectorType.Axis][i];
                angle = MathUtils.AngleBetweenVectors(vector.Vector, axis.Vector);
//                angle = MathUtils.TranslateRadianToAngle(radians);
                vectorAngles.Add(new VectorAngle(axis.Name.Text, angle));
            }

            VectorData.Color = SelectedVector.Color;

            //angle = MathUtils.RadiansBetweenVectors(new Vector3D(0, VectorData.Projections.YOZ.X, VectorData.Projections.YOZ.Y),
            //                                      new Vector3D(VectorData.Projections.XOY.X, VectorData.Projections.XOY.Y, 0));
            //vectorAngles.Add(new VectorAngle("XOY : YOZ", MathUtils.TranslateRadianToAngle(angle)));

            //angle = MathUtils.RadiansBetweenVectors(new Vector3D(0, VectorData.Projections.YOZ.X, VectorData.Projections.YOZ.Y),
            //                                   new Vector3D(VectorData.Projections.XOZ.X, 0, VectorData.Projections.XOZ.Y));
            //vectorAngles.Add(new VectorAngle("XOZ : YOZ", MathUtils.TranslateRadianToAngle(angle)));

            //angle = MathUtils.RadiansBetweenVectors(new Vector3D(VectorData.Projections.XOY.X, VectorData.Projections.XOY.Y, 0),
            //                                   new Vector3D(VectorData.Projections.XOZ.X, 0, VectorData.Projections.XOZ.Y));
            //vectorAngles.Add(new VectorAngle("XOZ : XOZ", MathUtils.TranslateRadianToAngle(angle)));



            //angle = MathUtils.RadiansBetweenVectors(new Vector3D(0, VectorData.Projections.OY, 0),
            //                                    new Vector3D(VectorData.Projections.XOY.X, VectorData.Projections.XOY.Y, 0));
            //vectorAngles.Add(new VectorAngle("XOY : OY", MathUtils.TranslateRadianToAngle(angle)));

            //angle = MathUtils.RadiansBetweenVectors(new Vector3D(0, VectorData.Projections.OY, 0),
            //                                   new Vector3D(0, VectorData.Projections.YOZ.X, VectorData.Projections.YOZ.Y));
            //vectorAngles.Add(new VectorAngle("YOZ : OY", MathUtils.TranslateRadianToAngle(angle)));
            return vectorAngles;
        }


        List<VectorProjection> CalculateVectorProjection(CustomVector vector)
        {
            List<VectorProjection> projections = new List<VectorProjection>();
            for (int i = 0; i < vectors[VectorType.Axis].Count; i++)
            {
                CustomVector axis = vectors[VectorType.Axis][i];
                projections.Add(new VectorProjection(vector.Vector, axis.Vector)
                {
                    VectorName = axis.Name.Text
                });
            }

            return projections;
        }
    }



    public class VectorData
    {
        public Color Color
        {
            get;
            set;
        }

        public List<VectorAngle> Angles
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public double Length
        {
            get;
            set;
        }

        public Point3D StartPoint
        {
            get;
            set;
        }

        public Point3D EndPoint
        {
            get;
            set;
        }

        public List<VectorProjection> Projections
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class VectorAngle
    {
        public VectorAngle(string name, double angle)
        {
            VectorName = name;
            Angle = angle;
        }

        public string VectorName
        {
            get;
            set;
        }

        public double Angle
        {
            get;
            set;
        }
    }

    public class VectorProjection
    {
        public VectorProjection(Vector3D origin, Vector3D vector)
        {
            vector.Normalize();
            Projection = MathUtils.VectorProjectionOnVector(origin, vector);
        }


        public String VectorName
        {
            get;
            set;
        }

        public double Projection
        {
            get;
            set;
        }
    }
}
