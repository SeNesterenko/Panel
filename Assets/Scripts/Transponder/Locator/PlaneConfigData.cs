using System;
using System.Collections.Generic;
using UnityEngine;

namespace Transponder.Locator
{
    [Serializable]
    public class PlaneConfigData
    {
        [SerializeField] private List<Vector3> _pathPoints;
        
        [field: SerializeField] public bool IsLooping { get; private set; }
        
        public IReadOnlyList<Vector3> PathPoints => _pathPoints;
    }
}