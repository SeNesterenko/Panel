namespace Transponder.Panel.Buttons.Presenters
{
    public class PressButtonPresenter : BasePanelButtonPresenter
    {
        public PressButtonPresenter(PanelActionButton buttonView, PanelView panelView,
            ActionButtonType type, PanelController.IEventReceiver eventReceiver) 
            : base(buttonView, panelView, type, eventReceiver)
        {
        }

        protected override void HandleActionButton()
        {
            if (_panelView.CodeInputTitle.text.Length >= 4)
                return;

            _panelView.CodeInputTitle.text += _data.Index;

            if (_panelView.CodeInputTitle.text.Length < 4) 
                return;
            
            _eventReceiver.UpdateCurrentState(PanelState.Default);
            _panelView.CodeTitle.text = _panelView.CodeInputTitle.text;
                
            _panelView.CodeTitle.gameObject.SetActive(true);
            _panelView.CodeInputTitle.transform.parent.gameObject.SetActive(false);
        }

        public override void UpdateData(ActionButtonData data, PanelActionButton buttonView)
        {
            base.UpdateData(data, buttonView);
            _buttonView.ChangeDisplayText(data.Index.ToString());
        }
    }
}