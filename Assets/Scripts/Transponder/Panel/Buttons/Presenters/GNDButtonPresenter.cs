namespace Transponder.Panel.Buttons.Presenters
{
    public class GNDButtonPresenter : BasePanelButtonPresenter
    {
        private const string GND_TEXT = "GND";
        
        public GNDButtonPresenter(PanelActionButton buttonView, PanelView panelView,
            ActionButtonType type, PanelController.IEventReceiver eventReceiver) 
            : base(buttonView, panelView, type, eventReceiver)
        {
        }

        protected override void HandleActionButton()
        {
            _eventReceiver.UpdateCurrentMode(TransmissionMode.GND);
            _eventReceiver.ResetInformationSettings();
            
            _panelView.ModeTitle.text = GND_TEXT;
        }
    }
}