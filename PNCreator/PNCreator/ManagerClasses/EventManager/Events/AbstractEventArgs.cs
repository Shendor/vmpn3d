using System;

namespace PNCreator.ManagerClasses.EventManager.Events
{
    public abstract class AbstractEventArgs
    {
        public AbstractEventArgs()
        {
        }

        public AbstractEventArgs(object source)
        {
            Source = source;
        }

        public object Source
        {
            get; set;
        }
    }
}
