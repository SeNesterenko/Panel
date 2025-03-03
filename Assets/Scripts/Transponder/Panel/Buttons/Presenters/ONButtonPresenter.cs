namespace Transponder.Panel.Buttons.Presenters
{
    public class ONButtonPresenter : BasePanelButtonPresenter
    {
        private const string ON_TEXT = "ON";
        
        public ONButtonPresenter(ActionButtonData data, PanelActionButton buttonView, PanelView panelView, 
            ActionButtonType type, PanelController.IEventReceiver eventReceiver) 
            : base(buttonView, panelView, type, eventReceiver)
        {
        }

        protected override void HandleActionButton()
        {
            _eventReceiver.UpdateCurrentMode(TransmissionMode.ON);
            _eventReceiver.ResetInformationSettings();
            
            _panelView.ModeTitle.text = ON_TEXT;
        }
    }
}