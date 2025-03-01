using SimpleEventBus.Events;
using Transponder.Panel.Buttons;

namespace Transponder.Events
{
    public class OnButtonClickedEvent : EventBase
    {
        public ActionButtonType ButtonType { get; }

        public OnButtonClickedEvent(ActionButtonType buttonType) => 
            ButtonType = buttonType;
    }
}