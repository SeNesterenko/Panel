namespace Transponder.Panel.Buttons.Presenters
{
    public class PressButtonPresenter : BasePanelButtonPresenter
    {
        public PressButtonPresenter(ActionButtonData data, PanelActionButton buttonView, PanelView panelView,
            ActionButtonType type, PanelController.IEventReceiver eventReceiver) 
            : base(buttonView, panelView, type, eventReceiver)
        {
        }

        protected override void HandleActionButton()
        {
            if (_panelView.CodeTitle.text.Length >= 4)
                return;
            
            _panelView.CodeTitle.text += _data.Index;
        }

        public override void UpdateData(ActionButtonData data, PanelActionButton buttonView)
        {
            base.UpdateData(data, buttonView);
            _buttonView.ChangeDisplayText(data.Index.ToString());
        }
    }
}