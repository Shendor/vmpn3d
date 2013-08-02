using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using PNCreator.ManagerClasses.EventManager.EventHandlers;
using PNCreator.Properties;

namespace PNCreator.ManagerClasses.EventManager
{
    public class EventPublisher
    {
        private readonly Dictionary<Type, ArrayList> events;

        public EventPublisher()
        {
            events = new Dictionary<Type, ArrayList>();
        }

        public void Register<T>(ApplicationEventHandler<T> applicationEventHandler)
        {
            ParameterInfo[] parameters = applicationEventHandler.Method.GetParameters();
            if (parameters.Length != 1)
            {
                throw new InvalidParametersCountException(Messages.Default.InvalidParametersCount);
            }

            Type eventArgumentType = parameters[0].ParameterType;
            if (!events.ContainsKey(eventArgumentType))
            {
                events.Add(eventArgumentType, new ArrayList());
            }
            events[eventArgumentType].Add(applicationEventHandler);
        }


        public void ExecuteEvents<T>(T arg)
        {
            if (events.ContainsKey(arg.GetType()))
            {
                foreach (var eventHandler in events[arg.GetType()])
                {
                    ((ApplicationEventHandler<T>)eventHandler)(arg);
                }
            }
        }

        public void UnRegister(Type type)
        {
            events.Remove(type);
        }
    }
}
