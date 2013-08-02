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
// trackball implementation: http://viewport3d.com/htm
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
using System.Windows.Media.Animation;
namespace _3DTools
{
    public class TrackballDecorator : Viewport3DDecorator
    {
        private CameraView cameraView = CameraView.Front;

        private ProjectionCamera camera1;
        private Point previousPosition2D;
        private Vector3D previousPosition3D;

        private double zoomRatio = 1;
        private double moveRatio = 1;
        private Transform3DGroup transform;
        private ScaleTransform3D scale;
        private AxisAngleRotation3D rotation;
        private TranslateTransform3D move;
        private RotateTransform3D r;
        private Border eventSource;

        private const double CAMERA_Z_POSITION = 600;
        private const long FAR_PLANE_DISTANCE = 18000;
        private const double SCALE = 0.0055;
        private const double SCALE_TO_OBJECT = 0.01;
        private const double SCALE_FOR_PERSPECTIVE = 0.2;

        private const double LOOK_DIRECTION_Z = -650;

        public TrackballDecorator()
        {
            previousPosition3D = new Vector3D(0, 0, 1);
            // the transform that will be applied to the viewport 3d's camera
            scale = new ScaleTransform3D();
            rotation = new AxisAngleRotation3D();
            move = new TranslateTransform3D(new Vector3D(0, 0, -CAMERA_Z_POSITION));

            transform = new Transform3DGroup();
            transform.Children.Add(scale);
            r = new RotateTransform3D(rotation);
            transform.Children.Add(r);
            transform.Children.Add(move);

            // used so that we always get events while activity occurs within
            // the viewport3D
            eventSource = new Border();
            eventSource.Background = Brushes.Transparent;

            PreViewportChildren.Add(eventSource);
            //camera1 = new PerspectiveCamera();
            // camera1.Transform = transform;
        }

        #region Event Handling

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            previousPosition2D = e.GetPosition(this);
            previousPosition3D = ProjectToTrackball(ActualWidth, ActualHeight, previousPosition2D);
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

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            Zoom(e.Delta);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.Cursor = Cursors.Arrow;
            if (IsMouseCaptured)
            {
                Point currentPosition = e.GetPosition(this);

                // avoid any zero axis conditions
                if (currentPosition == previousPosition2D)
                    return;

                // Prefer tracking to zooming if both buttons are pressed.
                if (e.RightButton == MouseButtonState.Pressed && CameraView == _3DTools.CameraView.Perspective)
                {
                    this.Cursor = Cursors.None;
                    Track(currentPosition);
                }
                else if (e.MiddleButton == MouseButtonState.Pressed)
                {
                    this.Cursor = Cursors.ScrollAll;
                    Translate(currentPosition);
                }
                previousPosition2D = currentPosition;


                //if (this.Viewport3D != null)
                //{
                //    if (this.Viewport3D.Camera != null)
                //    {
                //        if (this.Viewport3D.Camera.IsFrozen)
                //        {
                //            this.Viewport3D.Camera = this.Viewport3D.Camera.Clone();
                //            camera1 = this.Viewport3D.Camera as PerspectiveCamera;
                //        }

                //        if (this.Viewport3D.Camera.Transform != transform)
                //        {
                //            this.Viewport3D.Camera.Transform = transform;
                //            this.Viewport3D.Camera = camera1;
                //        }
                //    }
                //}
            }
        }

        #endregion Event Handling

        #region Camera transfromations
        //==========================================================================================
        /// <summary>
        /// Rotate camera
        /// </summary>
        /// <param name="currentPosition">Mouse position</param>
        private void Track(Point currentPosition)
        {
            Vector3D currentPosition3D = ProjectToTrackball(ActualWidth, ActualHeight, currentPosition);

            Vector3D axis = Vector3D.CrossProduct(previousPosition3D, currentPosition3D);
            double angle = Vector3D.AngleBetween(previousPosition3D, currentPosition3D);

            // quaterion will throw if this happens - sometimes we can get 3D positions that
            // are very similar, so we avoid the throw by doing this check and just ignoring
            // the event 
            if (axis.Length == 0)
                return;

            Quaternion delta = new Quaternion(axis, -angle);

            // Get the current orientantion from the RotateTransform3D
            AxisAngleRotation3D r = rotation;
            Quaternion q = new Quaternion(rotation.Axis, rotation.Angle);

            // Compose the delta with the previous orientation
            q *= delta;

            // Write the new orientation back to the Rotation3D
            rotation.Axis = q.Axis;
            rotation.Angle = q.Angle;

            previousPosition3D = currentPosition3D;
        }
        //==========================================================================================
        /// <summary>
        /// Roatate camera
        /// </summary>
        /// <param name="axis">Rotate axis vector</param>
        /// <param name="angle">Angle</param>
        public void Track(Vector3D axis, double angle)
        {
            Quaternion delta = new Quaternion(axis, -angle);

            // Get the current orientantion from the RotateTransform3D
            AxisAngleRotation3D r = rotation;
            Quaternion q = new Quaternion(rotation.Axis, rotation.Angle);

            // Compose the delta with the previous orientation
            q *= delta;

            // Write the new orientation back to the Rotation3D
            rotation.Axis = q.Axis;
            rotation.Angle = q.Angle;
        }
        //==========================================================================================
        /// <summary>
        /// Zoom camera
        /// </summary>
        /// <param name="delta">Scroll parameter</param>
        public void Zoom(int delta)
        {
            if (delta < 0)
            {
                if (cameraView == CameraView.Perspective)        // perspective
                {
                    scale.ScaleX += zoomRatio * 0.001;
                    scale.ScaleY += zoomRatio * 0.001;
                    scale.ScaleZ += zoomRatio * 0.001;
                    if (scale.ScaleX <= 0.001)
                    {
                        scale.ScaleX = 0.001;
                        scale.ScaleY = 0.001;
                        scale.ScaleZ = 0.001;
                    }
                }

                if (cameraView == CameraView.Front)
                    move.OffsetZ += zoomRatio;
                else if (cameraView == CameraView.Top)
                    move.OffsetY -= zoomRatio;
                else if (cameraView == CameraView.Left)
                    move.OffsetX -= zoomRatio;
            }
            else if (delta > 0)
            {
                if (cameraView == CameraView.Perspective)        // perspective
                {
                    scale.ScaleX -= zoomRatio * 0.001;
                    scale.ScaleY -= zoomRatio * 0.001;
                    scale.ScaleZ -= zoomRatio * 0.001;
                    if (scale.ScaleX <= 0.001)
                    {
                        scale.ScaleX = 0.001;
                        scale.ScaleY = 0.001;
                        scale.ScaleZ = 0.001;
                    }
                }
                if (cameraView == CameraView.Front)
                    move.OffsetZ -= zoomRatio;
                else if (cameraView == CameraView.Top)
                    move.OffsetY += zoomRatio;
                else if (cameraView == CameraView.Left)
                    move.OffsetX += zoomRatio;
            }
        }

        //==========================================================================================
        /// <summary>
        /// Move camera by axises
        /// </summary>
        /// <param name="currentPosition">Mouse position</param>
        private void Translate(Point currentPosition)
        {
            // Calculate the panning vector from screen(the vector component of the Quaternion
            // the division of the X and Y components scales the vector to the mouse movement
            Quaternion qV = new Quaternion(((previousPosition2D.X - currentPosition.X) / 5), ((currentPosition.Y - previousPosition2D.Y) / 5), 0, 0);

            // Get the current orientantion from the RotateTransform3D
            Quaternion q = new Quaternion(rotation.Axis, rotation.Angle);
            Quaternion qC = q;
            qC.Conjugate();

            // Here we rotate our panning vector about the the rotaion axis of any current rotation transform
            // and then sum the new translation with any exisiting translation
            qV = q * qV * qC;
            move.OffsetX += qV.X;
            move.OffsetY += qV.Y;
            move.OffsetZ += qV.Z;

            //Camera.Position = new Point3D(move.OffsetX, move.OffsetY, move.OffsetZ);
            //Camera.LookDirection = new Vector3D(-move.OffsetX, -move.OffsetY, -move.OffsetZ);
        }

        #region Helper methods
        //==========================================================================================
        private Vector3D ProjectToTrackball(double width, double height, Point point)
        {
            double x = point.X / (width / 2);    // Scale so bounds map to [0,0] - [2,2]
            double y = point.Y / (height / 2);

            x = x - 1;                           // Translate 0,0 to the center
            y = 1 - y;                           // Flip so +Y is up instead of down

            double z2 = 1 - x * x - y * y;       // z^2 = 1 - x^2 - y^2
            double z = z2 > 0 ? Math.Sqrt(z2) : 0;

            return new Vector3D(x, y, z);
        }
        #endregion

        #endregion

        #region Properties

        public Transform3D Transform
        {
            get
            {
                return transform;
            }
        }

        public TranslateTransform3D Move
        {
            get
            {
                return move;
            }
            set
            {
                move = value;
            }
        }
        public ProjectionCamera Camera
        {
            get
            {
                return camera1;
            }
            set
            {
                camera1 = value;
                if (this.Viewport3D != null && this.Viewport3D.Camera != null)
                {
                    // if (this.Viewport3D.Camera.IsFrozen)
                    //{
                    this.Viewport3D.Camera = camera1.Clone();
                    camera1 = this.Viewport3D.Camera as ProjectionCamera;
                    //}
                    // if (this.Viewport3D.Camera.Transform != transform)
                    //{
                    this.Viewport3D.Camera.Transform = transform;
                    this.Viewport3D.Camera = camera1;
                    //}
                }
            }
        }

        public ScaleTransform3D Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
            }
        }
        public RotateTransform3D Rotate
        {
            get
            {
                return r;
            }
            set
            {
                r = value;
            }
        }
        public AxisAngleRotation3D AxisRotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
            }
        }

        public double ZoomRatio
        {
            get
            {
                return zoomRatio;
            }
            set
            {
                zoomRatio = value;
            }
        }

        public double MoveRatio
        {
            get
            {
                return moveRatio;
            }
            set
            {
                moveRatio = value;
            }
        }

        public CameraView CameraView
        {
            get
            {
                return cameraView;
            }
            set
            {
                cameraView = value;
            }
        }

        #endregion

        #region Camera
        //===========================================================================================================
        public void ZoomToSelectedObject(Point3D selectedObjectPosition)
        {
            if (cameraView == CameraView.Perspective)        // perspective
            {
                SetCameraToSelectedObject(selectedObjectPosition);
                scale.ScaleX = SCALE_TO_OBJECT;
                scale.ScaleY = SCALE_TO_OBJECT;
                scale.ScaleZ = SCALE_TO_OBJECT;
            }
            if (cameraView == CameraView.Front)
            {
                move.OffsetX = selectedObjectPosition.X;
                move.OffsetY = selectedObjectPosition.Y;
                move.OffsetZ = selectedObjectPosition.Z + 20;
            }
            if (cameraView == CameraView.Top)
            {
                move.OffsetX = selectedObjectPosition.X;
                move.OffsetY = selectedObjectPosition.Y - 20;
                move.OffsetZ = selectedObjectPosition.Z;
            }
            if (cameraView == CameraView.Right)
            {
                move.OffsetX = selectedObjectPosition.X - 20;
                move.OffsetY = selectedObjectPosition.Y;
                move.OffsetZ = selectedObjectPosition.Z;
            }
        }
        //===========================================================================================================
        private void SetCameraToSelectedObject(Point3D transformCeneter)
        {
            if (cameraView == CameraView.Perspective)
            {
                Scale.CenterX = transformCeneter.X;
                Scale.CenterY = transformCeneter.Y;
                Scale.CenterZ = transformCeneter.Z + CAMERA_Z_POSITION;

                Rotate.CenterX = transformCeneter.X;
                Rotate.CenterY = transformCeneter.Y;
                Rotate.CenterZ = transformCeneter.Z + CAMERA_Z_POSITION;

                this.Camera.LookDirection = (Vector3D)transformCeneter;

                move.OffsetX = move.OffsetY = 0;
                move.OffsetZ = -CAMERA_Z_POSITION;
                Camera.Position = new Point3D(-Camera.LookDirection.X, -Camera.LookDirection.Y, -Camera.LookDirection.Z + CAMERA_Z_POSITION);
            }
        }

        #region Camera views
        //===========================================================================================================
        private void SetCameraToFrontView(Vector3D translatePoint)
        {
            AxisRotation.Angle = 90;
            AxisRotation.Axis = new Vector3D(0, 0, 1);


            MoveRatio = 0.02;
            ZoomRatio = 1;
            //ZoomRatio = 0.01;
        }
        public void ResetToFrontView()
        {

            Scale.CenterX = 0;
            Scale.CenterY = 0;
            Scale.CenterZ = 0;
            Scale.ScaleX = SCALE;
            Scale.ScaleY = SCALE;
            Scale.ScaleZ = SCALE;

            Rotate.CenterX = 0;
            Rotate.CenterY = 0;
            Rotate.CenterZ = 0;
            AxisRotation.Angle = 0;

            Camera.LookDirection = new Vector3D(0, 0, -1);
            Camera.Position = new Point3D(0, 0, 5);
            //if(Camera is PerspectiveCamera) ((PerspectiveCamera)Camera).FieldOfView = 50;
            Camera.UpDirection = new Vector3D(0, 1, 0);

            Move.OffsetX = 0;
            Move.OffsetY = 0;
            Move.OffsetZ = -CAMERA_Z_POSITION;
            cameraView = CameraView.Front;
            MoveRatio = 0.2;
            ZoomRatio = 1;
        }
        //===========================================================================================================
        private void SetCameraToTopView(Vector3D translatePoint)
        {
            AxisRotation.Angle = 90;
            AxisRotation.Axis = new Vector3D(1, 0, 0);

            MoveRatio = 0.2;
            ZoomRatio = 1;
        }
        //===========================================================================================================
        private void SetCameraToLeftView(Vector3D translatePoint)
        {
            AxisRotation.Angle = 90;
            AxisRotation.Axis = new Vector3D(0, 1, 0);
            
            MoveRatio = 0.2;
            ZoomRatio = 1;
        }
        //===========================================================================================================
        private void SetCameraToPerspectiveView(Vector3D translatePoint)
        {
            Camera.LookDirection = new Vector3D(0, 0, LOOK_DIRECTION_Z);
            Camera.Position = new Point3D(0, 0, -LOOK_DIRECTION_Z + CAMERA_Z_POSITION);
            if (Camera is PerspectiveCamera)
                ((PerspectiveCamera)Camera).FieldOfView = 50;
            Camera.UpDirection = new Vector3D(0, 1, 0);

            Scale.CenterX = 0;
            Scale.CenterY = 0;
            Scale.CenterZ = LOOK_DIRECTION_Z + CAMERA_Z_POSITION;

            Scale.ScaleX = SCALE_FOR_PERSPECTIVE;
            Scale.ScaleY = SCALE_FOR_PERSPECTIVE;
            Scale.ScaleZ = SCALE_FOR_PERSPECTIVE;

            //scale.ScaleX = scale.ScaleY = scale.ScaleZ = 0.001;

            Rotate.CenterX = 0;
            Rotate.CenterY = 0;
            Rotate.CenterZ = LOOK_DIRECTION_Z + CAMERA_Z_POSITION;

            AxisRotation.Angle = 0;

            Move.OffsetX = translatePoint.X;
            Move.OffsetY = translatePoint.Y;
            Move.OffsetZ = translatePoint.Z;
            MoveRatio = 0.5;
            ZoomRatio = 3;

            //Camera.LookDirection = new Vector3D(0, 0, -CAMERA_Z_POSITION);
            //Camera.Position = new Point3D(0, 0, LOOK_DIRECTION_Z - CAMERA_Z_POSITION);
            //if (Camera is PerspectiveCamera)
            //    ((PerspectiveCamera)Camera).FieldOfView = 50;
            //Camera.UpDirection = new Vector3D(0, 1, 0);

            //Scale.CenterX = 0;
            //Scale.CenterY = 0;
            //Scale.CenterZ = LOOK_DIRECTION_Z + CAMERA_Z_POSITION;

            ////Scale.ScaleX = SCALE_FOR_PERSPECTIVE;
            ////Scale.ScaleY = SCALE_FOR_PERSPECTIVE;
            ////Scale.ScaleZ = SCALE_FOR_PERSPECTIVE;

            //scale.ScaleX = scale.ScaleY = scale.ScaleZ = 0.001;

            //Rotate.CenterX = 0;
            //Rotate.CenterY = 0;
            //Rotate.CenterZ = LOOK_DIRECTION_Z + CAMERA_Z_POSITION;

            //AxisRotation.Angle = 0;

            //Move.OffsetX = translatePoint.X;
            //Move.OffsetY = translatePoint.Y;
            //Move.OffsetZ = translatePoint.Z;
            //MoveRatio = 0.5;
            //ZoomRatio = 3;
        }

        public void SetCameraView(CameraView cameraView, Vector3D translatePoint, double zoom = 1)
        {
            Scale.CenterX = 0;
            Scale.CenterY = 0;
            Scale.CenterZ = 0;

            Scale.ScaleX = zoom * SCALE;
            Scale.ScaleY = zoom * SCALE;
            Scale.ScaleZ = zoom * SCALE;

            Rotate.CenterX = 0;
            Rotate.CenterY = 0;
            Rotate.CenterZ = 0;

            this.cameraView = cameraView;
            switch (cameraView)
            {
                case CameraView.Perspective:
                    SetCameraToPerspectiveView(translatePoint);
                    break;
                case CameraView.Front:
                    SetCameraToFrontView(translatePoint);
                    break;
                case CameraView.Top:
                    SetCameraToTopView(translatePoint);
                    break;
                case CameraView.Left:
                    SetCameraToLeftView(translatePoint);
                    break;
            }
        }

        //===========================================================================================================
        public void InitializeCamera(Vector3D position, Point3D translatePoint, Vector3D viewPosition,
                                        Vector3D axisRotation, double angle, double zoom)
        {
            Camera.LookDirection = viewPosition;
            Camera.Position = new Point3D(position.X, position.Y, -viewPosition.Z + position.Z);
            if (Camera is PerspectiveCamera)
                ((PerspectiveCamera)Camera).FieldOfView = 50;
            Camera.UpDirection = new Vector3D(0, 1, 0);

            Scale.CenterX = position.X;
            Scale.CenterY = position.Y;
            Scale.CenterZ = viewPosition.Z + position.Z;

            Scale.ScaleX = zoom;
            Scale.ScaleY = zoom;
            Scale.ScaleZ = zoom;

            Rotate.CenterX = position.X;
            Rotate.CenterY = position.Y;
            Rotate.CenterZ = viewPosition.Z + position.Z;

            AxisRotation.Angle = angle;
            AxisRotation.Axis = axisRotation;

            Move.OffsetX = translatePoint.X;
            Move.OffsetY = translatePoint.Y;
            Move.OffsetZ = translatePoint.Z;
            cameraView = CameraView.Perspective;
            MoveRatio = 0.5;
            ZoomRatio = zoom * 20;

            if (Viewport3D != null)
                Viewport3D.Camera = this.Camera;
        }

        public void InitializeCamera()
        {
            if (Viewport3D != null)
                Viewport3D.Camera = this.Camera;

            SetCameraView(_3DTools.CameraView.Perspective, new Vector3D());
        }
        #endregion

        #endregion

    }
}
