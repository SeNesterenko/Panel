using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Transponder.Locator
{
    public interface IPlanesFactory
    {
        public UniTask<List<PlanePresenter>> CreatePlanes(Transform root, SerializedDictionary<int, List<Transform>> pathPoints);
    }
}