
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.ManagerClasses.EventManager.Events
{
    public class PNObjectChangedEventArgs : AbstractEventArgs
    {
        public PNObjectChangedEventArgs(PNObject pnObject)
        {
            PNObject = pnObject;
        }

        public PNObject PNObject
        {
            get; set;
        }
    }
}
