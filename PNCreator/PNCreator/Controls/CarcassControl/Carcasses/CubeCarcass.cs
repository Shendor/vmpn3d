using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using _3DTools;
using Meshes3D;

namespace PNCreator.Controls.CarcassControl
{
    class CubeCarcass : AbstractCarcass
    {
        private const double STEP = 20;

        public CubeCarcass()
            : base()
        {
            xLength = 50;
            yLength = 50;
            zLength = 50;
        }

        public override void BuildCarcass(Viewport3D viewport)
        {
            if (carcassPoints != null && carcassPoints.Count > 0)
            {
                foreach (CarcassPoint point in carcassPoints)
                {
                    viewport.Children.Add(point);
                }
            }

            carcassPoints = new List<CarcassPoint>();

            Point3D x1, x2, y1, y2, z1, z2;

            for (double x = -xLength; x <= xLength; x += STEP)
            {
                for (double y = -yLength; y <= yLength; y += STEP)
                {
                    x1 = new Point3D(x, y, -zLength); // z = 0
                    x2 = new Point3D(x, y, zLength); // z = n

                    y1 = new Point3D(-xLength, y, x); // x = 0
                    y2 = new Point3D(xLength, y, x); // x = n

                    z1 = new Point3D(x, -yLength, y); // y = 0
                    z2 = new Point3D(x, yLength, y); // y = n

                    BoxPoint p1 = new BoxPoint(x1);
                    BoxPoint p2 = new BoxPoint(x2);
                    BoxPoint p3 = new BoxPoint(y1);
                    BoxPoint p4 = new BoxPoint(y2);
                    BoxPoint p5 = new BoxPoint(z1);
                    BoxPoint p6 = new BoxPoint(z2);

                    carcassPoints.Add(p1); // z = 0
                    carcassPoints.Add(p2); // z = n

                    carcassPoints.Add(p3); // x = 0
                    carcassPoints.Add(p4); // x = n

                    carcassPoints.Add(p5); // y = 0
                    carcassPoints.Add(p6); // y = n
                    //=================================
                    viewport.Children.Add(p1); // z = 0
                    viewport.Children.Add(p2); // z = n

                    viewport.Children.Add(p3); // x = 0
                    viewport.Children.Add(p4); // x = n

                    viewport.Children.Add(p5); // y = 0
                    viewport.Children.Add(p6); // y = n
                }
            }
        }

        public override void BuildCarcassWire(Viewport3D viewport)
        {
            carcassWire = new List<ScreenSpaceLines3D>();

            for (double x = -xLength; x <= xLength; x += STEP)
            {
                for (double y = -yLength; y <= yLength; y += STEP)
                {
                    Point3D x1 = new Point3D(x, y, -zLength); // z = 0
                    Point3D x2 = new Point3D(x, y, zLength); // z = n

                    Point3D y1 = new Point3D(-xLength, y, x); // x = 0
                    Point3D y2 = new Point3D(xLength, y, x); // x = n

                    Point3D z1 = new Point3D(x, -yLength, y); // y = 0
                    Point3D z2 = new Point3D(x, yLength, y); // y = n

                    if (y < yLength)
                    {
                        ShowCarcassWire(x1, new Point3D(x, y + STEP, -zLength), viewport);
                        ShowCarcassWire(x2, new Point3D(x, y + STEP, zLength), viewport);
                    }

                    if (x < xLength)
                    {
                        ShowCarcassWire(x1, new Point3D(x + STEP, y, -zLength), viewport);
                        ShowCarcassWire(x2, new Point3D(x + STEP, y, zLength), viewport);
                    }


                    if (y < yLength)
                    {
                        ShowCarcassWire(y1, new Point3D(-xLength, y + STEP, x), viewport);
                        ShowCarcassWire(y2, new Point3D(xLength, y + STEP, x), viewport);
                    }

                    if (x < xLength)
                    {
                        ShowCarcassWire(y1, new Point3D(-xLength, y, x + STEP), viewport);
                        ShowCarcassWire(y2, new Point3D(xLength, y, x + STEP), viewport);
                    }


                    if (y < yLength)
                    {
                        ShowCarcassWire(z1, new Point3D(x, -yLength, y + STEP), viewport);
                        ShowCarcassWire(z2, new Point3D(x, yLength, y + STEP), viewport);
                    }

                    if (x < xLength)
                    {
                        ShowCarcassWire(z1, new Point3D(x + STEP, -yLength, y), viewport);
                        ShowCarcassWire(z2, new Point3D(x + STEP, yLength, y), viewport);
                    }
                }
            }
        }
    }
}
