/// 
/// Copyright (c) 2005 by Andrej Benedik
///
/// This software is provided 'as-is', without any express or implied warranty.
/// In no event will the authors be held liable for any damages arising from the use of this software.
/// 
/// Permission is granted to anyone to use this software for any purpose, including commercial applications,
/// and to alter it and redistribute it freely, subject to the following restrictions:
///
/// 1. The origin of this software must not be misrepresented; you must not claim that you wrote the original software. If you use this software in a product, an acknowledgment in the product documentation would be appreciated but is not required.
/// 
/// 2. Altered source versions must be plainly marked as such, and must not be misrepresented as being the original software.
///
/// 3. This notice may not be removed or altered from any source distribution.
///

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using _3DTools;

namespace Meshes3D
{
    public class MeshFactory
    {
        #region enum ShadingMode
        public enum ShadingMode
        {
            Original,
            FlatShading,
            GouraudShading
        }
        #endregion

        #region Plane primitives

        public static MeshGeometry3D GetMesh(Type meshType)
        {
            MeshGeometry3D mesh = null;
            if (meshType.Equals(typeof(EllipsoidGeometry)))
            {
                mesh = new EllipsoidGeometry().Mesh3D;
            }
            else if (meshType.Equals(typeof(CubeGeometry)))
            {
                mesh = new CubeGeometry().Mesh3D;
            }

            return mesh;
        }

        private static MeshGeometry3D GetEllipse3D()
        {
            EllipsoidGeometry ellipse = new EllipsoidGeometry();

            return ellipse.Mesh3D;
        }

        private static MeshGeometry3D GetTriangle3D(CameraView view)
        {
            return null;
        }

        private static MeshGeometry3D GetSector3D(CameraView view)
        {
            MeshGeometry3D sector = new MeshGeometry3D();

            return sector;
        }

        public static MeshGeometry3D GetRectangle3D(CameraView view = CameraView.Perspective, Point3D center = new Point3D())
        {
            MeshGeometry3D rectangle = new MeshGeometry3D();

            if (view == CameraView.Top)
            {
                rectangle.Positions.Add(new Point3D(-10, 0, -10));
                rectangle.Positions.Add(new Point3D(10, 0, -10));
                rectangle.Positions.Add(new Point3D(10, 0, 10));
                rectangle.Positions.Add(new Point3D(-10, 0, 10));
            }
            else if (view == CameraView.Front)
            {
                rectangle.Positions.Add(new Point3D(-10, -10, 0));
                rectangle.Positions.Add(new Point3D(10, -10, 0));
                rectangle.Positions.Add(new Point3D(10, 10, 0));
                rectangle.Positions.Add(new Point3D(-10, 10, 0));
            }
            else
            {
                rectangle.Positions.Add(new Point3D(0, 10, -10));
                rectangle.Positions.Add(new Point3D(0, -10, -10));
                rectangle.Positions.Add(new Point3D(0, -10, 10));
                rectangle.Positions.Add(new Point3D(0, 10, 10));
            }

            rectangle.TriangleIndices.Add(0);
            rectangle.TriangleIndices.Add(1);
            rectangle.TriangleIndices.Add(2);

            rectangle.TriangleIndices.Add(2);
            rectangle.TriangleIndices.Add(3);
            rectangle.TriangleIndices.Add(0);

            return rectangle;
        }


        public static MeshGeometry3D GetTriangle3D(Point3D point1, Point3D point2, Point3D point3)
        {
            MeshGeometry3D triangle = new MeshGeometry3D();

            triangle.Positions.Add(point1);
            triangle.Positions.Add(point2);
            triangle.Positions.Add(point3);

            triangle.TriangleIndices.Add(0);
            triangle.TriangleIndices.Add(1);
            triangle.TriangleIndices.Add(2);

            return triangle;
        }

        public static MeshGeometry3D GetSector3D(double startAngle, double endAngle, Point3D center = new Point3D())
        {
            MeshGeometry3D triangle = new MeshGeometry3D();

            Point3D[] points = MathUtils.GetSectorPoints(20, startAngle, endAngle);
            //Point3D[] points = MathUtils.GetCirclePoints(20);

            triangle.Positions.Add(center);
            for (int i = 0; i < points.Length ; i++)
            {
                
                triangle.Positions.Add(points[i]);
                //triangle.Positions.Add(points[i + 1]);

                triangle.TriangleIndices.Add(0);
                triangle.TriangleIndices.Add(i);
                triangle.TriangleIndices.Add(i + 1);
            }
            //triangle.TriangleIndices.Add(0);
            //triangle.TriangleIndices.Add(points.Length);
            //triangle.TriangleIndices.Add(1);
            

            return triangle;
        }

        #endregion

        #region GetFlatModelGroup, GetGouraudModelGroup
        public static Model3DGroup GetFlatModelGroup(Model3DGroup modelGroupToShade)
        {
            return GetShadedModelGroup(modelGroupToShade, ShadingMode.FlatShading);
        }

        public static Model3DGroup GetGouraudModelGroup(Model3DGroup modelGroupToShade)
        {
            return GetShadedModelGroup(modelGroupToShade, ShadingMode.GouraudShading);
        }
        #endregion

        #region GetShadedModelGroup
        public static Model3DGroup GetShadedModelGroup(Model3DGroup modelGroupToShade, ShadingMode usedShadingMode)
        {
            Model3DGroup retModelGroup;

            retModelGroup = new Model3DGroup();

            foreach (Model3D oneModel in modelGroupToShade.Children)
            {
                if (oneModel is GeometryModel3D) // Apply shading
                {
                    GeometryModel3D oldGeometryModel3D;
                    GeometryModel3D newGeometryModel3D;
                    MeshGeometry3D oldMeshObject;
                    MeshGeometry3D newMeshObject;


                    oldGeometryModel3D = oneModel as GeometryModel3D;
                    oldMeshObject = oldGeometryModel3D.Geometry as MeshGeometry3D;

                    // Apply appropriate Shading
                    if (usedShadingMode == ShadingMode.FlatShading)
                        newMeshObject = GetFlatShadedMesh(oldMeshObject);
                    else if (usedShadingMode == ShadingMode.GouraudShading)
                        newMeshObject = GetGouraudShadedMesh(oldMeshObject);
                    else
                        newMeshObject = oldMeshObject;

                    newGeometryModel3D = new GeometryModel3D(newMeshObject, oldGeometryModel3D.Material);

                    retModelGroup.Children.Add(newGeometryModel3D);
                }
                else // else just add unchanged model to Mmodel3DGroup (lights, cameras, ...)
                    retModelGroup.Children.Add(oneModel);
            }

            return retModelGroup;
        }
        #endregion

        #region GetFlatShadedMesh
        /// <summary>
        /// Create points for all TriangleIndices - no points are used in more than one TriangleIndice and than calculate normals for each point
        /// </summary>
        /// <returns>MeshGeometry3D object</returns>
        public static MeshGeometry3D GetFlatShadedMesh(MeshGeometry3D meshGeometryToShade)
        {
            MeshGeometry3D retMesh;

            retMesh = new MeshGeometry3D();

            for (int i = 0; i < meshGeometryToShade.TriangleIndices.Count; i += 3) // tree integers for triangle
            {
                Point3D triangleVertex0;
                Point3D triangleVertex1;
                Point3D triangleVertex2;

                Vector3D normalVector;

                Vector3D triangleEdge0;
                Vector3D triangleEdge1;


                triangleVertex0 = meshGeometryToShade.Positions[meshGeometryToShade.TriangleIndices[i]];
                retMesh.Positions.Add(triangleVertex0);

                triangleVertex1 = meshGeometryToShade.Positions[meshGeometryToShade.TriangleIndices[i + 1]];
                retMesh.Positions.Add(triangleVertex1);

                triangleVertex2 = meshGeometryToShade.Positions[meshGeometryToShade.TriangleIndices[i + 2]];
                retMesh.Positions.Add(triangleVertex2);

                retMesh.TriangleIndices.Add(i);
                retMesh.TriangleIndices.Add(i + 1);
                retMesh.TriangleIndices.Add(i + 2);

                triangleEdge0 = triangleVertex0 - triangleVertex1;
                triangleEdge1 = triangleVertex0 - triangleVertex2;

                normalVector = Vector3D.CrossProduct(triangleEdge0, triangleEdge1);
                normalVector.Normalize();

                // Add tree normals - for each vertex - all face to the same direction
                retMesh.Normals.Add(normalVector);
                retMesh.Normals.Add(normalVector);
                retMesh.Normals.Add(normalVector);
            }

            return retMesh;
        }
        #endregion

        #region GetGouraudShadedMesh
        /// <summary>
        /// Create points for all TriangleIndices - no points are used in more than one TriangleIndice and than calculate normals for each point
        /// </summary>
        /// <returns>MeshGeometry3D object</returns>
        public static MeshGeometry3D GetGouraudShadedMesh(MeshGeometry3D meshGeometryToShade)
        {
            int nextIndex;
            int[] pointRemaping;
            MeshGeometry3D retMesh;

            retMesh = new MeshGeometry3D();

            pointRemaping = new int[meshGeometryToShade.Positions.Count];

            nextIndex = 0;
            for (int i = 0; i < meshGeometryToShade.Positions.Count; i++)
            {
                int newIndex;

                newIndex = GetTheSamePointIndex(meshGeometryToShade.Positions[i], meshGeometryToShade.Positions);

                // point is unique
                if (newIndex == -1 || newIndex == i)
                {
                    pointRemaping[i] = nextIndex;
                    nextIndex++;

                    retMesh.Positions.Add(meshGeometryToShade.Positions[i]); // and add it
                }
                else
                    pointRemaping[i] = newIndex; // just write the new index down
            }

            /* No need to calculate normals
            // for each Position add null-vector (later normals will be added to it)
            for (int i = 0; i < retMesh.Positions.Count; i++)
                retMesh.Normals.Add(new Vector3D(0, 0, 0));
            */

            for (int i = 0; i < meshGeometryToShade.TriangleIndices.Count; i += 3) // tree integers for triangle
            {
                int pointPosition0, pointPosition1, pointPosition2;

                Point3D triangleVertex0;
                Point3D triangleVertex1;
                Point3D triangleVertex2;

                pointPosition0 = pointRemaping[meshGeometryToShade.TriangleIndices[i]];
                retMesh.TriangleIndices.Add(pointPosition0);
                triangleVertex0 = meshGeometryToShade.Positions[pointPosition0];

                pointPosition1 = pointRemaping[meshGeometryToShade.TriangleIndices[i + 1]];
                retMesh.TriangleIndices.Add(pointPosition1);
                triangleVertex1 = meshGeometryToShade.Positions[pointPosition1];

                pointPosition2 = pointRemaping[meshGeometryToShade.TriangleIndices[i + 2]];
                retMesh.TriangleIndices.Add(pointPosition2);
                triangleVertex2 = meshGeometryToShade.Positions[pointPosition2];

                /* No need to calculate normals
                Vector3D normalVector;
                Vector3D triangleEdge0;
                Vector3D triangleEdge1;
				  
                triangleEdge0 = triangleVertex0 - triangleVertex1;
                triangleEdge1 = triangleVertex0 - triangleVertex2;

                normalVector = Vector3D.CrossProduct(triangleEdge0, triangleEdge1);
                normalVector.Normalize();


                retMesh.Normals[pointPosition0] += normalVector;
                retMesh.Normals[pointPosition1] += normalVector;
                retMesh.Normals[pointPosition2] += normalVector;
                */
            }

            /* No need to calculate normals
            // and finnaly just normalize normals
            for (int i = 0; i < _originalMesh.Normals.Count; i++)
                retMesh.Normals[i].Normalize();
            */

            return retMesh;
        }
        #endregion

        #region Private methods (GetTheSamePointIndex)
        /// <summary>
        /// Search in pointCollection if there exsist point with the same x, y and z coordinats and returns its index
        /// </summary>
        /// <param name="point">point to look for</param>
        /// <param name="pointCollection">collection of point</param>
        /// <returns>index of point in pointCollection or -1 if not found</returns>
        private static int GetTheSamePointIndex(Point3D point, Point3DCollection pointCollection)
        {
            int pointIndex;

            pointIndex = -1;

            for (int i = 0; i < pointCollection.Count; i++)
            {
                Point3D onePoint = pointCollection[i];

                if ((onePoint.X == point.X) && (onePoint.Y == point.Y) && (onePoint.Z == point.Z))
                {
                    pointIndex = i;
                    break;
                }

                pointIndex++;
            }

            return pointIndex;
        }
        #endregion
    }
}
