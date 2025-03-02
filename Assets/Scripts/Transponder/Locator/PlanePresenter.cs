using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Transponder.Locator
{
    public class PlanePresenter : PlaneHint.IEventReceiver
    {
        public interface IEventReceiver
        {
            public void OnHintClicked(PlanePresenter presenter);
        }
        
        private const int IDENT_TIME = 10;
        
        private readonly PlaneView _planeView;
        private readonly PlaneHint _planeHint;
        
        private readonly Vector3 _hintOffset;
        private readonly IReadOnlyList<Vector3> _pathPoints;
        private readonly PlaneConfigData _configData;

        private TweenerCore<Vector3, Path, PathOptions> _viewTween;
        private TweenerCore<Vector3, Path, PathOptions> _hintTween;
        
        private IEventReceiver _eventReceiver;
        private bool _isIDENTActive;

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
            _planeHint.Initialize(configData.DefaultResponderCode, configData.DispatcherCode, _configData.DispatcherComment, this);
        }
        
        public void Initialize(IEventReceiver eventReceiver) => 
            _eventReceiver = eventReceiver;

        public void OnHintClicked() => 
            _eventReceiver?.OnHintClicked(this);

        public void StartMove()
        {
            _planeView.transform.position = _pathPoints[0];
            _planeHint.transform.position = _pathPoints[0] + _hintOffset;

            _viewTween = _planeView.transform
                .DOPath(_pathPoints.ToArray(), _configData.Duration, _configData.PathType, _configData.PathMode)
                .SetLookAt(0.01f)
                .SetEase(_configData.Ease)
                .SetRelative(false)
                .OnComplete(StartMove);
            
            _hintTween = _planeHint.transform
                .DOPath(_pathPoints.Select(p => p + _hintOffset).ToArray(), _configData.Duration, _configData.PathType, _configData.PathMode)
                .SetEase(_configData.Ease)
                .SetRelative(false)
                .OnComplete(StartMove);
            
            _viewTween.Restart();
            _hintTween.Restart();
        }

        public void SetPlaneAndHintState(bool isActive)
        {
            _planeView.PlaneContainer.SetActive(isActive);
            _planeHint.SetStateHint(isActive);
        }

        public async void ActivateIDENT()
        {
            _isIDENTActive = true;
            _planeView.SetIDENTState(true);
            _planeHint.SetIDENTState(true);
            
            await UniTask.Delay(IDENT_TIME * 1000);
            
            _planeView.SetIDENTState(false);
            _planeHint.SetIDENTState(false);
        }

        public void ChangeHeight(bool isShow) => 
            _planeHint.SetHeightTitle(isShow ? GetCurrentHeight() : "???");

        private string GetCurrentHeight() => 
            "AF0015";

        public void Dispose()
        {
            _viewTween.Kill();
            _hintTween.Kill();
            
            if (_planeView)
                Object.Destroy(_planeView.gameObject);
            
            if (_planeHint)
                Object.Destroy(_planeHint.gameObject);
        }
    }
}