using DefaultNamespace.Services;
using UnityEngine;

namespace Transponder.Locator
{
    public interface ILocatorController : IService
    {
        public void Initialize(Transform planeContainer);
    }
}