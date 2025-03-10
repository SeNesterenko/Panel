namespace Transponder.Panel.Buttons.Presenters
{
    public class BKSPButtonPresenter : BasePanelButtonPresenter
    {
        public BKSPButtonPresenter(PanelActionButton buttonView, PanelView panelView,
            ActionButtonType type, PanelController.IEventReceiver eventReceiver) 
            : base(buttonView, panelView, type, eventReceiver)
        {
        }

        protected override void HandleActionButton()
        {
            if (_panelView.CodeInputTitle.text.Length == 0)
                return;
            
            _panelView.CodeInputTitle.text = _panelView.CodeInputTitle.text[..^1];
        }
    }
}