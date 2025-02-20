namespace Transponder.Panel.Buttons.Presenters
{
    public class ALTButtonPresenter : BasePanelButtonPresenter
    {
        private const string ALT_TEXT = "ALT";
        
        public ALTButtonPresenter(PanelActionButton buttonView, PanelView panelView,
            ActionButtonType type, PanelController.IEventReceiver eventReceiver) 
            : base(buttonView, panelView, type, eventReceiver)
        {
        }

        protected override void HandleActionButton()
        {
            _eventReceiver.UpdateCurrentMode(TransmissionMode.ALT);
            _eventReceiver.ResetInformationSettings();
            _panelView.ModeTitle.text = ALT_TEXT;
            
            //ToDo: Send Locator Event (New SendData)
        }
    }
}