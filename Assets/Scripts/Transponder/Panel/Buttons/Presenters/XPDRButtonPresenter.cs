namespace Transponder.Panel.Buttons.Presenters
{
    public class XPDRButtonPresenter : BasePanelButtonPresenter
    {
        public XPDRButtonPresenter(PanelActionButton buttonView, PanelView panelView,
            ActionButtonType type, PanelController.IEventReceiver receiver) 
            : base(buttonView, panelView, type, receiver)
        {
        }

        protected override void HandleActionButton() => 
            _eventReceiver.UpdateCurrentState(PanelState.XPDR);
    }
}