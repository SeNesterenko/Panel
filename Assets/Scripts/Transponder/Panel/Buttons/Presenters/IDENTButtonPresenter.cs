using Cysharp.Threading.Tasks;

namespace Transponder.Panel.Buttons.Presenters
{
    public class IDENTButtonPresenter : BasePanelButtonPresenter
    {
        private const string IDENT_TEXT = "IDNT";
        private const int IDENT_TIME = 10;
        
        public IDENTButtonPresenter(PanelActionButton buttonView, PanelView panelView,
            ActionButtonType type, PanelController.IEventReceiver eventReceiver) 
            : base(buttonView, panelView, type, eventReceiver)
        {
        }

        protected override void HandleActionButton()
        {
            if (_eventReceiver.GetCurrentMode() is TransmissionMode.STBY)
                return;
            
            _eventReceiver.ResetInformationSettings();
            HandleActionAsync().Forget();
        }

        private async UniTask HandleActionAsync()
        {
            _eventReceiver.UpdateCurrentState(PanelState.Default);
            _eventReceiver.SetButtonsLockState(false);
            
            var previousTitle = _panelView.ModeTitle.text;
            _panelView.ModeTitle.text = IDENT_TEXT;

            await UniTask.Delay(IDENT_TIME * 1000);

            _panelView.ModeTitle.text = previousTitle;
            _eventReceiver.SetButtonsLockState(true);
        }
    }
}