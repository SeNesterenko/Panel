using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Transponder.Locator
{
    [Serializable]
    public class PlaneConfigData
    {
        public List<Vector3> PathPoints;
        
        [field: SerializeField] public bool IsLooping { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public PathType PathType { get; private set; }
        [field: SerializeField] public PathMode PathMode { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; }
        [field: SerializeField] public string DispatcherCode { get; private set; }
        [field: SerializeField] public string DefaultResponderCode { get; private set; }
    }
}