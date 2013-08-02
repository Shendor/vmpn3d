using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Media3D;

namespace _3DTools.primitives3d
{
    public class Line3D
    {
        private static readonly double E = 1e-10f;

        private Point3D point;
        private Vector3D direction;

        public Line3D(Point3D point, Vector3D direction)
        {
            this.point = point;
            this.direction = direction;
        }

        public Point3D Point
        {
            get
            {
                return point;
            }
            set
            {
                point = value;
            }
        }

        public Vector3D Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }

        public Point3D MovePointAlongLine(double t)
        {
            double newX = t * (this.Point.X - this.Direction.X) + this.Direction.X;
            double newY = t * (this.Point.Y - this.Direction.Y) + this.Direction.Y;
            double newZ = t * (this.Point.Z - this.Direction.Z) + this.Direction.Z;

            return point = new Point3D(newX, newY, newZ);
        }

        public Point3D ComputePlaneIntersection(Face3D plane)
        {

            Vector3D normal = plane.GetNormal();
            Point3D resultPoint = ComputePlaneIntersection(normal, plane.Point1);

            Vector3D v1 = new Vector3D(point.X - resultPoint.X,
                                          point.Y - resultPoint.Y,
                                          point.Z - resultPoint.Z);

            Vector3D v2 = new Vector3D(direction.X - resultPoint.X,
                                       direction.Y - resultPoint.Y,
                                       direction.Z - resultPoint.Z);
            double s = v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
            if (s <= E)
            {
                return resultPoint;
            }
            else
            {
                return new Point3D();
            }
        }


        public Point3D ComputePlaneIntersection(Vector3D normal, Point3D planePoint)
        {
            Vector3D lineDirection = new Vector3D(direction.X - point.X,
                                                  direction.Y - point.Y,
                                                  direction.Z - point.Z);
            lineDirection.Normalize();

            double A = normal.X;
            double B = normal.Y;
            double C = normal.Z;
            double D = -(normal.X * planePoint.X + normal.Y * planePoint.Y + normal.Z * planePoint.Z);

            double numerator = A * point.X + B * point.Y + C * point.Z + D;
            double denominator = DotProduct(normal, lineDirection);

            if (Math.Abs(denominator) < E)
            {
                if (Math.Abs(numerator) < E)
                {
                    return point;
                }
                else
                {
                    return new Point3D();
                }
            }
            else
            {
                double t = -numerator / denominator;
                Point3D resultPoint = new Point3D(point.X + t * lineDirection.X,
                    point.Y + t * lineDirection.Y, point.Z + t * lineDirection.Z);

                return resultPoint;
            }
        }
        public static double DotProduct(Vector3D v1, Vector3D v2)
        {
            return
            (
               v1.X * v2.X +
               v1.Y * v2.Y +
               v1.Z * v2.Z
            );
        }
    }
}
