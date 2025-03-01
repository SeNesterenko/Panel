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
            _subscriptions = new CompositeDisposable(EventStreams.Game.Subscribe<OnButtonClickedEvent>(OnButtonClicked));
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
        }

        public void OnHintClicked(PlanePresenter presenter) => 
            _currentPresenter = presenter;

        private void OnButtonClicked(OnButtonClickedEvent eventData)
        {
            if (_currentPresenter is null)
            {
                Debug.LogError("Current presenter is null");
                return;
            }
            
            switch (eventData.ButtonType)
            {
                case ActionButtonType.IDENT:
                    _currentPresenter.ActivateIDENT();
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
            foreach (var planePresenter in _presenters) 
                planePresenter.Dispose();
            
            _presenters.Clear();
            _subscriptions?.Dispose();
        }
    }
}