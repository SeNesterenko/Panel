using System;
using System.Collections.Generic;

namespace SimpleEventBus.Interfaces
{
    public interface IDebuggableEventBus : IEventBus
    {
        Dictionary<Type, List<ISubscriptionHolder>> Subscriptions { get; }
    }
}