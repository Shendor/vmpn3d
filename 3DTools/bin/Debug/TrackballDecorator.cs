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
// The following article discusses the mechanics behind this
// trackball implementation: http://viewport3d.com/trackball.htm
//
// Reading the article is not required to use this sample code,
// but skimming it might be useful.
//
//---------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Markup; // IAddChild, ContentPropertyAttribute

namespace _3DTools
{
    public class TrackballDecorator : Viewport3DDecorator
    {
        public TrackballDecorator()
        {
            // the transform that will be applied to the viewport 3d's camera
            _transform = new Transform3DGroup();
            _transform.Children.Add(_scale);
            r = new RotateTransform3D(_rotation);
           // _transform.Children.Add(new RotateTransform3D(_rotation));
            _transform.Children.Add(r);
            _transform.Children.Add(_move);

            // used so that we always get events while activity occurs within
            // the viewport3D
            _eventSource = new Border();
            _eventSource.Background = Brushes.Transparent;

            PreViewportChildren.Add(_eventSource);
            camera1 = new PerspectiveCamera();
            camera1.FarPlaneDistance = 4000;

            _scale.CenterX = 0;
            _scale.CenterY = 0;
            _scale.CenterZ = 0;

            _scale.ScaleX = 0.0055;
            _scale.ScaleY = 0.0055;
            _scale.ScaleZ = 0.0055;

            r.CenterX = 0;
            r.CenterY = 0;
            r.CenterZ = 0;
        }

        /// <summary>
        ///     A transform to move the camera or scene to the trackball's
        ///     current orientation and scale.
        /// </summary>
        public static Transform3D Transform
        {
            get { return _transform; }
        }

        #region Event Handling

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            _previousPosition2D = e.GetPosition(this);
            _previousPosition3D = ProjectToTrackball(ActualWidth,
                                                     ActualHeight,
                                                     _previousPosition2D);
            if (Mouse.Captured == null)
            {
                Mouse.Capture(this, CaptureMode.Element);
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (IsMouseCaptured)
            {
                Mouse.Capture(this, CaptureMode.None);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (IsMouseCaptured)
            {
                Point currentPosition = e.GetPosition(this);

                // avoid any zero axis conditions
                if (currentPosition == _previousPosition2D) return;

                // Prefer tracking to zooming if both buttons are pressed.
                if (e.LeftButton == MouseButtonState.Pressed && cameraView == 1)
                {
                    //Track(currentPosition);
                }
                else if (e.RightButton == MouseButtonState.Pressed)
                {
                    double y = _previousPosition2D.Y;
                    Zoom(currentPosition,y);
                }
                else if (e.MiddleButton == MouseButtonState.Pressed)
                {
                    //Track(currentPosition);
                    double x = _previousPosition2D.X;
                    double y = _previousPosition2D.Y;
                    Move(currentPosition, x, y);
                }

                _previousPosition2D = currentPosition;

                Viewport3D viewport3D = this.Viewport3D;
                
                if (viewport3D != null)
                {
                    if (viewport3D.Camera != null)
                    {
                        if (viewport3D.Camera.IsFrozen)
                        {
                            viewport3D.Camera = viewport3D.Camera.Clone();
                            camera1 = viewport3D.Camera as PerspectiveCamera;
                        }

                        if (viewport3D.Camera.Transform != _transform)
                        {
                            viewport3D.Camera.Transform = _transform;
                            camera1.Transform = _transform;
                            //camera1.Position = new Point3D(_move.OffsetX+2, _move.OffsetY+1, _move.OffsetZ+15);
                            //camera1.LookDirection = new Vector3D(_move.OffsetX, _move.OffsetY, _move.OffsetZ-1);
                        }
                    }
                }
            }
        }

        #endregion Event Handling

        private void Track(Point currentPosition)
        {
            
        }

        public Vector3D ProjectToTrackball(double width, double height, Point point)
        {
            double x = point.X / (width / 2);    // Scale so bounds map to [0,0] - [2,2]
            double y = point.Y / (height / 2);

            x = x - 1;                           // Translate 0,0 to the center
            y = 1 - y;                           // Flip so +Y is up instead of down

            double z2 = 1 - x * x - y * y;       // z^2 = 1 - x^2 - y^2
            double z = z2 > 0 ? Math.Sqrt(z2) : 0;

            return new Vector3D(x, y, z);
        }

        private void Zoom(Point currentPosition,double y)
        {
            double yDelta = currentPosition.Y - _previousPosition2D.Y;

            double scale = Math.Exp(yDelta / 100);    // e^(yDelta/100) is fairly arbitrary.

            _scale.ScaleX *= scale;
            _scale.ScaleY *= scale;
            _scale.ScaleZ *= scale;

           /* if (currentPosition.Y < y)
            {
                offsetZ -= 0.03;
                camera1.Position = new Point3D(offsetX, offsetY, offsetZ);
                //camera1.LookDirection = new Vector3D(offsetX, offsetY, offsetZ-15);
            }
            else if (currentPosition.Y > y)
            {
                offsetZ += 0.03;
                camera1.Position = new Point3D(offsetX, offsetY, offsetZ);
                //camera1.LookDirection = new Vector3D(offsetX, offsetY, offsetZ-15);
            }*/
        }

        private void Move(Point currentPosition, double x, double y)
        {
            //double yDelta = currentPosition.Y - _previousPosition2D.Y;
            //double scale = Math.Exp(yDelta / 100);

           if (currentPosition.X < x)
           {
               offsetX += 5;
               camera1.Position = new Point3D(offsetX, offsetY, offsetZ);
           }
           else if (currentPosition.X > x)
           {
                 offsetX -= 5;
                 camera1.Position = new Point3D(offsetX, offsetY, offsetZ);
           }
           else if (currentPosition.Y < y)
           {
                offsetY -= 5;
                camera1.Position = new Point3D(offsetX, offsetY, offsetZ);
           }
           else if (currentPosition.Y > y)
           {
                 offsetY += 5;
                 camera1.Position = new Point3D(offsetX, offsetY, offsetZ);
           }

        }


        public static TranslateTransform3D MoveTransform
        {
            get { return _move; }
            set { _move = value; }
        }
        public static PerspectiveCamera Camera
        {
            get { return camera1; }
            set { camera1 = value; }
        }

        public static ScaleTransform3D Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        public static RotateTransform3D Rotate
        {
            get { return r; }
            set { r = value; }
        }
        public static AxisAngleRotation3D AxisRotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }
        //--------------------------------------------------------------------
        //
        // Private data
        //
        //--------------------------------------------------------------------
       // private static PerspectiveCamera camera1;
        public static int cameraView = 2;

        private static PerspectiveCamera camera1;
        private Point _previousPosition2D;
        private Vector3D _previousPosition3D = new Vector3D(0, 0, 1);

        public static double offsetX = 0, offsetY = 5, offsetZ = 0;
        private static Transform3DGroup _transform;
        private static ScaleTransform3D _scale = new ScaleTransform3D();
        private static AxisAngleRotation3D _rotation = new AxisAngleRotation3D();
        private static TranslateTransform3D _move = new TranslateTransform3D();
        private static RotateTransform3D r;

        private Border _eventSource;
    }
}
