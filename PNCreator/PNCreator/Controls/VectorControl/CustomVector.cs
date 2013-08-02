using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _3DTools;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PNCreator.Properties;
using System.Windows.Controls;
using System.Windows;
using PNCreator.Controls.CarcassControl;
using PNCreator.ManagerClasses;

namespace PNCreator.Controls.VectorControl
{
    public class CustomVector : ScreenSpaceLines3D
    {
        private const double DEFAULT_THICKNESS = 5;
        private const double ARROW_HEADER_LENGTH = 5;

        private TextBlock name;
        private CarcassPoint startPoint;
        private CarcassPoint endPoint;
        private Color previousColor;

        public CustomVector(string name, Point3D startPoint, Point3D endPoint)
        {
            this.name = new TextBlock();
            this.name.Text = name;

            Points.Add(startPoint);   // 0
            Points.Add(endPoint);  // 1

            //Points.Add(endPoint);  // 2
            //Points.Add(new Point3D());  // 3

            //Points.Add(endPoint);  // 4
            //Points.Add(new Point3D());  // 5  

            ChangeVectorPosition(startPoint, endPoint);

            Thickness = DEFAULT_THICKNESS;
            previousColor = Color = Colors.DodgerBlue; //;PNColors.DiscreteObjects;
        }

        //public CustomVector(string name, CarcassPoint startPoint, CarcassPoint endPoint)
        //{
        //    this.name = new TextBlock();
        //    this.name.Text = name;
        //    //Points.Add(startPoint.Position);
        //    this.startPoint = startPoint;
        //    this.endPoint = endPoint;
        //    Points.Add(startPoint.Position);   // 0
        //    Points.Add(endPoint.Position);  // 1

        //    //Points.Add(endPoint.Position);  // 2
        //    //Points.Add(new Point3D());  // 3

        //    //Points.Add(endPoint.Position);  // 4
        //    //Points.Add(new Point3D());  // 5      

        //    ChangeVectorPosition(endPoint.Position);

        //    Thickness = DEFAULT_THICKNESS;
        //    previousColor = Color = Colors.Orange;
        //}

        public CustomVector()
            : this("", new Point3D(), new Point3D())
        {
        }

        public void SetMaterial(Color color)
        {
            Color = previousColor = color;
        }

        public Vector3D Vector
        {
            get
            {
                return (Vector3D)EndPoint;
            }
        }

        public void ResetMaterial()
        {
            Color = previousColor;
        }

        public Point3D StartPoint
        {
            get
            {
                return Points[0];
            }
            set
            {
                Points[0] = value;
            }
        }

        public Point3D EndPoint
        {
            get
            {
                return Points[1];
            }
            set
            {
                Points[1] = value;
            }
        }

        public void ChangeVectorPosition(Point3D startPosition, Point3D endPosition)
        {
            //if(startPoint != null)
            //startPoint.Position = startPosition;

            //if (endPoint != null)
            //endPoint.Position = endPosition;

            Points[0] = startPosition;
            Points[1] = endPosition;
            //Points[2] = endPosition;
            //Points[4] = endPosition;
            ////endPoint.Position = endPosition;
            ////Point startArrowPoint = new Point(Points[0].X, (Points[0].Y == 0) ? Points[0].Z : Points[0].Y);
            ////Point endArrowPoint = new Point(Points[1].X, (Points[1].Y == 0) ? Points[1].Z : Points[1].Y);

            //double arrowHalfBackLength = Math.Sqrt(ARROW_HEADER_LENGTH * ARROW_HEADER_LENGTH / 3);
            ////lengths of arrow(the biggest triangle)
            //double xlen = Points[1].X - Points[0].X;
            //// Hack. Use a better way to draw arrows
            //double ylen = Points[1].Y - Points[0].Y;
            ////double zlen = Points[1].Z - Points[0].Z;
            //double hip = Math.Sqrt(xlen * xlen + ylen * ylen);

            ////lengths of arrows proections on Axes Oy and Ox
            //double arrLenX = ARROW_HEADER_LENGTH * xlen / hip;
            //double arrLenY = ARROW_HEADER_LENGTH * ylen / hip;
            ////double arrLenZ = ARROW_HEADER_LENGTH * zlen / hip;
            ////Back point of the arrow header
            //Point3D point = new Point3D(Points[1].X - arrLenX, Points[1].Y - arrLenY, Points[1].Z);
            ////Proections of the back side of arrow
            //double xBackLen = (arrowHalfBackLength * arrLenY) / ARROW_HEADER_LENGTH;
            //double yBackLen = (arrowHalfBackLength * arrLenX) / ARROW_HEADER_LENGTH;
            ////double zBackLen = (arrowHalfBackLength * arrLenZ) / ARROW_HEADER_LENGTH;

            //Points[3] = new Point3D(point.X + xBackLen, point.Y - yBackLen, point.Z);
            //Points[5] = new Point3D(point.X - xBackLen, point.Y + yBackLen, point.Z);

            ChangeNamePosition();
        }

        private Point3D GetMiddlePoint(Point3D startPosition, Point3D endPosition)
        {
            Point3D namePosition = new Point3D((endPosition.X + startPosition.X) / 2,
                                                (endPosition.Y + startPosition.Y) / 2, 0);
            return namePosition;
        }

        public Point3D MiddlePoint
        {
            get
            {
                return GetMiddlePoint(Points[0], Points[1]);
            }
        }

        public void ChangeVectorPosition(Point3D endPosition)
        {
            ChangeVectorPosition(new Point3D(), endPosition);


            //Points[3] = point;
            //Points[5] = point;

            //Matrix3D matrixX = new Matrix3D();
            //Matrix3D matrixY = new Matrix3D();
            //Matrix3D matrixZ = new Matrix3D();
            //Matrix3D result = new Matrix3D();

            //matrixX.M11 = 1;
            //matrixX.M22 = Math.Cos(45);
            //matrixX.M23 = - Math.Sin(45);
            //matrixX.M32 = Math.Sin(45);
            //matrixX.M33 = Math.Cos(45);

            //matrixY.M11 = Math.Cos(45);
            //matrixY.M13 = Math.Sin(45);
            //matrixY.M22 = 1;
            //matrixY.M31 = -Math.Sin(45);
            //matrixY.M33 = Math.Cos(45);

            //matrixZ.M11 = Math.Cos(45);
            //matrixZ.M12 = -Math.Sin(45);
            //matrixZ.M21 = Math.Sin(45);
            //matrixZ.M22 = Math.Cos(45);
            //matrixZ.M33 = 1;

            //result.M11 = Points[1].X;
            //result.M21 = Points[1].Y;
            //result.M31 = Points[1].Z;

            //result.M22 = 0;
            //result.M33 = 0;
            //result.M44 = 0;

            // matrixX.Prepend(matrixY);
            // matrixX.Prepend(matrixZ);

            // matrixX.Prepend(result);


            //double xnew = Points[1].X + (Points[3].X - Points[1].X) * Math.Cos(45) - (Points[3].Y - Points[1].Y) * Math.Sin(45);
            //double ynew = Points[1].Y + (Points[3].X - Points[1].X) * Math.Sin(45) + (Points[3].Y - Points[1].Y) * Math.Cos(45);

            // Points[3] = new Point3D(matrixX.M11, matrixX.M22, matrixX.M33);

            //xnew = Points[1].X + (Points[5].X - Points[1].X) * Math.Cos(-45) - (Points[5].Y - Points[1].Y) * Math.Sin(-45);
            //ynew = Points[1].Y + (Points[5].X - Points[1].X) * Math.Sin(-45) + (Points[5].Y - Points[1].Y) * Math.Cos(-45);

            //Points[5] = new Point3D(xnew, ynew, point.Z);

        }

        public void ChangeNamePosition(Point namePosition)
        {
            Canvas.SetTop(name, namePosition.Y);
            Canvas.SetLeft(name, namePosition.X);
        }

        public void ChangeNamePosition()
        {
            Point convertedNamePosition = MathUtils.Convert3DPoint(EndPoint, this);
            ChangeNamePosition(convertedNamePosition);
        }

        public TextBlock Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
    }
}
