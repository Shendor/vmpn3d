using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.ManagerClasses
{
    public class IdGenerator
    {
        private static long id;

        public static long ID
        {
            get
            {
                return id++;
            }
            set
            {
                id = value;
            }
        }

        public static long CurrentID
        {
            get
            {
                return id;
            }
        }

        public static void UpdateID(IDictionary<long, PNObject> pnObjects)
        {
            id = pnObjects.Keys.Max() + 1;
        }
    }
}
