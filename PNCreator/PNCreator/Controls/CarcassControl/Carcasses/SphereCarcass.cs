using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Controls;

namespace PNCreator.Controls.CarcassControl
{
    class SphereCarcass : AbstractCarcass
    {
        public SphereCarcass() : base()
        {
            xLength = yLength = zLength = 50;
        }

        /// <summary>
        /// Get point on sphere by Theta-angle and Pi-angleh
        /// </summary>
        /// <param name="theta">Theta-angle</param>
        /// <param name="phi">Phi-angle</param>
        /// <returns>Point3D on sphere</returns>
        private Point3D GetPosition(double theta, double phi)
        {
            theta *= Math.PI / 180.0;
            phi *= Math.PI / 180.0;

            double x = xLength * Math.Sin(theta) * Math.Sin(phi);
            double y = yLength * Math.Cos(phi);
            double z = zLength * Math.Cos(theta) * Math.Sin(phi);

            Point3D pt = new Point3D(x, y, z);
            pt += center;

            return pt;
        }

        public override void BuildCarcass(Viewport3D viewport)
        {
            if (carcassPoints != null && carcassPoints.Count > 0)
            {
                foreach (CarcassPoint point in carcassPoints)
                {
                    viewport.Children.Add(point);
                }
                return;
            }
            carcassPoints = new List<CarcassPoint>();

            int thetaDiv = 16;
            int phiDiv = 16;
            double dt = 360.0 / thetaDiv;
            double dp = 180.0 / phiDiv;

            for (int i = 0; i <= phiDiv; i++)
            {
                double phi = i * dp;

                for (int j = 0; j <= thetaDiv-1; j++)
                {
                    double theta = j * dt;

                    BoxPoint spherePoint = new BoxPoint(GetPosition(theta, phi));
                    carcassPoints.Add(spherePoint);
                    viewport.Children.Add(spherePoint);
                }
            }
           
        }

        public override void BuildCarcassWire(Viewport3D viewport)
        {
            Point3D[] p = new Point3D[2];
            carcassWire = new List<_3DTools.ScreenSpaceLines3D>();
            for (int s = 0; s < carcassPoints.Count - 3; s++)
            {
                p[0] = carcassPoints[s].Position;
                p[1] = carcassPoints[s+1].Position;

                ShowCarcassWire(p[0], p[1], viewport);
            }
        }

       
    }
}
