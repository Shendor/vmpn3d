using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.ManagerClasses
{
    class PNProgramStorage
    {
        #region Variables
        [Obsolete("Use FormulaManager")]
        public static bool IsNeedToCompile
        {
            get;
            set;
        }

        /*[Obsolete("Use GetSimulationNames method")]
        public static List<string> SimulationNames
        {
            get;
            set;
        }*/
       /* public static int SimulationNumber
        {
            get;
            set;
        }*/

        static PNProgramStorage()
        {
//            SimulationNames = new List<string>();
//            SimulationNumber = 0;
            IsNeedToCompile = false;
        }

        public static List<string> GetSimulationNames(IEnumerable<PNObject> pnObjects)
        {
            HashSet<string> simulationNames = new HashSet<string>();
            foreach (var pnObject in pnObjects)
            {
                foreach (string simulationName in pnObject.ObjectHistory.Keys)
                {
                    simulationNames.Add(simulationName);
                }
            }
            return new List<string>(simulationNames);
        }
        #endregion
    }
}
