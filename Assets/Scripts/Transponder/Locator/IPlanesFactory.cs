using System.Collections.Generic;
using UnityEngine;

namespace Transponder.Locator
{
    public interface IPlanesFactory
    {
        public List<PlanePresenter> CreatePlanes(Transform root);
    }
}