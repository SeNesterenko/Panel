using DefaultNamespace.Services;

namespace Transponder.Panel
{
    public interface IPanelController : IService
    {
        public void Initialize(PanelView container);
    }
}