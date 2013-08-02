using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Modules.Simulation.Service
{
    public interface ISimulationHistoryDao
    {
        void LoadSimulationData(string fileName, ICollection<PNObject> pnObjects);

        void SaveSimulationData(string fileName, ICollection<PNObject> pnObjects);
    }
}
