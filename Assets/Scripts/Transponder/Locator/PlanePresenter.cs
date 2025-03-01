using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
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

        private TweenerCore<Vector3, Path, PathOptions> _viewTween;
        private TweenerCore<Vector3, Path, PathOptions> _hintTween;

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

        public void StartMove() => 
            Move();

        private void Move()
        {
            _planeView.transform.position = _pathPoints[0];
            _planeHint.transform.position = _pathPoints[0] + _hintOffset;

            _viewTween = _planeView.transform
                .DOPath(_pathPoints.ToArray(), _configData.Duration, _configData.PathType, _configData.PathMode)
                .SetLookAt(0.01f)
                .SetEase(_configData.Ease)
                .SetRelative(false)
                .OnComplete(Move);
            
            _hintTween = _planeHint.transform
                .DOPath(_pathPoints.Select(p => p + _hintOffset).ToArray(), _configData.Duration, _configData.PathType, _configData.PathMode)
                .SetEase(_configData.Ease)
                .SetRelative(false)
                .OnComplete(Move);
            
            _viewTween.Restart();
            _hintTween.Restart();
        }


        public void Dispose()
        {
            _viewTween.Kill();
            _hintTween.Kill();
            
            Object.Destroy(_planeView.gameObject);
            Object.Destroy(_planeHint.gameObject);
        }
    }
}