using PNCreator.PNObjectsIerarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNCreator.ManagerClasses.EventManager.Events
{
    public class PNObjectsValuesChangedEventArgs : AbstractEventArgs
    {
        public PNObjectsValuesChangedEventArgs(ICollection<PNObject> pnObjects)
        {
            PNObjects = pnObjects;
        }

        public ICollection<PNObject> PNObjects
        {
            get; set;
        }
    }
}
