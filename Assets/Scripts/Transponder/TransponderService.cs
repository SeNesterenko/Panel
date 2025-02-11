using DefaultNamespace.Services.Windows;
using UnityEngine;

namespace Transponder
{
    public class TransponderService
    {
        private readonly IWindowFactory _windowFactory;
        private readonly ILocatorController _locatorController;
        private readonly IPanelController _panelController;

        private TransponderWindow _window;

        public TransponderService(IWindowFactory windowFactory, ILocatorController locatorController, IPanelController panelController)
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

            _locatorController.Initialize();
            _panelController.Initialize(_window.PanelView);
        }
    }
}