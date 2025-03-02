using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using SimpleEventBus;
using SimpleEventBus.Disposables;
using Transponder.Events;
using Transponder.Panel.Buttons;
using Transponder.Panel.Buttons.Presenters;
using UnityEngine;

namespace Transponder.Panel
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
            public void SetButtonsLockState(bool isInteractable);
            public void SetPreviousCodeTitle();
        }
        
        private readonly IPanelButtonsFactory _factory;
        private readonly ButtonsPreset _buttonsPreset;
        private readonly ButtonsSetup _buttonsSetup;
        private readonly CompositeDisposable _subscriptions;
        
        private List<BasePanelButtonPresenter> _buttons;
        private PanelState _currentState;
        private TransmissionMode _currentMode;
        private PanelView _panelView;

        private CancellationTokenSource _cts;
        private bool _isRIndicationActive;
        
        private string _previousCode;

        public PanelController(IPanelButtonsFactory factory, IPanelConfigProvider configProvider)
        {
            _factory = factory;
            
            _subscriptions = new CompositeDisposable(EventStreams.Game.Subscribe<OnHintClickedEvent>(OnHintClicked));
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
            if (_currentState == state && state is PanelState.Default)
                return;
            
            if (_currentState == state)
            {
                Debug.LogError($"{state} has already been selected!!!");
                return;
            }
            
            _currentState = state;
            
            if (_currentState is PanelState.CODE)
                _previousCode = _panelView.CodeTitle.text;
            
            _buttons = _factory.UpdateButtonsState(_buttons, _currentState, _panelView, this);
        }

        public PanelState GetCurrentState() => 
            _currentState;

        public void ResetInformationSettings()
        {
            _panelView.ResetInformationSettings(_currentMode is TransmissionMode.STBY);
            SetPreviousCodeTitle();

            if (_currentMode is TransmissionMode.STBY)
                StopRIndication();
            else
            {
                if (_isRIndicationActive) 
                    return;
                
                ResetCancellationToken();
                _cts = new CancellationTokenSource();
                StartRIndication(_cts.Token).Forget();
            }
        }

        public void UpdateCurrentMode(TransmissionMode mode) => 
            _currentMode = mode;

        public TransmissionMode GetCurrentMode() => 
            _currentMode;

        public void SetButtonsLockState(bool isInteractable)
        {
            foreach (var button in _buttons) 
                button.SetInteractable(isInteractable);
        }

        public void SetPreviousCodeTitle()
        {
            if (_currentState is PanelState.CODE)
                _panelView.SetCodeTitle(_previousCode);
        }

        private void OnHintClicked(OnHintClickedEvent eventData) => 
            _panelView.SetCodeTitle(eventData.ResponderCode);

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
            _panelView.RIndication.SetActive(false);
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
            _subscriptions?.Dispose();
        }
    }
}