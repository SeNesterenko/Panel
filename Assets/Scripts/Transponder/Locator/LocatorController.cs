using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Transponder.Locator
{
    [UsedImplicitly] //Register in DI Container
    public class LocatorController : ILocatorController
    {
        private readonly IPlanesFactory _planesFactory;

        private List<PlanePresenter> _presenters;

        public LocatorController(IPlanesFactory planesFactory) => 
            _planesFactory = planesFactory;

        public void Initialize(Transform planeContainer)
        {
            _presenters = _planesFactory.CreatePlanes(planeContainer);

            foreach (var planePresenter in _presenters) 
                planePresenter.StartMove();
        }

        public void Dispose()
        {
            foreach (var planePresenter in _presenters) 
                planePresenter.Dispose();
            
            _presenters.Clear();
        }
    }
}