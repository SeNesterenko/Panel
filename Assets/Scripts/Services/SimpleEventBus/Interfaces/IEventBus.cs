using System;
using SimpleEventBus.Events;

namespace SimpleEventBus.Interfaces
{
    public interface IEventBus
    {
        IDisposable Subscribe<TEventBase>(Action<TEventBase> action) where TEventBase : EventBase;
        void Publish<TEventBase>(TEventBase eventItem) where TEventBase : EventBase;
    }
}