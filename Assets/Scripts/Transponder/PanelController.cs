namespace Transponder
{
    public class PanelController : IPanelController
    {
        private PanelState _currentState;
        
        public void Initialize(PanelView container)
        {
            foreach (var button in container.Buttons) 
                button.Initialize(this);
        }

        public void OnButtonPressed(PanelActionButton button)
        {
            var type = button.Data.Type;
            switch (type)
            {
                case ActionButtonType.Press:
                    if (_currentState != PanelState.Code)
                        return;
                    break;
                
                case ActionButtonType.Code:
                    _currentState = PanelState.Code;
                    break;
            }
        }

        public void Dispose()
        {
            
        }
    }
}