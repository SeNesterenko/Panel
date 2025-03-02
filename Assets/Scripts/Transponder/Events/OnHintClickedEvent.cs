using SimpleEventBus.Events;

namespace Transponder.Events
{
    public class OnHintClickedEvent : EventBase
    {
        public string ResponderCode { get; }

        public OnHintClickedEvent(string responderCode) => 
            ResponderCode = responderCode;
    }
}