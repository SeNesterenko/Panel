using JetBrains.Annotations;
using UnityEngine;

namespace Transponder
{
    [UsedImplicitly] //Register in DI Container
    public class PanelController : IPanelController
    {
        private const string CONFIGS_BUTTONS_PRESET = "Configs/ButtonsPreset";
        
        private PanelState _currentState;
        private readonly ButtonsPreset _buttonsPreset;

        public PanelController()
        {
            _currentState = PanelState.Default;
            _buttonsPreset = Resources.Load<ButtonsPreset>(CONFIGS_BUTTONS_PRESET);
        }
        
        public void Initialize(PanelView container)
        {
            foreach (var button in container.Buttons)
            {
                button.Initialize(this);
                
                var presetData = _buttonsPreset.Buttons[button.Type];
                button.UpdateData(new ActionButtonData(presetData, false, button.Type));
            }
        }

        public void OnButtonPressed(PanelActionButton button)
        {
            var type = button.Data.Type;
            switch (type)
            {
                
            }
        }

        public void Dispose()
        {
            
        }
    }
}