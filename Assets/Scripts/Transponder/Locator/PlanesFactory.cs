using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Transponder.Locator
{
    [UsedImplicitly] //Register in DI Container
    public class PlanesFactory : IPlanesFactory
    {
        private const string PLANE_VIEW_PREFAB_PATH = "Prefabs/Plane";
        private const string PLANE_HINT_PREFAB_PATH = "Prefabs/PlaneHint";
        
        private readonly IPlanesConfigProvider _configProvider;
        
        private PlaneView _planeViewPrefab;
        private PlaneHint _planeHintPrefab;

        public PlanesFactory(IPlanesConfigProvider configProvider) => 
            _configProvider = configProvider;

        public List<PlanePresenter> CreatePlanes(Transform root)
        {
            var result = new List<PlanePresenter>();
            
            foreach (var data in _configProvider.PlanesConfig)
            {
                var planeView = Object.Instantiate(GetPlanePrefab(), root);
                var planeHint = Object.Instantiate(GetPlaneHintPrefab(), root);

                var planePresenter = new PlanePresenter(planeView, planeHint, _configProvider.HintOffset, data);
                result.Add(planePresenter);
            }

            return result;
        }
        
        private PlaneView GetPlanePrefab() =>
            _planeViewPrefab ? _planeViewPrefab : _planeViewPrefab = Resources.Load<PlaneView>(PLANE_VIEW_PREFAB_PATH);
        
        private PlaneHint GetPlaneHintPrefab() =>
            _planeHintPrefab ? _planeHintPrefab : _planeHintPrefab = Resources.Load<PlaneHint>(PLANE_HINT_PREFAB_PATH);
    }
}