using DefaultNamespace.Services;
using Transponder.Panel.Buttons;

namespace Transponder.Panel
{
    public interface IPanelConfigProvider : IService
    {
        public ButtonsPreset ButtonsPreset { get; }
        public ButtonsSetup ButtonsSetup { get; }
    }
}