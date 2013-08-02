using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.ManagerClasses.Constructor
{
    public class ShapeBuilderFactory
    {

        /// <summary>
        /// Get PNObject Builder object by indicated type of PN object
        /// </summary>
        /// <param name="pnObjectType">Type of PN Object</param>
        /// <returns>PNObject Builder. Null is not possible to return at this context</returns>
        public static AbstractPNObjectBuilder GetShapeBuilder(PNObjectTypes pnObjectType)
        {
            AbstractPNObjectBuilder builder = null;
            switch (pnObjectType)
            {
                case PNObjectTypes.Membrane:
                case PNObjectTypes.StructuralMembrane: builder = new MembraneBuilder(pnObjectType); break;

                case PNObjectTypes.DiscreteLocation:
                case PNObjectTypes.ContinuousLocation: builder = new LocationBuilder(pnObjectType); break;

                case PNObjectTypes.DiscreteTransition:
                case PNObjectTypes.ContinuousTransition: builder = new TransitionBuilder(pnObjectType); break;
            }
            return builder;
        }
    }
}
