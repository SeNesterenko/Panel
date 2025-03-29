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
        private const string INVISIBLE_MOVEMENT_OBJECT_PREFAB_PATH = "Prefabs/InvisibleMovementObject";
        private const string PATH_POINT_PREFAB_PATH = "Prefabs/PathPoint";
        private const string UI_LINE_PREFAB_PATH = "Prefabs/UiLineDrawer";
        
        private readonly IPlanesConfigProvider _configProvider;

        private PlaneView _planeViewPrefab;
        private PlaneHint _planeHintPrefab;
        private PathPointObject _pathPointObjectPrefab;
        private UILineDrawer _uiLinePrefab;
        private InvisibleMovementObject _invisibleMovementObjectPrefab;

        public PlanesFactory(IPlanesConfigProvider configProvider) => 
            _configProvider = configProvider;

        public List<PlanePresenter> CreatePlanes(Transform root)
        {
            var result = new List<PlanePresenter>();
            
            foreach (var data in _configProvider.PlanesConfig)
            {
                var planeView = Object.Instantiate(GetPlanePrefab(), root);
                var planeHint = Object.Instantiate(GetPlaneHintPrefab(), root);
                var invisibleMovementObject = Object.Instantiate(GetInvisibleMovementObjectPrefab(), root);
                var uiLine = Object.Instantiate(GetUILinePrefab(), root);

                var planePresenter = new PlanePresenter(planeView, planeHint, uiLine, _configProvider.HintOffset, data, invisibleMovementObject);
                result.Add(planePresenter);

                var pathPointsObjects = new List<PathPointObject>();
                for (var i = 0; i < _configProvider.CountPathPointsObjects; i++)
                {
                    var pathPointObject = Object.Instantiate(GetPathPointPrefab(), root).GetComponent<PathPointObject>();
                    pathPointObject.gameObject.SetActive(false);
                    pathPointsObjects.Add(pathPointObject);
                }
                
                planeView.Initialize(pathPointsObjects);
                uiLine.Initialize(planeHint.LineRendererTarget, planeView.LineRendererTarget);
            }

            return result;
        }
        
        private PlaneView GetPlanePrefab() =>
            _planeViewPrefab ? _planeViewPrefab : _planeViewPrefab = Resources.Load<PlaneView>(PLANE_VIEW_PREFAB_PATH);
        
        private PlaneHint GetPlaneHintPrefab() =>
            _planeHintPrefab ? _planeHintPrefab : _planeHintPrefab = Resources.Load<PlaneHint>(PLANE_HINT_PREFAB_PATH);
        
        private PathPointObject GetPathPointPrefab() =>
            _pathPointObjectPrefab ? _pathPointObjectPrefab : _pathPointObjectPrefab = Resources.Load<PathPointObject>(PATH_POINT_PREFAB_PATH);
        
        private UILineDrawer GetUILinePrefab() =>
            _uiLinePrefab ? _uiLinePrefab : _uiLinePrefab = Resources.Load<UILineDrawer>(UI_LINE_PREFAB_PATH);
        
        private InvisibleMovementObject GetInvisibleMovementObjectPrefab() =>
            _invisibleMovementObjectPrefab ? _invisibleMovementObjectPrefab : _invisibleMovementObjectPrefab = Resources.Load<InvisibleMovementObject>(INVISIBLE_MOVEMENT_OBJECT_PREFAB_PATH);
    }
}