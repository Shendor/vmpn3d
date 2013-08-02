using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Controls;
using _3DTools;
using System.Windows.Media;
using Meshes3D;

namespace PNCreator.Controls.CarcassControl
{
    public abstract class AbstractCarcass
    {
        protected List<CarcassPoint> carcassPoints;
        protected List<ScreenSpaceLines3D> carcassWire;

        protected Vector3D center;

        protected double xLength = 6;
        protected double yLength = 6;
        protected double zLength = 6;

        public AbstractCarcass()
        {
            center = new Vector3D();
            carcassPoints = new List<CarcassPoint>();
            carcassWire = new List<ScreenSpaceLines3D>();
        }

        public void ShowCarcassWire(Point3D p0, Point3D p1, Viewport3D viewport)
        {
            ScreenSpaceLines3D ssl = new ScreenSpaceLines3D();

            ssl.Points.Add(p0);
            ssl.Points.Add(p1);
            ssl.Color = Colors.Black;
            ssl.Thickness = 0.3;
            viewport.Children.Add(ssl);

            carcassWire.Add(ssl);
        }

        public List<CarcassPoint> CarcassPoints
        {
            get { return carcassPoints; }
            set { carcassPoints = value; }
        }

        public List<ScreenSpaceLines3D> CarcassWire
        {
            get { return carcassWire; }
            set { carcassWire = value; }
        }

        public abstract void BuildCarcass(Viewport3D viewport);
        public abstract void BuildCarcassWire(Viewport3D viewport);

        public void DestroyCarcass(Viewport3D viewport)
        {
            if (carcassPoints == null) return;

            foreach (ModelVisual3D model in CarcassPoints)
            {
                viewport.Children.Remove(model);
            }
            carcassPoints.Clear();
            carcassPoints = null;
        }

        public void HideCarcass(Viewport3D viewport)
        {
            if (carcassPoints == null) return;

            foreach (ModelVisual3D model in CarcassPoints)
            {
                viewport.Children.Remove(model);
            }
        }

        public void HideCarcassWire(Viewport3D viewport)
        {
            if (carcassWire == null) return;
            foreach (ScreenSpaceLines3D line in CarcassWire)
            {
                viewport.Children.Remove(line);
            }
        }

        public void DestroyCarcassWire(Viewport3D viewport)
        {
            if (carcassWire == null) return;
            foreach (ScreenSpaceLines3D line in CarcassWire)
            {
                viewport.Children.Remove(line);
            }
            carcassWire.Clear();
            carcassWire = null;
        }
    }
}
