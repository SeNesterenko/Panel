using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Transponder.Locator
{
    public class PlanePresenter
    {
        private readonly PlaneView _planeView;
        private readonly PlaneHint _planeHint;
        
        private readonly Vector3 _hintOffset;
        private readonly IReadOnlyList<Vector3> _pathPoints;
        private readonly PlaneConfigData _configData;

        public PlanePresenter(
            PlaneView planeView,
            PlaneHint planeHint,
            Vector3 hintOffset,
            PlaneConfigData configData)
        {
            _planeView = planeView;
            _planeHint = planeHint;
            _hintOffset = hintOffset;
            _configData = configData;
            
            _pathPoints = configData.PathPoints;
        }

        public void StartMove()
        {
            _planeView.transform.position = _pathPoints[0];
            _planeHint.transform.position = _pathPoints[0] + _hintOffset;
            
            Move();
        }
        
        private void Move()
        {
            _planeView.transform
                .DOPath(_pathPoints.ToArray(), _configData.Duration, _configData.PathType, _configData.PathMode)
                .SetLookAt(0.01f)
                .SetEase(_configData.Ease);
        }


        public void Dispose()
        {
            Object.Destroy(_planeView.gameObject);
            Object.Destroy(_planeHint.gameObject);
        }
    }
}