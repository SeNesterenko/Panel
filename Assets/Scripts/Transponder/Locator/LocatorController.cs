using JetBrains.Annotations;
using UnityEngine;

namespace Transponder.Locator
{
    [UsedImplicitly] //Register in DI Container
    public class LocatorController : ILocatorController
    {
        private readonly PlanesConfigProvider _planesConfigProvider;
        
        public void Initialize(Transform planeContainer)
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}