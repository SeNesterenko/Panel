using DefaultNamespace.Services;

namespace Transponder.Locator
{
    public interface ILocatorController : IService
    {
        public void Initialize();
    }
}