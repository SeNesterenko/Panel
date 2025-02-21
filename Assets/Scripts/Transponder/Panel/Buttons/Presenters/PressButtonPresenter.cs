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
            if (_panelView.CodeTitle.text.Length >= 4)
                return;

            _panelView.CodeTitle.text += _data.Index;
            
            if (_panelView.CodeTitle.text.Length >= 4)
                _eventReceiver.UpdateCurrentState(PanelState.Default);
        }

        public override void UpdateData(ActionButtonData data, PanelActionButton buttonView)
        {
            base.UpdateData(data, buttonView);
            _buttonView.ChangeDisplayText(data.Index.ToString());
        }
    }
}