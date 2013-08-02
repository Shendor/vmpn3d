using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.ManagerClasses.Constructor
{
    public class ArcBuilderFactory
    {

        /// <summary>
        /// Get Arc Builder object by indicated type of arc
        /// </summary>
        /// <param name="pnObjectType">Type of Arc</param>
        /// <returns>Arc Builder. Null is not possible to return at this context</returns>
        public static ArcBuilder GetArcBuilder(PNObjectTypes pnObjectType)
        {
            ArcBuilder builder = new ArcBuilder(pnObjectType);
           
            return builder;
        }
    }
}
