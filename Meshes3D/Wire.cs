using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using _3DTools;
using _3DTools.primitives3d;
namespace Meshes3D
{
    public class Wire : ScreenSpaceLines3D
    {
        private Point3DCollection gridLines;

        public enum WireOrientation
        {
            Front,
            Top,
            Left
        }


        /// <summary>
        /// Remove wire
        /// </summary>
        public void DestroyWire()
        {
            Points.Clear();
        }



        /// <summary>
        /// Set new points collection to the wire
        /// </summary>
        /// <param name="wireOrientation">Wire orientatiom</param>
        /// <returns>Wire</returns>
        public ScreenSpaceLines3D ReBuildWire(WireOrientation wireOrientation)
        {
            Points = GetWirePoints(wireOrientation, 50, 10);
            TranslateTransform3D move = new TranslateTransform3D(new Vector3D(0, 0, -650));
            Transform = move;
            return this;
        }

        /// <summary>
        /// Set new points collection to the wire
        /// </summary>
        /// <param name="wireOrientation">Wire orientatiom</param>
        /// <param name="position">Wire position</param>
        /// <param name="linesCount">Lines count</param>
        /// <param name="lineLength">Line length</param>
        /// <param name="spaceLength">Length of indent</param>
        /// <returns>Wire</returns>
        public ScreenSpaceLines3D ReBuildWire(WireOrientation wireOrientation, Vector3D position, int length, int step)
        {
            Points = GetWirePoints(wireOrientation, length, step);
            TranslateTransform3D move = new TranslateTransform3D(position);
            Transform = move;
            return this;
        }


        /// <summary>
        /// Build new wire
        /// </summary>
        /// <param name="wireOrientation">Wire orientatiom</param>
        /// <param name="linesCount">Lines count</param>
        /// <param name="lineLength">Line length</param>
        /// <param name="spaceLength">Length of indent</param>
        /// <returns>Wire</returns>
        public ScreenSpaceLines3D BuildWire(WireOrientation wireOrientation, int length, int step)
        {

            gridLines = GetWirePoints(wireOrientation, length, step);

            Points = gridLines;
            Color = Colors.Black;
            Thickness = 1;
            return this;
        }

        /// <summary>
        /// Get wire points
        /// </summary>
        /// <param name="wireOrientation">Wire orientation</param>
        /// <param name="linesCount">Lines count</param>
        /// <param name="length">Line length</param>
        /// <param name="step">Length of indent</param>
        /// <returns>Point3DCollection</returns>
        private Point3DCollection GetWirePoints(WireOrientation wireOrientation, int length, int step)
        {
            gridLines = new Point3DCollection();

            for (int j = -length; j <= length; j += step)
            {

                if (wireOrientation == WireOrientation.Top)      // for perspective camera
                {
                    // x line
                    gridLines.Add(new Point3D(j, 0.02, -length));
                    gridLines.Add(new Point3D(j, 0.02, length));

                    // z line
                    gridLines.Add(new Point3D(-length, 0.02, j));
                    gridLines.Add(new Point3D(length, 0.02, j));
                }
                else if (wireOrientation == WireOrientation.Front)     // for front camera
                {
                    // x line
                    gridLines.Add(new Point3D(j, -length, -0.02));
                    gridLines.Add(new Point3D(j, length, -0.02));

                    // y line
                    gridLines.Add(new Point3D(-length, j, -0.02));
                    gridLines.Add(new Point3D(length, j, -0.02));
                }
                else if (wireOrientation == WireOrientation.Left)     // for righ camera
                {
                    // y line
                    gridLines.Add(new Point3D(-0.02, j, -length));
                    gridLines.Add(new Point3D(-0.02, j, length));

                    // z line
                    gridLines.Add(new Point3D(-0.02, -length, j));
                    gridLines.Add(new Point3D(-0.02, length, j));
                }
            }
            return gridLines;
        }

        public Face3D GetPlane()
        {
            return new Face3D(new Point3D(-1000, -1000, 0), new Point3D(1000, -1000, 0), new Point3D(-1000, 1000, 0));
        }
    }
}
