using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.PNObjectsIerarchy;
using System.Windows.Media.Media3D;
using PNCreator.Controls;

namespace PNCreator.ManagerClasses.Constructor
{
    public abstract class AbstractShapeBuilder : AbstractPNObjectBuilder
    {

        public AbstractShapeBuilder(PNObjectTypes pnObjectType) : base(pnObjectType) { }

        /// <summary>
        /// Builds PN object, add it to viewport and return it
        /// </summary>
        /// <param name="position">Object position</param>
        /// <param name="viewport">Viewport</param>
        /// <returns>PN Object</returns>
        public abstract Shape3D BuildShape(Point3D position);
    }
}
