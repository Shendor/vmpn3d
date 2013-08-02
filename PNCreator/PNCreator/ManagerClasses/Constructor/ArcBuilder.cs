using PNCreator.PNObjectsIerarchy;
using PNCreator.ManagerClasses.Exception;
using PNCreator.Properties;

namespace PNCreator.ManagerClasses.Constructor
{
    public class ArcBuilder : AbstractPNObjectBuilder
    {

        public ArcBuilder(PNObjectTypes pnObjectType) : base(pnObjectType) { }

        /// <summary>
        /// Build arc
        /// </summary>
        /// <param name="startObject">Start shape index</param>
        /// <param name="endObject">End shape index</param>
        /// <returns>Arc</returns>
        public Arc3D BuildArc(Shape3D startObject, Shape3D endObject)
        {
            Arc3D arc = null;
            switch (PNObjectType)
            {
                case PNObjectTypes.DiscreteArc:
                    arc = BuildDiscreteArc(startObject, endObject);
                    break;
                case PNObjectTypes.DiscreteInhibitorArc:
                    arc = BuildDiscreteInghibitorArc(startObject, endObject);
                    break;
                case PNObjectTypes.DiscreteTestArc:
                    arc = BuildDiscreteTestArc(startObject, endObject);
                    break;

                case PNObjectTypes.ContinuousArc:
                    arc = BuildContinuousArc(startObject, endObject);
                    break;
                case PNObjectTypes.ContinuousInhibitorArc:
                    arc = BuildContinuousInghibitorArc(startObject, endObject);
                    break;
                case PNObjectTypes.ContinuousTestArc:
                    arc = BuildContinuousTestArc(startObject, endObject);
                    break;
                case PNObjectTypes.ContinuousFlowArc:
                    arc = BuildFlowArc(startObject, endObject);
                    break;
            }
            
            if (arc == null)
            {
                throw new IllegalPNObjectException(Messages.Default.WrongPNObjectForArc);
            }

            return arc;
        }

        #region Arcs

        private Arc3D BuildDiscreteArc(Shape3D startObject, Shape3D endObject)
        {
            if ((startObject.Type == PNObjectTypes.DiscreteLocation && endObject.Type == PNObjectTypes.DiscreteTransition) ||
                (startObject.Type == PNObjectTypes.DiscreteTransition && endObject.Type == PNObjectTypes.DiscreteLocation))
            {
                if (startObject.Type == PNObjectTypes.DiscreteTransition)
                {
                    DiscreteLocation loc = (DiscreteLocation)endObject;
                    loc.IncomeTransitionsID.Add(startObject.ID);
                    ((DiscreteTransition)startObject).OutLocationAmount += 1;
                }
                else if (startObject.Type == PNObjectTypes.DiscreteLocation)
                {
                    DiscreteTransition trans = (DiscreteTransition)endObject;
                    trans.IncomeLocationsID.Add(startObject.ID);
                }
                Arc3D arc = new Arc3D(startObject, endObject, PNObjectTypes.DiscreteArc);

                return arc;
            }
            return null;
        }

        private Arc3D BuildDiscreteInghibitorArc(Shape3D startObject, Shape3D endObject)
        {
            if (startObject.Type == PNObjectTypes.DiscreteLocation &&
                (endObject.Type == PNObjectTypes.ContinuousTransition || endObject.Type == PNObjectTypes.DiscreteTransition))
            {
                if (endObject.Type == PNObjectTypes.DiscreteTransition || endObject.Type == PNObjectTypes.ContinuousTransition)
                {
                    Transition trans = (Transition)endObject;
                    trans.IncomeLocationsID.Add(startObject.ID);
                    trans.SALocations.Add(startObject.ID);
                }

                Arc3D arc = new Arc3D(startObject, endObject, PNObjectTypes.DiscreteInhibitorArc);

                return arc;
            }
            return null;
        }

        private Arc3D BuildDiscreteTestArc(Shape3D startObject, Shape3D endObject)
        {
            if (startObject.Type == PNObjectTypes.DiscreteLocation && (endObject.Type == PNObjectTypes.ContinuousTransition ||
                                   endObject.Type == PNObjectTypes.DiscreteTransition))
            {
                if (endObject.Type == PNObjectTypes.DiscreteTransition || endObject.Type == PNObjectTypes.ContinuousTransition)
                {
                    Transition trans = (Transition)endObject;
                    trans.IncomeLocationsID.Add(startObject.ID);
                    trans.SALocations.Add(startObject.ID);
                }

                Arc3D arc = new Arc3D(startObject, endObject, PNObjectTypes.DiscreteTestArc);

                return arc;
            }
            return null;
        }

        private Arc3D BuildContinuousArc(Shape3D startObject, Shape3D endObject)
        {
            if ((startObject.Type == PNObjectTypes.ContinuousLocation && endObject.Type == PNObjectTypes.ContinuousTransition) ||
                    (startObject.Type == PNObjectTypes.ContinuousTransition && endObject.Type == PNObjectTypes.ContinuousLocation))
            {
                if (startObject.Type == PNObjectTypes.ContinuousTransition)
                {
                    ContinuousLocation loc = (ContinuousLocation)endObject;
                    loc.IncomeTransitionsID.Add(startObject.ID);
                    ((ContinuousTransition)startObject).OutLocationAmount += 1;
                }
                else if (startObject.Type == PNObjectTypes.ContinuousLocation)
                {
                    ContinuousTransition trans = (ContinuousTransition)endObject;
                    trans.IncomeLocationsID.Add(startObject.ID);
                }
                Arc3D arc = new Arc3D(startObject, endObject, PNObjectTypes.ContinuousArc);

                return arc;
            }
            return null;
        }

        private Arc3D BuildContinuousInghibitorArc(Shape3D startObject, Shape3D endObject)
        {
            if ((startObject.Type == PNObjectTypes.ContinuousLocation && endObject.Type == PNObjectTypes.ContinuousTransition) ||
                   (startObject.Type == PNObjectTypes.ContinuousLocation && endObject.Type == PNObjectTypes.DiscreteTransition))
            {
                if (endObject.Type == PNObjectTypes.ContinuousTransition || endObject.Type == PNObjectTypes.DiscreteTransition)
                {
                    Transition trans = (Transition)endObject;
                    trans.IncomeLocationsID.Add(startObject.ID);
                    trans.SALocations.Add(startObject.ID);
                }

                Arc3D arc = new Arc3D(startObject, endObject, PNObjectTypes.ContinuousInhibitorArc);

                return arc;
            }
            return null;
        }

        private Arc3D BuildContinuousTestArc(Shape3D startObject, Shape3D endObject)
        {
            if (startObject.Type == PNObjectTypes.ContinuousLocation && (endObject.Type == PNObjectTypes.ContinuousTransition ||
                    startObject.Type == PNObjectTypes.DiscreteTransition))
            {
                if (endObject.Type == PNObjectTypes.ContinuousTransition || endObject.Type == PNObjectTypes.DiscreteTransition)
                {
                    Transition trans = (Transition)endObject;
                    trans.IncomeLocationsID.Add(startObject.ID);
                    //trans.IncomeArcsID.Add(arcs.Count);
                    trans.SALocations.Add(startObject.ID);
                }

                Arc3D arc = new Arc3D(startObject, endObject, PNObjectTypes.ContinuousTestArc);

                return arc;
            }
            return null;
        }

        private Arc3D BuildFlowArc(Shape3D startObject, Shape3D endObject)
        {
            if ((startObject.Type == PNObjectTypes.ContinuousLocation && endObject.Type == PNObjectTypes.DiscreteTransition) ||
                    (startObject.Type == PNObjectTypes.DiscreteTransition && endObject.Type == PNObjectTypes.ContinuousLocation))
            {
                if (startObject.Type == PNObjectTypes.DiscreteTransition)
                {
                    ContinuousLocation loc = (ContinuousLocation)endObject;
                    loc.IncomeTransitionsID.Add(startObject.ID);
                    ((DiscreteTransition)startObject).OutLocationAmount += 1;
                }
                else if (startObject.Type == PNObjectTypes.ContinuousLocation)
                {
                    DiscreteTransition trans = (DiscreteTransition)endObject;
                    trans.IncomeLocationsID.Add(startObject.ID);
                }
                Arc3D arc = new Arc3D(startObject, endObject, PNObjectTypes.ContinuousFlowArc);

                return arc;
            }
            return null;
        }

        #endregion
    }
}
