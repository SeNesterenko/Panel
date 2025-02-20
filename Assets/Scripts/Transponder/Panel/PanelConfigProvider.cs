using JetBrains.Annotations;
using Transponder.Panel.Buttons;
using UnityEngine;

namespace Transponder.Panel
{
    [UsedImplicitly] //Register in DI Container
    public class PanelConfigProvider : IPanelConfigProvider
    {
        private const string BUTTONS_PRESET_CONFIG_PATH = "Configs/ButtonsPreset";
        private const string BUTTONS_SETUP_CONFIG_PATH = "Configs/ButtonsSetup";
        
        public ButtonsPreset ButtonsPreset { get; private set; }
        public ButtonsSetup ButtonsSetup { get; private set; }

        public PanelConfigProvider()
        {
            ButtonsPreset = Resources.Load<ButtonsPreset>(BUTTONS_PRESET_CONFIG_PATH);
            ButtonsSetup = Resources.Load<ButtonsSetup>(BUTTONS_SETUP_CONFIG_PATH);
        }

        public void Dispose()
        {
            ButtonsPreset = null;
            ButtonsSetup = null;
        }
    }
}