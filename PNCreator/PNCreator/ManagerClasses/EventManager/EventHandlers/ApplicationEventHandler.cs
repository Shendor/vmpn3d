
using PNCreator.ManagerClasses.EventManager.Events;

namespace PNCreator.ManagerClasses.EventManager.EventHandlers
{
    public delegate void ApplicationEventHandler<in T>(T args);
}
