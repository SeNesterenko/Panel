using DefaultNamespace.Services.Windows;
using JetBrains.Annotations;
using SimpleEventBus;
using SimpleEventBus.Disposables;
using TheoryWindow;
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
        private readonly ITheoryWindowService _theoryWindowService;
        private readonly CompositeDisposable _subscriptions;
        
        private MenuWindow _window;

        public MenuWindowService(
            ITransponderService transponderService,
            IWindowFactory windowFactory,
            ITheoryWindowService theoryWindowService)
        {
            _transponderService = transponderService;
            _windowFactory = windowFactory;
            _theoryWindowService = theoryWindowService;

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
            _theoryWindowService.ShowWindow();

        public void OnExitButtonClicked() => 
            Application.Quit();

        public void Dispose() => 
            _subscriptions?.Dispose();
    }
}