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
        
        private readonly List<KeyValuePair<float, int>> _pathHeightList;

        private TweenerCore<Vector3, Path, PathOptions> _viewTween;

        private Vector3 _hintOffset;
        private IEventReceiver _eventReceiver;
        private string _currentResponderCode;
        private bool _isSelected;

        private bool _isHintDragging;
        private bool _isShow;

        private CancellationTokenSource _cts;
        private bool _isVFRActive;
        private int _previousHeight = -1;
        private int _lastArrowHeight = -1;

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
            _pathHeightList = _configData.PathHeight.ToList();
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
        
        public void SetVFRState(bool isActive)
        {
            _isVFRActive = isActive;
            _planeHint.SetResponderCode(isActive ? "1200" : _currentResponderCode);
        }

        public void OnHintClicked()
        {
            if (!_isVFRActive)
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
            _planeView.SetActive(isActive);
            _planeHint.SetStateHint(isActive);
            _uiLineDrawer.gameObject.SetActive(isActive);
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
            
            _previousHeight = -1;
            _lastArrowHeight = -1;
            
            StartMove();
        }

        private void EnablePlaneObjects(bool isEnable)
        {
            _planeHint.gameObject.SetActive(isEnable);
            _planeView.SetActive(isEnable);
            _uiLineDrawer.gameObject.SetActive(isEnable);
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

            foreach (var t in _pathHeightList.Where(t => progress >= t.Key)) 
                result = t.Value;

            if (result == _configData.MaxHeight)
                return $"AF00{result}";
            
            if (result == _previousHeight)
                return $"AF00{result}{(_lastArrowHeight == result ? "" : result > _lastArrowHeight ? "↑" : "↓")}";

            var arrow = "";
            if (_previousHeight != -1)
            {
                if (result > _previousHeight && result < _configData.MaxHeight)
                    arrow = "↑";
                else if (result < _previousHeight) 
                    arrow = "↓";
            }
            
            _lastArrowHeight = _previousHeight;
            _previousHeight = result;

            return $"AF00{result}{arrow}";
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
            {
                _planeView.Dispose();
                Object.Destroy(_planeView.gameObject);
            }
            
            if (_planeHint)
                Object.Destroy(_planeHint.gameObject);
            
            if (_uiLineDrawer)
                Object.Destroy(_uiLineDrawer.gameObject);
        }
    }
}