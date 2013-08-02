//---------------------------------------------------------------------------
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Limited Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/limitedpermissivelicense.mspx
// All other rights reserved.
//
// This file is part of the 3D Tools for Windows Presentation Foundation
// project.  For more information, see:
// 
// http://CodePlex.com/Wiki/View.aspx?ProjectName=3DTools
//
//---------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Controls;
using System.Collections.Generic;
namespace _3DTools
{
    public static class MathUtils
    {
        public static readonly Matrix3D ZeroMatrix = new Matrix3D(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        public static readonly Vector3D XAxis = new Vector3D(1, 0, 0);
        public static readonly Vector3D YAxis = new Vector3D(0, 1, 0);
        public static readonly Vector3D ZAxis = new Vector3D(0, 0, 1);
        public static double GetAspectRatio(Size size)
        {
            return size.Width / size.Height;
        }

        public static double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }

        private static Matrix3D GetViewMatrix(ProjectionCamera camera)
        {
            Debug.Assert(camera != null,
                "Caller needs to ensure camera is non-null.");

            // This math is identical to what you find documented for
            // D3DXMatrixLookAtRH with the exception that WPF uses a
            // LookDirection vector rather than a LookAt point.

            Vector3D zAxis = -camera.LookDirection;
            zAxis.Normalize();

            Vector3D xAxis = Vector3D.CrossProduct(camera.UpDirection, zAxis);
            xAxis.Normalize();

            Vector3D yAxis = Vector3D.CrossProduct(zAxis, xAxis);

            Vector3D position = (Vector3D)camera.Position;
            double offsetX = -Vector3D.DotProduct(xAxis, position);
            double offsetY = -Vector3D.DotProduct(yAxis, position);
            double offsetZ = -Vector3D.DotProduct(zAxis, position);

            return new Matrix3D(
                xAxis.X, yAxis.X, zAxis.X, 0,
                xAxis.Y, yAxis.Y, zAxis.Y, 0,
                xAxis.Z, yAxis.Z, zAxis.Z, 0,
                offsetX, offsetY, offsetZ, 1);
        }

        /// <summary>
        ///     Computes the effective view matrix for the given
        ///     camera.
        /// </summary>
        public static Matrix3D GetViewMatrix(Camera camera)
        {
            if (camera == null)
            {
                throw new ArgumentNullException("camera");
            }

            ProjectionCamera projectionCamera = camera as ProjectionCamera;

            if (projectionCamera != null)
            {
                return GetViewMatrix(projectionCamera);
            }

            MatrixCamera matrixCamera = camera as MatrixCamera;

            if (matrixCamera != null)
            {
                return matrixCamera.ViewMatrix;
            }

            throw new ArgumentException(String.Format("Unsupported camera type '{0}'.", camera.GetType().FullName), "camera");
        }

        private static Matrix3D GetProjectionMatrix(OrthographicCamera camera, double aspectRatio)
        {
            Debug.Assert(camera != null,
                "Caller needs to ensure camera is non-null.");

            // This math is identical to what you find documented for
            // D3DXMatrixOrthoRH with the exception that in WPF only
            // the camera's width is specified.  Height is calculated
            // from width and the aspect ratio.

            double w = camera.Width;
            double h = w / aspectRatio;
            double zn = camera.NearPlaneDistance;
            double zf = camera.FarPlaneDistance;

            double m33 = 1 / (zn - zf);
            double m43 = zn * m33;

            return new Matrix3D(
                2 / w, 0, 0, 0,
                  0, 2 / h, 0, 0,
                  0, 0, m33, 0,
                  0, 0, m43, 1);
        }

        private static Matrix3D GetProjectionMatrix(PerspectiveCamera camera, double aspectRatio)
        {
            Debug.Assert(camera != null,
                "Caller needs to ensure camera is non-null.");

            // This math is identical to what you find documented for
            // D3DXMatrixPerspectiveFovRH with the exception that in
            // WPF the camera's horizontal rather the vertical
            // field-of-view is specified.

            double hFoV = MathUtils.DegreesToRadians(camera.FieldOfView);
            double zn = camera.NearPlaneDistance;
            double zf = camera.FarPlaneDistance;

            double xScale = 1 / Math.Tan(hFoV / 2);
            double yScale = aspectRatio * xScale;
            double m33 = (zf == double.PositiveInfinity) ? -1 : (zf / (zn - zf));
            double m43 = zn * m33;

            return new Matrix3D(
                xScale, 0, 0, 0,
                     0, yScale, 0, 0,
                     0, 0, m33, -1,
                     0, 0, m43, 0);
        }

        /// <summary>
        ///     Computes the effective projection matrix for the given
        ///     camera.
        /// </summary>
        public static Matrix3D GetProjectionMatrix(Camera camera, double aspectRatio)
        {
            if (camera == null)
            {
                throw new ArgumentNullException("camera");
            }

            PerspectiveCamera perspectiveCamera = camera as PerspectiveCamera;

            if (perspectiveCamera != null)
            {
                return GetProjectionMatrix(perspectiveCamera, aspectRatio);
            }

            OrthographicCamera orthographicCamera = camera as OrthographicCamera;

            if (orthographicCamera != null)
            {
                return GetProjectionMatrix(orthographicCamera, aspectRatio);
            }

            MatrixCamera matrixCamera = camera as MatrixCamera;

            if (matrixCamera != null)
            {
                return matrixCamera.ProjectionMatrix;
            }

            throw new ArgumentException(String.Format("Unsupported camera type '{0}'.", camera.GetType().FullName), "camera");
        }

        private static Matrix3D GetHomogeneousToViewportTransform(Rect viewport)
        {
            double scaleX = viewport.Width / 2;
            double scaleY = viewport.Height / 2;
            double offsetX = viewport.X + scaleX;
            double offsetY = viewport.Y + scaleY;

            return new Matrix3D(
                 scaleX, 0, 0, 0,
                      0, -scaleY, 0, 0,
                      0, 0, 1, 0,
                offsetX, offsetY, 0, 1);
        }

        /// <summary>
        ///     Computes the transform from world space to the Viewport3DVisual's
        ///     inner 2D space.
        /// 
        ///     This method can fail if Camera.Transform is non-invertable
        ///     in which case the camera clip planes will be coincident and
        ///     nothing will render.  In this case success will be false.
        /// </summary>
        public static Matrix3D TryWorldToViewportTransform(Viewport3DVisual visual, out bool success)
        {
            success = false;
            Matrix3D result = TryWorldToCameraTransform(visual, out success);

            if (success)
            {
                result.Append(GetProjectionMatrix(visual.Camera, MathUtils.GetAspectRatio(visual.Viewport.Size)));
                result.Append(GetHomogeneousToViewportTransform(visual.Viewport));
                success = true;
            }

            return result;
        }


        /// <summary>
        ///     Computes the transform from world space to camera space
        /// 
        ///     This method can fail if Camera.Transform is non-invertable
        ///     in which case the camera clip planes will be coincident and
        ///     nothing will render.  In this case success will be false.
        /// </summary>
        public static Matrix3D TryWorldToCameraTransform(Viewport3DVisual visual, out bool success)
        {
            success = false;
            if (visual != null)
            {

                Matrix3D result = Matrix3D.Identity;
                Camera camera = visual.Camera;

                if (camera == null)
                {
                    return ZeroMatrix;
                }

                Rect viewport = visual.Viewport;

                if (viewport == Rect.Empty)
                {
                    return ZeroMatrix;
                }

                Transform3D cameraTransform = camera.Transform;

                if (cameraTransform != null)
                {
                    Matrix3D m = cameraTransform.Value;

                    if (!m.HasInverse)
                    {
                        return ZeroMatrix;
                    }

                    m.Invert();
                    result.Append(m);
                }

                result.Append(GetViewMatrix(camera));

                success = true;
                return result;
            }
            else return new Matrix3D();
        }

        /// <summary>
        /// Gets the object space to world space transformation for the given DependencyObject
        /// </summary>
        /// <param name="visual">The visual whose world space transform should be found</param>
        /// <param name="viewport">The Viewport3DVisual the Visual is contained within</param>
        /// <returns>The world space transformation</returns>
        private static Matrix3D GetWorldTransformationMatrix(DependencyObject visual, out Viewport3DVisual viewport)
        {
            Matrix3D worldTransform = Matrix3D.Identity;
            viewport = null;

            if (!(visual is Visual3D))
            {
                throw new ArgumentException("Must be of type Visual3D.", "visual");
            }

            while (visual != null)
            {
                if (!(visual is ModelVisual3D))
                {
                    break;
                }

                Transform3D transform = (Transform3D)visual.GetValue(ModelVisual3D.TransformProperty);

                if (transform != null)
                {
                    worldTransform.Append(transform.Value);
                }

                visual = VisualTreeHelper.GetParent(visual);
            }

            viewport = visual as Viewport3DVisual;

            if (viewport == null)
            {
                if (visual != null)
                {
                    // In WPF 3D v1 the only possible configuration is a chain of
                    // ModelVisual3Ds leading up to a Viewport3DVisual.

                    throw new ApplicationException(
                        String.Format("Unsupported type: '{0}'.  Expected tree of ModelVisual3Ds leading up to a Viewport3DVisual.",
                        visual.GetType().FullName));
                }

                return ZeroMatrix;
            }

            return worldTransform;
        }

        /// <summary>
        ///     Computes the transform from the inner space of the given
        ///     Visual3D to the 2D space of the Viewport3DVisual which
        ///     contains it.
        /// 
        ///     The result will contain the transform of the given visual.
        /// 
        ///     This method can fail if Camera.Transform is non-invertable
        ///     in which case the camera clip planes will be coincident and
        ///     nothing will render.  In this case success will be false.
        /// </summary>
        /// <param name="visual"></param>
        /// <param name="success"></param>
        /// <returns></returns>
        public static Matrix3D TryTransformTo2DAncestor(DependencyObject visual, out Viewport3DVisual viewport, out bool success)
        {
            Matrix3D to2D = GetWorldTransformationMatrix(visual, out viewport);
            to2D.Append(MathUtils.TryWorldToViewportTransform(viewport, out success));

            if (!success)
            {
                return ZeroMatrix;
            }

            return to2D;
        }


        /// <summary>
        ///     Computes the transform from the inner space of the given
        ///     Visual3D to the camera coordinate space
        /// 
        ///     The result will contain the transform of the given visual.
        /// 
        ///     This method can fail if Camera.Transform is non-invertable
        ///     in which case the camera clip planes will be coincident and
        ///     nothing will render.  In this case success will be false.
        /// </summary>
        /// <param name="visual"></param>
        /// <param name="success"></param>
        /// <returns></returns>
        public static Matrix3D TryTransformToCameraSpace(DependencyObject visual, out Viewport3DVisual viewport, out bool success)
        {
            Matrix3D toViewSpace = GetWorldTransformationMatrix(visual, out viewport);
            toViewSpace.Append(MathUtils.TryWorldToCameraTransform(viewport, out success));

            if (!success)
            {
                return ZeroMatrix;
            }

            return toViewSpace;
        }

        /// <summary>
        ///     Transforms the axis-aligned bounding box 'bounds' by
        ///     'transform'
        /// </summary>
        /// <param name="bounds">The AABB to transform</param>
        /// <returns>Transformed AABB</returns>
        public static Rect3D TransformBounds(Rect3D bounds, Matrix3D transform)
        {
            double x1 = bounds.X;
            double y1 = bounds.Y;
            double z1 = bounds.Z;
            double x2 = bounds.X + bounds.SizeX;
            double y2 = bounds.Y + bounds.SizeY;
            double z2 = bounds.Z + bounds.SizeZ;

            Point3D[] points = new Point3D[] {
                new Point3D(x1, y1, z1),
                new Point3D(x1, y1, z2),
                new Point3D(x1, y2, z1),
                new Point3D(x1, y2, z2),
                new Point3D(x2, y1, z1),
                new Point3D(x2, y1, z2),
                new Point3D(x2, y2, z1),
                new Point3D(x2, y2, z2),
            };

            transform.Transform(points);

            // reuse the 1 and 2 variables to stand for smallest and largest
            Point3D p = points[0];
            x1 = x2 = p.X;
            y1 = y2 = p.Y;
            z1 = z2 = p.Z;

            for (int i = 1; i < points.Length; i++)
            {
                p = points[i];

                x1 = Math.Min(x1, p.X); y1 = Math.Min(y1, p.Y); z1 = Math.Min(z1, p.Z);
                x2 = Math.Max(x2, p.X); y2 = Math.Max(y2, p.Y); z2 = Math.Max(z2, p.Z);
            }

            return new Rect3D(x1, y1, z1, x2 - x1, y2 - y1, z2 - z1);
        }

        /// <summary>
        ///     Normalizes v if |v| > 0.
        /// 
        ///     This normalization is slightly different from Vector3D.Normalize. Here
        ///     we just divide by the length but Vector3D.Normalize tries to avoid
        ///     overflow when finding the length.
        /// </summary>
        /// <param name="v">The vector to normalize</param>
        /// <returns>'true' if v was normalized</returns>
        public static bool TryNormalize(ref Vector3D v)
        {
            double length = v.Length;

            if (length != 0)
            {
                v /= length;
                return true;
            }

            return false;
        }
        //===========================================================================================================
        public static Point3D Convert2DPoint(Point pointToConvert, Visual3D sphere, TranslateTransform3D cameraPosition)        // transform world matrix
        {
            bool success = true;
            Viewport3DVisual viewport;
            Matrix3D screenTransform = TryTransformTo2DAncestor(sphere, out viewport, out success);

            Point3D pointInWorld = new Point3D();
            if (screenTransform.HasInverse)
            {
                //Matrix3D reverseTransform = screenTransform;
                //reverseTransform.Invert();

                screenTransform.Invert();

                Point3D pointOnScreen = new Point3D(pointToConvert.X, pointToConvert.Y, 1);

                //  pointInWorld = reverseTransform.Transform(pointOnScreen);
                pointInWorld = screenTransform.Transform(pointOnScreen);
                //pointInWorld = new Point3D(((pointInWorld.X + cameraPosition.OffsetX) / 4),
                //                            ((pointInWorld.Y + cameraPosition.OffsetY) / 4),
                //                            ((pointInWorld.Z + cameraPosition.OffsetZ) / 4));
            }
            return pointInWorld;
        }

        //FIXME: Should be replaced with method below
        public static Point Convert3DPoint(Point3D p3d, Viewport3D vp)
        {
            bool TransformationResultOK;
            Viewport3DVisual vp3Dv = VisualTreeHelper.GetParent(vp.Children[0]) as Viewport3DVisual;
            Matrix3D m = TryWorldToViewportTransform(vp3Dv, out TransformationResultOK);
            if (!TransformationResultOK) return new Point(0, 0);
            Point3D pb = m.Transform(p3d);
            Point p2d = new Point(pb.X, pb.Y);
            return p2d;
        }

        public static Point Convert3DPoint(Point3D p3d, DependencyObject dependencyObject)
        {
            bool TransformationResultOK;
            Viewport3DVisual vp3Dv = VisualTreeHelper.GetParent(dependencyObject) as Viewport3DVisual;
            Matrix3D m = TryWorldToViewportTransform(vp3Dv, out TransformationResultOK);
            if (!TransformationResultOK) return new Point(0, 0);
            Point3D pb = m.Transform(p3d);
            Point p2d = new Point(pb.X, pb.Y);
            return p2d;
        }

        /// <summary>
        /// Takes a 3D point and returns the corresponding 2D point (X,Y) within the viewport.  
        /// Requires the 3DUtils project available at http://www.codeplex.com/Wiki/View.aspx?ProjectName=3DTools
        /// </summary>
        /// <param name="point3D">A point in 3D space</param>
        /// <param name="viewPort">An instance of Viewport3D</param>
        /// <returns>The corresponding 2D point or null if it could not be calculated</returns>
        public static Point Point3DToScreen2D(Point3D point3D, Viewport3D viewPort)
        {
            bool bOK = false;

            // We need a Viewport3DVisual but we only have a Viewport3D.
            Viewport3DVisual vpv = VisualTreeHelper.GetParent(viewPort.Children[0]) as Viewport3DVisual;

            // Get the world to viewport transform matrix
            Matrix3D m = MathUtils.TryWorldToViewportTransform(vpv, out bOK);

            if (bOK)
            {
                // Transform the 3D point to 2D
                Point3D transformedPoint = m.Transform(point3D);

                Point screen2DPoint = new Point(transformedPoint.X, transformedPoint.Y);

                return screen2DPoint;
            }
            else
            {
                return new Point();
            }
        }


        /// <summary>
        ///     Computes the center of 'box'
        /// </summary>
        /// <param name="box">The Rect3D we want the center of</param>
        /// <returns>The center point</returns>
        public static Point3D GetCenter(Rect3D box)
        {
            return new Point3D(box.X + box.SizeX / 2, box.Y + box.SizeY / 2, box.Z + box.SizeZ / 2);
        }

        public static double VectorLength(Vector3D a)
        {
            return Math.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z);
        }

        public static double VectorLength(Vector a)
        {
            return Math.Sqrt(a.X * a.X + a.Y * a.Y);
        }

        public static double VectorMultiplication(Vector3D a, Vector3D b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public static double RadiansBetweenVectors(Vector3D a, Vector3D b)
        {
            return Math.Acos(VectorMultiplication(a, b) / (VectorLength(a) * VectorLength(b)));
        }

        public static double AngleBetweenVectors(Vector3D a, Vector3D b)
        {
            return TranslateRadianToAngle(RadiansBetweenVectors(a,b));
        }

        public static double TranslateRadianToAngle(double radian)
        {
            return radian * (180 / Math.PI);
        }

        public static double TranslateAngleToRadian(double angle)
        {
            return angle * Math.PI / 180;
        }

        //public static double VectorProjectionOnVector(Vector3D a, Vector3D b)
        //{
        //    return VectorLength(a) * AngleBetweenVectors(a, b);
        //}

        public static double VectorProjectionOnVector(Vector3D a, Vector3D b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public static Vector VectorOnPlaneYOZProjection(Vector3D a)
        {
            return new Vector(a.Y, a.Z);
        }

        public static Vector VectorProjectionOnPlane(Vector3D a, Vector3D plane)
        {
            if (plane.X == 0) return new Vector(a.Y * plane.Y, a.Z * plane.Z);
            else if (plane.Y == 0) return new Vector(a.X * plane.X, a.Z * plane.Z);
            else if (plane.Z == 0) return new Vector(a.X * plane.X, a.Y * plane.Y);
            else new ArgumentException("The vector 'plane' doesn't contain at least one coordinate equals 0");
            return new Vector();
        }

        public static Vector VectorOnPlaneXOYrojection(Vector3D a)
        {
            return new Vector(a.X, a.Y);
        }

        public static Point3D MultiplyPoints(Point3D point1, Point3D point2)
        {
            return new Point3D(point1.X * point2.X, point1.Y * point2.Y, point1.Z * point2.Z);
        }

        public static Point3D RotatePoint3D(double angle, Point3D point, Point3D center = new Point3D())
        {
            double radians = TranslateAngleToRadian(angle);
            double x = center.X + (point.X - center.X) * Math.Cos(radians) + (center.Y - point.Y) * Math.Sin(radians);
            double y = center.Y + (point.X - center.X) * Math.Sin(radians) + (point.Y - center.Y) * Math.Cos(radians);
            return new Point3D(x, y, point.Z);
        }

        public static Point3D GetCirclePoint(double angle, double radius, Point3D orientation = new Point3D())
        {
            double x = radius * Math.Cos(TranslateAngleToRadian(angle));
            double y = radius * Math.Sin(TranslateAngleToRadian(angle));

            // TODO: Try to find the best way to calculate circle point
            if (orientation.Equals(new Point3D())) orientation = new Point3D(1, 1, 0);
            if (orientation.X != 0)
            {
                return new Point3D(x*orientation.X, y*orientation.Y, y*orientation.Z);
            }
            else
            {
                return new Point3D(x * orientation.X, x * orientation.Y, y * orientation.Z);
            }
        }


        public static Point3D[] GetCirclePoints(int quantity, Point3D orientation = new Point3D(), double radius = 70)
        {
            Point3D[] circlePoints = new Point3D[quantity];

            double step = 360 / quantity;
            double angle = 0;
            for (int i = 0; i < quantity; i++, angle += step)
            {
                circlePoints[i] = GetCirclePoint(angle, radius, orientation);
            }

            return circlePoints;
        }

        public static Point3D[] GetSectorPoints(int resolution, double startAngle, double endAngle,
                                                    Point3D orientation = new Point3D(), double radius = 70)
        {
            Point3D[] circlePoints = new Point3D[resolution + 1];

            double step = endAngle / resolution;
            double angle = startAngle;
            for (int i = 0; i < resolution + 1; i++, angle += step)
            {
                circlePoints[i] = GetCirclePoint(angle, radius, orientation);
            }

            return circlePoints;
        }
    }
}
