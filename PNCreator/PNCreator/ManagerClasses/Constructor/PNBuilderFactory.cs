using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.ManagerClasses.Constructor
{
    public class PNBuilderFactory
    {
        
        /// <summary>
        /// Get PNObject Builder object by indicated type of PN object
        /// </summary>
        /// <param name="pnObjectType">Type of PN Object</param>
        /// <returns>PNObject Builder. Null is not possible to return at this context</returns>
        public static AbstractPNObjectBuilder GetPNObjectBuilder(PNObjectTypes pnObjectType)
        {
            switch (pnObjectType)
            {
                case PNObjectTypes.None: return null;
                case PNObjectTypes.Membrane:
                case PNObjectTypes.StructuralMembrane:
                case PNObjectTypes.DiscreteLocation:
                case PNObjectTypes.ContinuousLocation:
                case PNObjectTypes.DiscreteTransition:
                case PNObjectTypes.ContinuousTransition: return ShapeBuilderFactory.GetShapeBuilder(pnObjectType);

                default: return ArcBuilderFactory.GetArcBuilder(pnObjectType);
            }
        }

    }
}
