using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using Meshes3D;
using System.Windows.Media;
using PNCreator.ManagerClasses;
using System.Windows;
using PNCreator.Controls;
using _3DTools;

namespace PNCreator.Controls.CarcassControl
{
    public abstract class AbstractMultiobjectCarcass : PNViewport
    {
        private static readonly int DEFAULT_ZOOM = 35;

        protected AbstractCarcass carcass;
        protected GeometryModel3D selectedObject;
        protected int selectedPointIndex = -1;

        private Point3D viewportOrientation;

        Axises3D axises = new Axises3D(500);

        public AbstractMultiobjectCarcass()
        {
            Trackball.InitializeCamera();

            viewportOrientation = new Point3D(1, 1, 1);

            this.Viewport3D.MouseLeftButtonDown += MultiobjectCarcass_MouseLeftButtonDown;
        }

        public AbstractMultiobjectCarcass(Point3D viewportOrientation)
        {
            Trackball.InitializeCamera();

            ViewportOrientation = viewportOrientation;

            this.Viewport3D.MouseLeftButtonDown += MultiobjectCarcass_MouseLeftButtonDown;
        }

        public Point3D ViewportOrientation
        {
            get { return viewportOrientation; }
            set
            {
                viewportOrientation = new Point3D((value.X == 0) ? value.X : value.X / value.X,
                                                  (value.Y == 0) ? value.Y : value.Y / value.Y,
                                                  (value.Z == 0) ? value.Z : value.Z / value.Z);
                InitializeViewport(viewportOrientation);

            }
        }

        public GeometryModel3D SelectedGeometry
        {
            get { return selectedObject; }
            set { selectedObject = value; }
        }

        private void InitializeViewport(Point3D viewportOrientation)
        {
            Viewport3D.Children.Remove(axises.AxisX);
            Viewport3D.Children.Remove(axises.AxisZ);
            Viewport3D.Children.Remove(axises.AxisY);

            if (viewportOrientation.Y.Equals(0))
            {
                Trackball.SetCameraView(CameraView.Top, new Vector3D(0, 0, 44), DEFAULT_ZOOM);
                Viewport3D.Children.Add(axises.AxisX);
                Viewport3D.Children.Add(axises.AxisZ);
                Viewport3D.Children.Add(new Wire().BuildWire(Wire.WireOrientation.Top, 150, 10));
            }
            else if (viewportOrientation.Z.Equals(0))
            {
                Trackball.SetCameraView(CameraView.Front, new Vector3D(0, 0, 44), DEFAULT_ZOOM);
                Viewport3D.Children.Add(axises.AxisX);
                Viewport3D.Children.Add(axises.AxisY);
                Viewport3D.Children.Add(new Wire().BuildWire(Wire.WireOrientation.Front, 150, 10));
            }
            else if (viewportOrientation.X.Equals(0))
            {
                Trackball.SetCameraView(CameraView.Left, new Vector3D(0, 0, 44), DEFAULT_ZOOM);
                Viewport3D.Children.Add(axises.AxisY);
                Viewport3D.Children.Add(axises.AxisZ);
                Viewport3D.Children.Add(new Wire().BuildWire(Wire.WireOrientation.Left, 150, 10));
            }
            else
            {
                Viewport3D.Children.Add(axises.AxisX);
                Viewport3D.Children.Add(axises.AxisY);
                Viewport3D.Children.Add(axises.AxisZ);

                Trackball.InitializeCamera(new Vector3D(0, 0, 600), new Point3D(0, 0, 44), new Vector3D(0, 0, -650),
                                                 new Vector3D(-0.1, -0.5, -0.8), 216, 0.2);
            }
        }

        public AbstractCarcass Carcass
        {
            get { return carcass; }
            set
            {
                BuildCarcass(value, false);
                carcass = value;
            }
        }
        //==================================================================================================
        public void MultiobjectCarcass_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point point2 = e.GetPosition(Viewport3D);
            Point3D p = MathUtils.Convert2DPoint(point2, Visual, Trackball.Move);
            SelectObject(GetSelectedObject(e.GetPosition(this)));
        }
        //==================================================================================================
        protected GeometryModel3D GetSelectedObject(Point point)
        {
            // Exception (+NullReference)!!!
            RayMeshGeometry3DHitTestResult meshHitResult = VisualTreeHelper.HitTest(this, point) as RayMeshGeometry3DHitTestResult;
            return (meshHitResult != null) ? meshHitResult.ModelHit as GeometryModel3D : null;
        }

        public void BuildCarcass(AbstractCarcass carcass, bool isWireVisible)
        {
            if (this.carcass != null) DestroyCarcass();
            this.carcass = carcass;
            if (this.carcass != null)
            {
                this.carcass.BuildCarcass(this.Viewport3D);
                if (isWireVisible) BuildCarcassWire();
            }
        }

        public void BuildCarcassWire()
        {
            if (this.carcass == null) return;
            if (this.carcass.CarcassWire != null) return;
            this.carcass.BuildCarcassWire(this.Viewport3D);
        }

        public void DestroyCarcass()
        {
            this.carcass.DestroyCarcass(this.Viewport3D);
            this.carcass.DestroyCarcassWire(this.Viewport3D);
        }

        public void HideCarcass()
        {
            this.carcass.HideCarcass(this.Viewport3D);
            this.carcass.HideCarcassWire(this.Viewport3D);
        }

        public void DestroyCarcassWire()
        {
            this.carcass.DestroyCarcassWire(this.Viewport3D);
        }


        protected abstract void SelectObject(GeometryModel3D selectedGeometry);

        protected bool FindModel(ModelVisual3D model)
        {
            if (((GeometryModel3D)model.Content) == selectedObject) return true;
            return false;
        }

    }
}
