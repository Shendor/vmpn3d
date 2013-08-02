using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.PNObjectsIerarchy;
using System.Windows.Media.Media3D;
using PNCreator.Controls;

namespace PNCreator.ManagerClasses.Constructor
{
    public class TransitionBuilder : AbstractShapeBuilder
    {
        public TransitionBuilder(PNObjectTypes pnObjectType) : base(pnObjectType) { }

        public override Shape3D BuildShape(Point3D position)
        {
            Transition transition = null;

            if (PNObjectType == PNObjectTypes.DiscreteTransition)
            {
                transition = new DiscreteTransition(position);
            }
            else if (PNObjectType == PNObjectTypes.ContinuousTransition)
            {
                transition = new ContinuousTransition(position);
            }

            return transition;
        }
    }
}
