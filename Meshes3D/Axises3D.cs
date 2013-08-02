using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _3DTools;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Controls;

namespace Meshes3D
{
    public class Axises3D
    {
        private const double AXIS_THICKNESS = 2;
        private const double STROKE_THICKNESS = 0.7;
        private const double STROKE_STEP = 5;
        private const double STROKE_LENGTH = 2;

        private ScreenSpaceLines3D axisX;
        private ScreenSpaceLines3D axisY;
        private ScreenSpaceLines3D axisZ;

        private ScreenSpaceLines3D strokesX;
        private ScreenSpaceLines3D strokesY;
        private ScreenSpaceLines3D strokesZ;

        public Axises3D()
        {
            InitializeAxises(50);
        }

        public Axises3D(double size)
        {
            InitializeAxises(size);
        }

        private void InitializeAxises(double size)
        {
            axisX = new ScreenSpaceLines3D();
            axisY = new ScreenSpaceLines3D();
            axisZ = new ScreenSpaceLines3D();

            strokesX = new ScreenSpaceLines3D();
            strokesY = new ScreenSpaceLines3D();
            strokesZ = new ScreenSpaceLines3D();

            axisX.Color = Colors.OrangeRed;
            axisY.Color = Colors.YellowGreen;
            axisZ.Color = Colors.DodgerBlue;

            strokesX.Color = Colors.OrangeRed;
            strokesY.Color = Colors.YellowGreen;
            strokesZ.Color = Colors.DodgerBlue;

            axisX.Thickness = AXIS_THICKNESS;
            axisY.Thickness = AXIS_THICKNESS;
            axisZ.Thickness = AXIS_THICKNESS;

            strokesX.Thickness = STROKE_THICKNESS;
            strokesY.Thickness = STROKE_THICKNESS;
            strokesZ.Thickness = STROKE_THICKNESS;

            axisX.Points.Add(new Point3D(-size, 0, 0));
            axisX.Points.Add(new Point3D(size, 0, 0));

            axisY.Points.Add(new Point3D(0, -size, 0));
            axisY.Points.Add(new Point3D(0, size, 0));

            axisZ.Points.Add(new Point3D(0, 0, -size));
            axisZ.Points.Add(new Point3D(0, 0, size));

            for (double i = axisX.Points[0].X; i < size; i += STROKE_STEP)
            {
                strokesX.Points.Add(new Point3D(i, STROKE_LENGTH, 0));
                strokesX.Points.Add(new Point3D(i, -STROKE_LENGTH, 0));
            }

            for (double i = axisY.Points[0].Y; i < size; i += STROKE_STEP)
            {
                strokesY.Points.Add(new Point3D(STROKE_LENGTH, i, 0));
                strokesY.Points.Add(new Point3D(-STROKE_LENGTH, i, 0));
            }

            for (double i = axisZ.Points[0].Z; i < size; i += STROKE_STEP)
            {
                strokesZ.Points.Add(new Point3D(STROKE_LENGTH, 0, i));
                strokesZ.Points.Add(new Point3D(-STROKE_LENGTH, 0, i));
            }
        }

        public void AddToViewport3D(Viewport3D viewport)
        {
            viewport.Children.Add(axisX);
            viewport.Children.Add(axisY);
            viewport.Children.Add(axisZ);

            //viewport.Children.Add(strokesX);
            //viewport.Children.Add(strokesY);
            //viewport.Children.Add(strokesZ);
        }

        public ScreenSpaceLines3D AxisX
        {
            get { return axisX; }
        }

        public ScreenSpaceLines3D AxisY
        {
            get { return axisY; }
        }

        public ScreenSpaceLines3D AxisZ
        {
            get { return axisZ; }
        }

        public ScreenSpaceLines3D StrokesX
        {
            get { return strokesX; }
        }

        public ScreenSpaceLines3D StrokesY
        {
            get { return strokesY; }
        }

        public ScreenSpaceLines3D StrokesZ
        {
            get { return strokesZ; }
        }
    }
}
