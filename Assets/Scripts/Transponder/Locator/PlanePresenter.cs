using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using SimpleEventBus;
using Transponder.Events;
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
        private readonly UILineDrawer _uiLineDrawer;

        private readonly IReadOnlyList<Vector3> _pathPoints;
        private readonly PlaneConfigData _configData;

        private TweenerCore<Vector3, Path, PathOptions> _viewTween;

        private Vector3 _hintOffset;
        private IEventReceiver _eventReceiver;
        private string _currentResponderCode;
        private bool _isSelected;

        private bool _isHintDragging;
        private bool _isShow;

        private CancellationTokenSource _cts;

        public PlanePresenter(
            PlaneView planeView,
            PlaneHint planeHint,
            UILineDrawer uiLineDrawer,
            Vector3 hintOffset,
            PlaneConfigData configData)
        {
            _planeView = planeView;
            _planeHint = planeHint;
            _uiLineDrawer = uiLineDrawer;
            _hintOffset = hintOffset;
            _configData = configData;
            
            _isShow = true;
            
            _pathPoints = configData.PathPoints;
            _currentResponderCode = configData.DefaultResponderCode;
            _planeHint.Initialize(configData.DefaultResponderCode, configData.DispatcherCode, _configData.DispatcherComment, this);
        }
        
        public void Initialize(IEventReceiver eventReceiver) => 
            _eventReceiver = eventReceiver;
        
        public void SetSelected(bool isSelected)
        {
            _isSelected = isSelected;
            _planeHint.SetState(false, isSelected);
            _planeView.SetState(false, isSelected);
            _uiLineDrawer.SetState(false, isSelected);
        }
        
        public void SetInteractable(bool isInteractable) => 
            _planeHint.SetInteractable(isInteractable);

        public string GetResponderCode() => 
            _currentResponderCode;
        
        public void SetResponderCode(string responderCode)
        {
            _planeHint.SetResponderCode(responderCode);
            _currentResponderCode = responderCode;
        }

        public void OnHintClicked()
        {
            EventStreams.Game.Publish(new OnHintClickedEvent(_currentResponderCode));
            _eventReceiver?.OnHintClicked(this);
        }

        public void OnHintStartDragging() => 
            _isHintDragging = true;

        public void OnHintReleased(Vector3 newHintPosition)
        {
            _hintOffset = newHintPosition - _planeView.transform.position;
            _isHintDragging = false;
        }

        public void StartMove()
        {
            _planeView.transform.position = _pathPoints[0];
            _planeHint.transform.position = _pathPoints[0] + _hintOffset;
            
            _viewTween.Kill();

            _viewTween = _planeView.transform
                .DOPath(_pathPoints.ToArray(), _configData.Duration, _configData.PathType, _configData.PathMode)
                .SetLookAt(0.01f)
                .SetEase(_configData.Ease)
                .OnUpdate(OnDOTWeenUpdate)
                .SetRelative(false)
                .OnComplete(RestartMove);

            _viewTween.Restart();
        }

        public void SetPlaneAndHintState(bool isActive)
        {
            _planeView.PlaneContainer.SetActive(isActive);
            _planeHint.SetStateHint(isActive);
        }

        public async UniTask ActivateIDENT()
        {
            _planeView.SetState(true, _isSelected);
            _planeHint.SetState(true, _isSelected);
            _uiLineDrawer.SetState(true, _isSelected);
            
            await UniTask.Delay(IDENT_TIME * 1000);
            
            _planeView.SetState(false, _isSelected);
            _planeHint.SetState(false, _isSelected);
            _uiLineDrawer.SetState(false, _isSelected);
        }

        public void ChangeHeight(bool isShow)
        {
            _isShow = isShow;
            SetHeightTitle();
        }

        private async void RestartMove()
        {
            ResetToken();
            _cts = new CancellationTokenSource();
            
            EnablePlaneObjects(false);
            await UniTask.Delay(_configData.RestartTime * 1000, cancellationToken: _cts.Token);
            EnablePlaneObjects(true);
            
            StartMove();
        }

        private void EnablePlaneObjects(bool isEnable)
        {
            _planeHint.gameObject.SetActive(isEnable);
            _planeView.gameObject.SetActive(isEnable);
        }

        private void OnDOTWeenUpdate()
        {
            UpdateHintPosition();
            SetHeightTitle();
        }

        private void UpdateHintPosition()
        {
            if (_isHintDragging)
                return;

            _planeHint.transform.position = _planeView.transform.position + _hintOffset;
        }

        private void SetHeightTitle() => 
            _planeHint.SetHeightTitle(_isShow ? GetCurrentHeight() : "???");

        private string GetCurrentHeight()
        {
            var progress = _viewTween.ElapsedPercentage();
            var result = 0;

            foreach (var height in _configData.PathHeight.Where(height => progress >= height.Key))
                result = height.Value;
            
            return $"AF00{result}";
        }

        private void ResetToken()
        {
            if (_cts is null)
                return;

            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }

        public void Dispose()
        {
            ResetToken();
            _viewTween.Kill();

            if (_planeView)
                Object.Destroy(_planeView.gameObject);
            
            if (_planeHint)
                Object.Destroy(_planeHint.gameObject);
        }
    }
}