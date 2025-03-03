namespace Transponder.Panel.Buttons.Presenters
{
    public class STBYButtonPresenter : BasePanelButtonPresenter
    {
        private const string STBY_TEXT = "STBY";
        
        public STBYButtonPresenter(PanelActionButton buttonView, PanelView panelView,
            ActionButtonType type, PanelController.IEventReceiver eventReceiver) 
            : base(buttonView, panelView, type, eventReceiver)
        {
        }

        protected override void HandleActionButton()
        {
            _eventReceiver.UpdateCurrentMode(TransmissionMode.STBY);
            _eventReceiver.ResetInformationSettings();
            
            _panelView.ModeTitle.text = STBY_TEXT;
        }
    }
}