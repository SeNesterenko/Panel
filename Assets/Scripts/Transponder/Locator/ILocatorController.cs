using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using DefaultNamespace.Services;
using UnityEngine;

namespace Transponder.Locator
{
    public interface ILocatorController : IService
    {
        public void Initialize(Transform planeContainer, SerializedDictionary<int, List<Transform>> pathPoints);
    }
}