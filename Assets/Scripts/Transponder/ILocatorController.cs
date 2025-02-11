using DefaultNamespace.Services;

namespace Transponder
{
    public interface ILocatorController : IService
    {
        public void Initialize();
    }
}