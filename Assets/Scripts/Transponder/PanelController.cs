using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Transponder.Buttons.Presenters;
using UnityEngine;

//ToDo: Grey OBS button
namespace Transponder
{
    [UsedImplicitly] //Register in DI Container
    public class PanelController : IPanelController, PanelController.IEventReceiver
    {
        public interface IEventReceiver
        {
            public void UpdateCurrentState(PanelState state);
            public PanelState GetCurrentState();
            public void ResetInformationSettings();
            public void UpdateCurrentMode(TransmissionMode mode);
            public TransmissionMode GetCurrentMode();
            public void SetButtonsLockState(bool isLock);
        }
        
        private readonly IPanelButtonsFactory _factory;
        private readonly IPanelConfigProvider _configProvider;
        private readonly ButtonsPreset _buttonsPreset;
        private readonly ButtonsSetup _buttonsSetup;
        
        private List<BasePanelButtonPresenter> _buttons;
        private PanelState _currentState;
        private TransmissionMode _currentMode;
        private PanelView _panelView;

        private CancellationTokenSource _cts;
        private bool _isRIndicationActive;

        public PanelController(IPanelButtonsFactory factory, IPanelConfigProvider configProvider)
        {
            _factory = factory;
            _configProvider = configProvider;
        }
        
        public void Initialize(PanelView container)
        {
            _panelView = container;
            _currentState = PanelState.Default;
            
            _buttons = _factory.CreateButtons(_currentState, _panelView, this);

            ResetCancellationToken();
            _cts = new CancellationTokenSource();
            
            StartRIndication(_cts.Token).Forget();
        }

        public void UpdateCurrentState(PanelState state)
        {
            if (_currentState == state)
            {
                Debug.LogError($"{state} has already been selected!!!");
                return;
            }
            
            _currentState = state;
            _buttons = _factory.UpdateButtonsState(_buttons, _currentState, _panelView, this);
        }

        public PanelState GetCurrentState() => 
            _currentState;

        public void ResetInformationSettings()
        {
            if (_currentMode is TransmissionMode.STBY)
            {
                _panelView.CodeTitle.color = _panelView.STBYColor;
                _panelView.ModeTitle.color = _panelView.STBYColor;
                
                StopRIndication();
            }
            else
            {
                if (!_isRIndicationActive)
                {
                    ResetCancellationToken();
                    _cts = new CancellationTokenSource();
                    StartRIndication(_cts.Token).Forget();
                }
            
                _panelView.CodeTitle.color = _panelView.DefaultColor;
                _panelView.ModeTitle.color = _panelView.DefaultColor;
            }
        }

        public void UpdateCurrentMode(TransmissionMode mode) => 
            _currentMode = mode;

        public TransmissionMode GetCurrentMode() => 
            _currentMode;

        public void SetButtonsLockState(bool isLock)
        {
            foreach (var button in _buttons) 
                button.SetInteractable(isLock);
        }

        private async UniTask StartRIndication(CancellationToken ct)
        {
            _isRIndicationActive = true;
            
            while (!ct.IsCancellationRequested)
            {
                _panelView.RIndication.SetActive(!_panelView.RIndication.activeSelf);
                try
                {
                    await UniTask.Delay(1000, cancellationToken: ct).SuppressCancellationThrow();
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }

        private void StopRIndication()
        {
            _isRIndicationActive = false;
            ResetCancellationToken();
            _panelView.RIndication.SetActive(true);
        }


        private void ResetCancellationToken()
        {
            if (_cts is null || _cts.IsCancellationRequested) 
                return;
            
            _cts.Cancel();
            _cts.Dispose();
        }

        public void Dispose()
        {
            ResetCancellationToken();
            
            _factory.Dispose();
            _buttons.Clear();
        }
    }
}