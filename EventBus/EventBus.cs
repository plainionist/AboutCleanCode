using System;
using System.Collections.Generic;
using System.Linq;

namespace TestFailureAnalyzer.Core
{
    public class EventBus
    {
        private class EventHandler
        {
            public Type EventType;
            public object HandlerRef;
            public Action<object> Invoke;
        }

        private readonly List<EventHandler> myHandlers;

        public EventBus()
        {
            myHandlers = new List<EventHandler>();
        }

        public void Publish<T>(T evt)
        {
            foreach (var entry in myHandlers.Where(x => x.EventType.IsAssignableFrom(evt.GetType())).ToList())
            {
                entry.Invoke(evt);
            }
        }

        public void Subscribe<T>(Action<T> handler) 
        {
            myHandlers.Add(new EventHandler
            {
                EventType = typeof(T),
                HandlerRef = handler,
                Invoke = x => handler((T)x)
            });
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            foreach (var entry in myHandlers.Where(x => Object.ReferenceEquals(x.HandlerRef, handler)).ToList())
            {
                myHandlers.Remove(entry);
            }
        }
    }
}