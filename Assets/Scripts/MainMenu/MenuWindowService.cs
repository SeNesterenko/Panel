using DefaultNamespace.Services.Windows;
using JetBrains.Annotations;
using SimpleEventBus;
using SimpleEventBus.Disposables;
using Transponder;
using Transponder.Events;
using UnityEngine;

namespace MainMenu
{
    [UsedImplicitly] //Register in DI Container
    public class MenuWindowService : IMenuWindowService, MenuWindow.IEventReceiver
    {
        private readonly ITransponderService _transponderService;
        private readonly IWindowFactory _windowFactory;
        private readonly CompositeDisposable _subscriptions;
        
        private MenuWindow _window;

        public MenuWindowService(ITransponderService transponderService, IWindowFactory windowFactory)
        {
            _transponderService = transponderService;
            _windowFactory = windowFactory;
            
            _subscriptions = new CompositeDisposable(EventStreams.Game.Subscribe<OnBackButtonClickedEvent>(OnShowEventReceived));
        }

        public void ShowWindow()
        {
            _window ??= _windowFactory.CreateWindow<MenuWindow>(WindowType.MenuWindow);
            
            if (_window is null)
            {
                Debug.LogError($"{nameof(_window)} is null");
                return;
            }
            
            _window.Initialize(this);
            _window.Show();
        }

        public void OnPlayButtonClicked()
        {
            _window.Close();
            _transponderService.ShowWindow();
        }

        private void OnShowEventReceived(OnBackButtonClickedEvent eventData) => 
            ShowWindow();

        public void OnTheoryButtonClicked() => 
            Debug.Log("Theory button clicked");

        public void OnExitButtonClicked() => 
            Application.Quit();

        public void Dispose() => 
            _subscriptions?.Dispose();
    }
}