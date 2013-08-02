using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.PNObjectsIerarchy;
using System.Windows.Media.Media3D;
using PNCreator.Controls;

namespace PNCreator.ManagerClasses.Constructor
{
    public class MembraneBuilder : AbstractShapeBuilder
    {
        public MembraneBuilder(PNObjectTypes pnObjectType) : base(pnObjectType) { }

        #region IPNObjectBuilder Members

        public override Shape3D BuildShape(Point3D position)
        {
            Membrane membrane = null;

            if (PNObjectType == PNObjectTypes.Membrane)
            {
                membrane = new Membrane(position);
            }
            else if (PNObjectType == PNObjectTypes.StructuralMembrane)
            {
                membrane = new StructuralMembrane(position);
            }

            return membrane;
        }

        #endregion
    }
}
