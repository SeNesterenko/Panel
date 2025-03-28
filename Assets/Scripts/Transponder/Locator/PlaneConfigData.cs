using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
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
        [field: SerializeField] public string DispatcherComment { get; private set; }
        [field: SerializeField] public SerializedDictionary<float, int> PathHeight { get; private set; }
        [field: SerializeField] public SerializedDictionary<float, int> PathSpeed { get; private set; }
        [field: SerializeField] public int RestartTime { get; private set; }
        [field: SerializeField] public int MaxHeight { get; private set; }
        [field: SerializeField] public float UpdatePositionTime { get; private set; }
    }
}