namespace Transponder.Panel.Buttons.Presenters
{
    public class CODEButtonPresenter : BasePanelButtonPresenter
    {
        public CODEButtonPresenter(ActionButtonData data, PanelActionButton buttonView, PanelView panelView,
            ActionButtonType type, PanelController.IEventReceiver eventReceiver) 
            : base(buttonView, panelView, type, eventReceiver)
        {
        }

        protected override void HandleActionButton()
        {
            _eventReceiver.UpdateCurrentState(PanelState.CODE);

            _eventReceiver.ResetInformationSettings();
            _panelView.CodeTitle.text = string.Empty;
        }
    }
}