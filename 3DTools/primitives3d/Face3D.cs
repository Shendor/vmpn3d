using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Media3D;

namespace _3DTools.primitives3d
{
    public class Face3D
    {
        private static readonly double E = 1e-10f;

        public Face3D(Point3D point1, Point3D point2, Point3D point3)
        {
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
        }

        public Point3D Point1
        {
            get;
            set;
        }


        public Point3D Point2
        {
            get;
            set;
        }

        public Point3D Point3
        {
            get;
            set;
        }


        public bool HasPoint(Point3D point)
        {
            double angle = 0.0;
            Vector3D vA, vB;
            Point3D[] positions = new Point3D[] { Point1, Point2, Point3 };
            for (int i = 0; i < 3; i++)
            {
                vA = new Vector3D(positions[i].X - point.X,
                                 positions[i].Y - point.Y,
                                 positions[i].Z - point.Z);
                Point3D position = positions[(i + 1) % 3];
                vB = new Vector3D(position.X - point.X,
                                 position.Y - point.Y,
                                 position.Z - point.Z);
                angle += MathUtils.AngleBetweenVectors(vA, vB);
            }
            if (angle >= (0.99 * (2 * Math.PI)))
            {
                return true;
            }

            return false;
        }

        public Vector3D GetNormal()
        {
            Vector3D xy = new Vector3D(Point2.X - Point1.X, Point2.Y - Point1.Y, Point2.Z - Point1.Z);
            Vector3D xz = new Vector3D(Point3.X - Point1.X, Point3.Y - Point1.Y, Point3.Z - Point1.Z);

            Vector3D result = CrossProduct(xy, xz);
            result.Normalize();

            return result;
        }

        private Vector3D CrossProduct(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.Y * v2.Z - v1.Z * v2.Y,
                                v1.Z * v2.X - v1.X * v2.Z,
                                v1.X * v2.Y - v1.Y * v2.X);
        }


    }
}
