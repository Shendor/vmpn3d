using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.ManagerClasses
{
    public class PNObjectRepository
    {
        static PNObjectRepository()
        {
            PNObjects = new PNObjectDictionary<long, PNObject>();
        }

        public static int Count
        {
            get { return PNObjects.Count; }
        }

        /// <summary>
        /// Get or set collection of PNObject.
        /// This collection is Key-Value pair where key is ID property of PNObject
        /// and value - PNObject itself
        /// </summary>
        public static PNObjectDictionary<long, PNObject> PNObjects
        {
            get;
            set;
        }

        /// <summary>
        /// Get PNObject by key
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>PNObject</returns>
        public static PNObject GetByKey(long key)
        {
            PNObject pnObject = null;
            if (key >= 0)
                PNObjects.TryGetValue(key, out pnObject);

            return pnObject;
        }

        /// <summary>
        /// Get IEnumerable collection of PNObject by indicated PNObjectTypes type
        /// </summary>
        /// <param name="type">PNObjectTypes type</param>
        /// <returns>Collection of PNObjects</returns>
        public static IEnumerable<PNObject> GetPNObjectsByType(PNObjectTypes type)
        {
            foreach (PNObject pnObject in PNObjects.Values)
            {
                if (pnObject.Type == type)
                    yield return pnObject;
            }
        }

        /// <summary>
        /// Get IEnumerable collection of PNObject by type
        /// </summary>
        /// <typeparam name="T">Type of PNObject (class or interface)</typeparam>
        /// <returns>Collection of objects with derived type</returns>
        public static IEnumerable<T> GetPNObjects<T>() where T : PNObject
        {
            foreach (PNObject pnObject in PNObjects.Values)
            {
                if (pnObject is T)
                    yield return (T)pnObject;
            }
        }

        public static IEnumerable<Arc3D> GetArcs()
        {
            // ? it was created because of empty collection from GetPNObjects<T>
            List<Arc3D> arcs = new List<Arc3D>();
            foreach (PNObject pnObject in PNObjects.Values)
            {
                if (pnObject is Arc3D)
                    arcs.Add((Arc3D)pnObject);
            }
            return arcs;
        }
    }
}
