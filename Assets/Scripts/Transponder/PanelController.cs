using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

//ToDo: Grey OBS button
namespace Transponder
{
    [UsedImplicitly] //Register in DI Container
    public class PanelController : IPanelController
    {
        private readonly IPanelButtonsFactory _factory;
        private readonly IPanelConfigProvider _configProvider;
        private readonly ButtonsPreset _buttonsPreset;
        private readonly ButtonsSetup _buttonsSetup;
        
        private List<PanelActionButton> _buttons;
        private PanelState _currentState;
        private PanelView _container;

        private CancellationTokenSource _cts;
        private bool _isRIndicationActive;

        public PanelController(IPanelButtonsFactory factory, IPanelConfigProvider configProvider)
        {
            _factory = factory;
            _configProvider = configProvider;
        }
        
        public void Initialize(PanelView container)
        {
            _container = container;
            _currentState = PanelState.Default;
            
            _buttons = _factory.CreateButtons(_currentState, _container.Container, this);

            ResetCancellationToken();
            _cts = new CancellationTokenSource();
            
            StartRIndication(_cts.Token).Forget();
        }

        public async void OnButtonPressed(PanelActionButton button)
        {
            var type = button.Data.Type;
            switch (type)
            {
                case ActionButtonType.XPDR:
                    HandleXPDRButton();
                    break;
                
                case ActionButtonType.IDENT:
                    await HandleIdentButton();
                    break;
                
                case ActionButtonType.STBY:
                    HandleSTBYButton();
                    break;
                
                case ActionButtonType.ON:
                    HandleOnButton();
                    break;
                
                case ActionButtonType.ALT:
                    HandleALTButton();
                    break;
                
                case ActionButtonType.GND:
                    HandleGNDButton();
                    break;
                
                case ActionButtonType.ALERTS:
                case ActionButtonType.NRST:
                case ActionButtonType.TMR:
                case ActionButtonType.PFD:
                case ActionButtonType.INSET:
                case ActionButtonType.OBS:
                case ActionButtonType.CDI:
                case ActionButtonType.ADF:
                default:
                    return;
            }
        }

        private void HandleGNDButton()
        {
            ResetInformationSettingsToDefault();
            _container.ModeTitle.text = TransponderConstants.GND_TEXT;
            
            //ToDo: Send Locator Event (Hide)
        }

        private void HandleALTButton()
        {
            ResetInformationSettingsToDefault();
            _container.ModeTitle.text = TransponderConstants.ALT_TEXT;
            
            //ToDo: Send Locator Event (New SendData)
        }

        private void HandleOnButton()
        {
            ResetInformationSettingsToDefault();
            _container.ModeTitle.text = TransponderConstants.ON_TEXT;
            
            //ToDo: Send Locator Event (New SendData)
        }

        private void HandleSTBYButton()
        {
            if (_currentState is PanelState.STBY)
                Debug.LogError("STBY has already been selected!!!");
            
            _currentState = PanelState.STBY;
            
            StopRIndication();
            
            _container.ModeTitle.color = _container.STBYColor;
            _container.CodeTitle.color = _container.STBYColor;
            _container.ModeTitle.text = TransponderConstants.STBY_TEXT;
            
            //ToDo: Send Locator Event (Hide)
        }

        private void HandleXPDRButton()
        {
            if (_currentState is PanelState.XPDR)
                Debug.LogError("XPDR has already been selected!!!");

            _currentState = PanelState.XPDR;
            UpdateButtonsState(_currentState);
        }

        private async UniTask HandleIdentButton()
        {
            if (_currentState is PanelState.STBY)
                return;

            ResetInformationSettingsToDefault();
            
            var previousTitle = _container.ModeTitle.text;
            _container.ModeTitle.text = TransponderConstants.IDENT_TEXT;
            //ToDo: Send Locator Event (Highlight)

            await UniTask.Delay(TransponderConstants.IDENT_TIME * 1000);

            _container.ModeTitle.text = previousTitle;
        }

        private void ResetInformationSettingsToDefault()
        {
            if (!_isRIndicationActive)
            {
                ResetCancellationToken();
                _cts = new CancellationTokenSource();
                StartRIndication(_cts.Token).Forget();
            }

            _container.CodeTitle.color = _container.DefaultColor;
            _container.ModeTitle.color = _container.DefaultColor;
        }

        private async UniTask StartRIndication(CancellationToken ct)
        {
            _isRIndicationActive = true;
            
            while (!ct.IsCancellationRequested)
            {
                _container.RIndication.SetActive(!_container.RIndication.activeSelf);
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
            _container.RIndication.SetActive(true);
        }


        private void UpdateButtonsState(PanelState state)
        {
            if (_buttons.Count != _configProvider.ButtonsSetup.ButtonsOrderByState[state].Count)
            {
                Debug.LogError("Buttons count != buttons count from the setup!!!");
                return;
            }

            var buttonsTypes = _configProvider.ButtonsSetup.ButtonsOrderByState[state];
            
            for (var index = 0; index < _buttons.Count; index++)
            {
                var button = _buttons[index];
                var type = buttonsTypes[index];
                
                button.UpdateData(new ActionButtonData(_configProvider.ButtonsPreset.Buttons[type], false, type));
            }
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