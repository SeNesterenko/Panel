using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Transponder.Locator
{
    [Serializable]
    public class PlaneConfigData
    {
        [field: SerializeField] private List<Vector3> _pathPoints;
        
        [field: SerializeField] public bool IsLooping { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public PathType PathType { get; private set; }
        [field: SerializeField] public PathMode PathMode { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; }

        public IReadOnlyList<Vector3> PathPoints => _pathPoints;
    }
}