namespace Transponder.Buttons.Presenters
{
    public class BasePanelButtonPresenter : PanelActionButton.IEventReceiver
    {
        protected readonly PanelView _panelView;
        protected readonly PanelController.IEventReceiver _eventReceiver;

        protected PanelActionButton _buttonView;
        protected ActionButtonData _data;

        public BasePanelButtonPresenter(PanelActionButton buttonView, PanelView panelView, ActionButtonType type, PanelController.IEventReceiver eventReceiver)
        {
            _buttonView = buttonView;
            _panelView = panelView;
            _eventReceiver = eventReceiver;
            
            Type = type;

            _buttonView.SetEventReceiver(this);
        }
        
        public ActionButtonType Type { get; }

        public void OnButtonPressed() => 
            HandleActionButton();

        public void SetInteractable(bool isInteractable) => 
            _buttonView.SetInteractable(isInteractable);
        
        public virtual void UpdateData(ActionButtonData data, PanelActionButton buttonView)
        {
            _data = data;
            _buttonView = buttonView;
            
            _buttonView.UpdateState(data);
            _buttonView.RemoveEventReceiver();
            _buttonView.SetEventReceiver(this);
        }

        protected virtual void HandleActionButton() { }
    }
}