using DefaultNamespace.Services.Windows;
using JetBrains.Annotations;
using SimpleEventBus;
using Transponder.Events;
using UnityEngine;

namespace TheoryWindow
{
    [UsedImplicitly] //DI registered
    public class TheoryWindowService : TheoryWindow.IEventReceiver, ITheoryWindowService
    {
        private readonly IWindowFactory _windowFactory;
        
        private TheoryWindow _window;

        public TheoryWindowService(IWindowFactory windowFactory) => 
            _windowFactory = windowFactory;

        public void ShowWindow()
        {
            _window ??= _windowFactory.CreateWindow<TheoryWindow>(WindowType.TheoryWindow);
            
            if (_window is null)
            {
                Debug.LogError($"{nameof(_window)} is null");
                return;
            }
            
            _window.Initialize(this);
            _window.Show();
        }

        public void OnCloseButtonClicked()
        {
            _window.Close();
            EventStreams.Game.Publish(new OnBackButtonClickedEvent());
        }

        public void Dispose() { }
    }
}