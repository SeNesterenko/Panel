using UnityEngine;

namespace Transponder.Panel.Buttons.Presenters
{
    public class BACKButtonPresenter : BasePanelButtonPresenter
    {
        public BACKButtonPresenter(PanelActionButton buttonView, PanelView panelView, 
            ActionButtonType type, PanelController.IEventReceiver eventReceiver) 
            : base(buttonView, panelView, type, eventReceiver)
        {
        }

        protected override void HandleActionButton()
        {
            switch (_eventReceiver.GetCurrentState())
            {
                case PanelState.XPDR:
                    _eventReceiver.UpdateCurrentState(PanelState.Default);
                    break;
                case PanelState.CODE:
                    _eventReceiver.UpdateCurrentState(PanelState.XPDR);
                    break;
                case PanelState.Default:
                default:
                    Debug.LogError("Invalid state");
                    return;
            }
        }
    }
}