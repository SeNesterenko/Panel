using System.Collections.Generic;
using UnityEngine;

namespace Transponder.Locator
{
    public class PlanePresenter
    {
        private readonly PlaneView _planeView;
        private readonly PlaneHint _planeHint;
        
        private readonly bool _isLooping;
        private readonly Vector3 _hintOffset;
        private readonly IReadOnlyList<Vector3> _pathPoints;

        public PlanePresenter(
            PlaneView planeView,
            PlaneHint planeHint,
            bool isLooping,
            Vector3 hintOffset,
            IReadOnlyList<Vector3> pathPoints)
        {
            _planeView = planeView;
            _planeHint = planeHint;
            _isLooping = isLooping;
            _hintOffset = hintOffset;
            _pathPoints = pathPoints;
        }

        public void StartMove()
        {
            _planeView.transform.position = _pathPoints[0];
            _planeHint.transform.position = _pathPoints[0] + _hintOffset;
        }
        
        public void Dispose()
        {
            Object.Destroy(_planeView.gameObject);
            Object.Destroy(_planeHint.gameObject);
        }
    }
}