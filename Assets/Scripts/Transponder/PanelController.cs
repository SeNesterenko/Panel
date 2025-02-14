using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Transponder
{
    [UsedImplicitly] //Register in DI Container
    public class PanelController : IPanelController
    {
        private readonly IPanelButtonsFactory _factory;
        private readonly IPanelConfigProvider _configProvider;
        private readonly ButtonsPreset _buttonsPreset;
        private readonly ButtonsSetup _buttonsSetup;
        
        private List<PanelActionButton> _buttons;
        private PanelState _currentState;
        private PanelView _container;

        public PanelController(IPanelButtonsFactory factory, IPanelConfigProvider configProvider)
        {
            _factory = factory;
            _configProvider = configProvider;
        }
        
        public void Initialize(PanelView container)
        {
            _container = container;
            _currentState = PanelState.Default;
            
            _buttons = _factory.CreateButtons(_currentState, _container.Container, this);
        }

        public void OnButtonPressed(PanelActionButton button)
        {
            var type = button.Data.Type;
            switch (type)
            {
                case ActionButtonType.XPDR:
                    if (_currentState is PanelState.XPDR)
                        Debug.LogError("XPDR has already been selected!!!");
                    
                    UpdateButtonsState(_currentState);
                    _currentState = PanelState.XPDR;
                    break;
                case ActionButtonType.PFD:
                case ActionButtonType.INSET:
                case ActionButtonType.OBS:
                case ActionButtonType.CDI:
                case ActionButtonType.ADF:
                default:
                    return;
            }
        }
        
        private void UpdateButtonsState(PanelState state)
        {
            if (_buttons.Count != _configProvider.ButtonsSetup.ButtonsOrderByState[state].Count)
            {
                Debug.LogError("Buttons count != buttons count from the setup!!!");
                return;
            }

            var buttonsTypes = _configProvider.ButtonsSetup.ButtonsOrderByState[state];
            
            for (var index = 0; index < _buttons.Count; index++)
            {
                var button = _buttons[index];
                var type = buttonsTypes[index];
                
                button.UpdateData(new ActionButtonData(_configProvider.ButtonsPreset.Buttons[type], false, type));
            }
        }

        public void Dispose()
        {
            
        }
    }
}