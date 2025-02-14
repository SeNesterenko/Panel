using DefaultNamespace.Services;

namespace Transponder
{
    public interface IPanelConfigProvider : IService
    {
        public ButtonsPreset ButtonsPreset { get; }
        public ButtonsSetup ButtonsSetup { get; }
    }
}