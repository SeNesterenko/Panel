using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using SimpleEventBus;
using SimpleEventBus.Disposables;
using Transponder.Events;
using Transponder.Panel.Buttons;
using UnityEngine;

namespace Transponder.Locator
{
    [UsedImplicitly] //Register in DI Container
    public class LocatorController : ILocatorController, PlanePresenter.IEventReceiver
    {
        private readonly IPlanesFactory _planesFactory;
        private readonly CompositeDisposable _subscriptions;

        private List<PlanePresenter> _presenters;
        private PlanePresenter _currentPresenter;

        public LocatorController(IPlanesFactory planesFactory)
        {
            _planesFactory = planesFactory;
            _subscriptions = new CompositeDisposable(
                EventStreams.Game.Subscribe<OnButtonClickedEvent>(OnButtonClicked),
                EventStreams.Game.Subscribe<OnNewResponderCodeEnteredEvent>(OnNewResponderCodeEntered));
        }

        public void Initialize(Transform planeContainer)
        {
            _presenters = _planesFactory.CreatePlanes(planeContainer);

            foreach (var planePresenter in _presenters)
            {
                planePresenter.Initialize(this);
                planePresenter.StartMove();
            }
            
            _currentPresenter = _presenters.First();
            _currentPresenter.SetSelected(true);
            EventStreams.Game.Publish(new OnHintClickedEvent(_currentPresenter.GetResponderCode()));
        }

        public void OnHintClicked(PlanePresenter presenter)
        {
            _currentPresenter = presenter;
            _presenters.ForEach(p => p.SetSelected(false));
            _currentPresenter.SetSelected(true);
        }

        private void OnNewResponderCodeEntered(OnNewResponderCodeEnteredEvent eventData) => 
            _currentPresenter.SetResponderCode(eventData.ResponderCode);

        private async void OnButtonClicked(OnButtonClickedEvent eventData)
        {
            if (_currentPresenter is null)
            {
                Debug.LogError("Current presenter is null");
                return;
            }
            
            switch (eventData.ButtonType)
            {
                case ActionButtonType.IDENT:
                    _presenters.ForEach(p => p.SetInteractable(false));
                    await _currentPresenter.ActivateIDENT();
                    _presenters.ForEach(p => p.SetInteractable(true));
                    break;
                case ActionButtonType.GND:
                case ActionButtonType.STBY:
                    _currentPresenter.SetPlaneAndHintState(false);
                    break;
                case ActionButtonType.ON:
                    _currentPresenter.SetPlaneAndHintState(true);
                    _currentPresenter.ChangeHeight(false);
                    break;
                case ActionButtonType.ALT:
                    _currentPresenter.SetPlaneAndHintState(true);
                    _currentPresenter.ChangeHeight(true);
                    break;
                default:
                    return;
            }
        }

        public void Dispose()
        {
            _subscriptions?.Dispose();
            if (_presenters is null)
                return;
            
            foreach (var planePresenter in _presenters) 
                planePresenter.Dispose();
            
            _presenters.Clear();
        }
    }
}