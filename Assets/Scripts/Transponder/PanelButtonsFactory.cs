using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Transponder
{
    [UsedImplicitly] //Register in DI Container
    public class PanelButtonsFactory : IPanelButtonsFactory
    {
        private const string BUTTON_PREFAB_PATH = "Prefabs/PanelActionButton";
        
        private readonly IPanelConfigProvider _configProvider;
        
        private PanelActionButton _buttonPrefab;
        private List<PanelActionButton> _buttons;

        public PanelButtonsFactory(IPanelConfigProvider configProvider) => 
            _configProvider = configProvider;

        public List<PanelActionButton> CreateButtons(PanelState state, Transform root, PanelActionButton.IEventReceiver receiver)
        {
            var buttons = new List<PanelActionButton>();
            
            foreach (var type in _configProvider.ButtonsSetup.ButtonsOrderByState[state])
            {
                var presetData = _configProvider.ButtonsPreset.Buttons[type];
                var view = Object.Instantiate(GetPrefab(), root);
                
                view.Initialize(receiver);
                view.UpdateData(new ActionButtonData(presetData, false, type));
                
                buttons.Add(view);
            }

            if (_buttons is null || _buttons.Count > 0)
                _buttons = buttons;
            else
            {
                Dispose();
                _buttons = buttons;
            }
            return buttons;
        }
        
        private PanelActionButton GetPrefab() =>
            _buttonPrefab ? _buttonPrefab : _buttonPrefab = Resources.Load<PanelActionButton>(BUTTON_PREFAB_PATH);

        public void Dispose()
        {
            if (_buttons is null)
                return;
            
            foreach (var button in _buttons)
                Object.Destroy(button);

            _buttons.Clear();
        }
    }
}