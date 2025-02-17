using DefaultNamespace.Services;

namespace Transponder
{
    public interface IPanelController : IService
    {
        public void Initialize(PanelView container);
    }
}