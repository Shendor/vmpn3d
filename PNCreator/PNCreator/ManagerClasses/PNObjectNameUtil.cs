using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.ManagerClasses
{
    public class PNObjectNameUtil
    {
        /// <summary>
        /// Return object name
        /// </summary>
        /// <param name="objectType">Object type</param>
        /// <returns></returns>
        public static string GetPNObjectName(PNObjectTypes objectType)
        {
            switch (objectType)
            {
                case PNObjectTypes.DiscreteLocation:
                    return "DL";
                case PNObjectTypes.DiscreteTransition:
                    return "DT";

                case PNObjectTypes.ContinuousLocation:
                    return "CL";
                case PNObjectTypes.ContinuousTransition:
                    return "CT";

                case PNObjectTypes.Membrane:
                    return "M";
                case PNObjectTypes.StructuralMembrane:
                    return "SM";

                case PNObjectTypes.DiscreteArc:
                    return "DA";
                case PNObjectTypes.DiscreteInhibitorArc:
                    return "DIA";
                case PNObjectTypes.DiscreteTestArc:
                    return "DTA";

                case PNObjectTypes.ContinuousFlowArc:
                    return "FA";
                case PNObjectTypes.ContinuousArc:
                    return "CA";
                case PNObjectTypes.ContinuousInhibitorArc:
                    return "CIA";
                case PNObjectTypes.ContinuousTestArc:
                    return "CTA";
            }

            return null;
        }
    }
}
