using DefaultNamespace.Services.Windows;
using JetBrains.Annotations;
using SimpleEventBus;
using Transponder.Events;
using Transponder.Locator;
using Transponder.Panel;
using UnityEngine;

namespace Transponder
{
    [UsedImplicitly] //Register in DI Container
    public class TransponderService : ITransponderService, TransponderWindow.IEventReceiver
    {
        private readonly IWindowFactory _windowFactory;
        private readonly ILocatorController _locatorController;
        private readonly IPanelController _panelController;

        private TransponderWindow _window;

        public TransponderService(IWindowFactory windowFactory, 
            ILocatorController locatorController,
            IPanelController panelController)
        {
            _windowFactory = windowFactory;
            _locatorController = locatorController;
            _panelController = panelController;
        }

        public void ShowWindow()
        {
            _window ??= _windowFactory.CreateWindow<TransponderWindow>(WindowType.TransponderWindow);
            
            if (_window is null)
            {
                Debug.LogError($"{nameof(_window)} is null");
                return;
            }

            _window.Show();
            _window.SetEventReceiver(this);
            
            _panelController.Initialize(_window.PanelView);
            _locatorController.Initialize(_window.PlaneContainer, _window.PathPoints);
        }

        public void OnCloseButtonClicked()
        {
            Dispose();
            
            _window.Close();
            EventStreams.Game.Publish(new OnBackButtonClickedEvent());
        }

        public void Dispose()
        {
            _locatorController?.Dispose();
            _panelController?.Dispose();
        }
    }
}