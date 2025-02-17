namespace Transponder.Buttons.Presenters
{
    public class VFRButtonPresenter : BasePanelButtonPresenter
    {
        private const string VFR_CODE = "1200";
        
        private string _previousCode;

        public VFRButtonPresenter(PanelActionButton buttonView, PanelView panelView,
            ActionButtonType type, PanelController.IEventReceiver eventReceiver)
            : base(buttonView, panelView, type, eventReceiver)
        {
        }

        protected override void HandleActionButton()
        {
            if (_panelView.CodeTitle.text is VFR_CODE)
                _panelView.CodeTitle.text = _previousCode;
            else
            {
                _previousCode = _panelView.CodeTitle.text;
                
                _eventReceiver.ResetInformationSettings();
                _panelView.CodeTitle.text = VFR_CODE;
            }
        }
    }
}