using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using PNCreator.ManagerClasses;

namespace PNCreator.Controls.CarcassControl.Carcasses
{
    class CircleCarcass : AbstractCarcass
    {
        private static readonly int CARCASS_SIZE = 16;

        public override void BuildCarcass(System.Windows.Controls.Viewport3D viewport)
        {
            carcassPoints = new List<CarcassPoint>();

            double step = 360 / CARCASS_SIZE;
            double angle = 0;
            for (int i = 0; i < CARCASS_SIZE; i++, angle += step)
            {
                Point3D pointPosition = _3DTools.MathUtils.GetCirclePoint(angle,
                                                                            PNCreator.Controls.SectorControl.SectoredCircle.DEFAULT_RADIUS);

                BoxPoint point = new BoxPoint(pointPosition);
                //point.Material = PNObjectMaterial.GetPlainMaterial(PNColors.CarcassPoint);
                point.Size = 2;
                carcassPoints.Add(point);
                viewport.Children.Add(point);
            }
        }

        public override void BuildCarcassWire(System.Windows.Controls.Viewport3D viewport)
        {
            
        }
    }
}
