using SimpleEventBus.Events;

namespace Transponder.Events
{
    public class OnNewResponderCodeEnteredEvent : EventBase
    {
        public string ResponderCode { get; }

        public OnNewResponderCodeEnteredEvent(string responderCode) => 
            ResponderCode = responderCode;
    }
}