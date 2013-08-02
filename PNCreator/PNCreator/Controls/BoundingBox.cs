using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using _3DTools;
using System.Windows.Media;
using Meshes3D;

namespace PNCreator.Controls
{
    public class BoundingBox : ScreenSpaceLines3D
    {
        private static readonly int DEFAULT_LINE_LENGTH = 4;

        public BoundingBox()
        {
            this.Color = Colors.WhiteSmoke;
            this.Thickness = 1;

        }

        public void Clear()
        {
            Points.Clear();
        }

        public void BuildBoundingBox(ScreenSpaceLines3D line)
        {
            Points.Clear();

            BuildBoundingBox(line.Geometry.Bounds);
        }

        /// <summary>
        /// Build bounding box
        /// </summary>
        /// <param name="bound">Bounding rectangle of mesh</param>
        public void BuildBoundingBox(Rect3D bound)
        {
            float lineLengthX = (float)bound.SizeX / DEFAULT_LINE_LENGTH;
            float lineLengthY = (float)bound.SizeY / DEFAULT_LINE_LENGTH;
            float lineLengthZ = (float)bound.SizeZ / DEFAULT_LINE_LENGTH;

            Points.Clear();

            Point3D startPoint = new Point3D(bound.X, bound.Y, bound.Z);

            // 0
            AddPoints(lineLengthX, lineLengthY, lineLengthZ, startPoint);

            // 1
            startPoint = new Point3D(bound.X, bound.Y + bound.SizeY, bound.Z);
            AddPoints(lineLengthX, -lineLengthY, lineLengthZ, startPoint);

            // 2
            startPoint = new Point3D(bound.X + bound.SizeX, bound.Y, bound.Z);
            AddPoints(-lineLengthX, lineLengthY, lineLengthZ, startPoint);

            // 3
            startPoint = new Point3D(bound.X + bound.SizeX, bound.Y + bound.SizeY, bound.Z);
            AddPoints(-lineLengthX, -lineLengthY, lineLengthZ, startPoint);

            // 4
            startPoint = new Point3D(bound.X, bound.Y, bound.Z + bound.SizeZ);
            AddPoints(lineLengthX, lineLengthY, -lineLengthZ, startPoint);

            // 5
            startPoint = new Point3D(bound.X, bound.Y + bound.SizeY, bound.Z + bound.SizeZ);
            AddPoints(lineLengthX, -lineLengthY, -lineLengthZ, startPoint);

            // 6
            startPoint = new Point3D(bound.X + bound.SizeX, bound.Y, bound.Z + bound.SizeZ);
            AddPoints(-lineLengthX, lineLengthY, -lineLengthZ, startPoint);

            // 7
            startPoint = new Point3D(bound.X + bound.SizeX, bound.Y + bound.SizeY, bound.Z + bound.SizeZ);
            AddPoints(-lineLengthX, -lineLengthY, -lineLengthZ, startPoint);
        }

        public void BuildBoundingBox(List<Mesh3D> meshes)
        {
            if (meshes == null)
            {
                Points.Clear();
            }
            else if (meshes.Count == 1)
            {
                BuildBoundingBox(meshes[0].Bounds);
            }
            else
            {

                Rect3D boundingBox = new Rect3D();

                //double minX = Double.MaxValue;
                //double maxX = Double.MinValue;
                //double minY = Double.MaxValue;
                //double maxY = Double.MinValue;
                double minZ = Double.MaxValue;
                double maxZ = Double.MinValue;

                //double maxSizeX = 0;
                //double maxSizeY = 0;
                //double maxSizeZ = 0;

                foreach (Mesh3D mesh in meshes)
                {
                    //if (pnObject.Bounds.X < minX) minX = pnObject.Bounds.X;
                    //else if (pnObject.Bounds.X > maxX) maxX = pnObject.Bounds.X;

                    //if (pnObject.Bounds.Y < minY) minY = pnObject.Bounds.Y;
                    //else if (pnObject.Bounds.Y > maxY) maxY = pnObject.Bounds.Y;

                    if (mesh.Bounds.Z < minZ)
                        minZ = mesh.Bounds.Z;
                    else if (mesh.Bounds.Z > maxZ)
                        maxZ = mesh.Bounds.Z;

                    //if (pnObject.Bounds.SizeX > maxSizeX) maxSizeX = pnObject.Bounds.SizeX;
                    //if (pnObject.Bounds.SizeY > maxSizeY) maxSizeY = pnObject.Bounds.SizeY;
                    //if (pnObject.Bounds.SizeZ > maxSizeZ) maxSizeZ = pnObject.Bounds.SizeZ;
                    boundingBox.Union(mesh.Bounds);
                }

                BuildBoundingBox(new Rect3D(boundingBox.X, boundingBox.Y, boundingBox.Z,
                   boundingBox.SizeX, boundingBox.SizeY, Math.Abs(maxZ - minZ) + 0.5));
            }
        }

        private void AddPoints(float lineLengthX, float lineLengthY, float lineLengthZ, Point3D startPoint)
        {
            Points.Add(startPoint);
            Points.Add(new Point3D(startPoint.X + lineLengthX, startPoint.Y, startPoint.Z));
            Points.Add(startPoint);
            Points.Add(new Point3D(startPoint.X, startPoint.Y + lineLengthY, startPoint.Z));
            Points.Add(startPoint);
            Points.Add(new Point3D(startPoint.X, startPoint.Y, startPoint.Z + lineLengthZ));
        }
    }
}
