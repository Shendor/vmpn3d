using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using PNCreator.Controls;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.ManagerClasses.Constructor
{
    public class LocationBuilder : AbstractShapeBuilder
    {
        public LocationBuilder(PNObjectTypes pnObjectType) : base(pnObjectType) { }

        #region IPNObjectBuilder Members

        public override Shape3D BuildShape(Point3D position)
        {
            Location location = null;

            if (PNObjectType == PNObjectTypes.DiscreteLocation)
            {
                location = new DiscreteLocation(position);
            }
            else if (PNObjectType == PNObjectTypes.ContinuousLocation)
            {
                location = new ContinuousLocation(position);
            }

            return location;
        }

        #endregion
    }
}
