using DefaultNamespace.Services;

namespace Transponder
{
    public interface IPanelController : IService, PanelActionButton.IEventReceiver
    {
        public void Initialize(PanelView container);
    }
}