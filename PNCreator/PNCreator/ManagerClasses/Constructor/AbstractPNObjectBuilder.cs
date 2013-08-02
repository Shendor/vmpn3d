using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.PNObjectsIerarchy;
using System.Windows.Media.Media3D;
using System.Windows.Controls;
using PNCreator.Controls;

namespace PNCreator.ManagerClasses.Constructor
{
    public abstract class AbstractPNObjectBuilder
    {
        protected PNObjectTypes PNObjectType
        {
            get;
            set;
        }

        public AbstractPNObjectBuilder(PNObjectTypes pnObjectType)
        {
            PNObjectType = pnObjectType;
        }

       
    }
}
