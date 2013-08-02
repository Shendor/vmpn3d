
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.ManagerClasses.EventManager.Events
{
    public class OpenNetEventArgs : AbstractEventArgs
    {

        public PNObjectDictionary<long, PNObject> PNObjects
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }
    }
}
