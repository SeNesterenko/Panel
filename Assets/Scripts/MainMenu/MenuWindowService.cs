using DefaultNamespace.Services.Windows;
using JetBrains.Annotations;
using Transponder;
using UnityEngine;
using VContainer.Unity;

namespace MainMenu
{
    [UsedImplicitly] //Register in DI Container
    public class MenuWindowService : IStartable, MenuWindow.IEventReceiver
    {
        private readonly ITransponderService _transponderService;
        private readonly IWindowFactory _windowFactory;
        
        private MenuWindow _window;

        public MenuWindowService(ITransponderService transponderService, IWindowFactory windowFactory)
        {
            _transponderService = transponderService;
            _windowFactory = windowFactory;
        }

        public void Start() => 
            ShowWindow();

        private void ShowWindow()
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

        public void OnTheoryButtonClicked() => 
            Debug.Log("Theory button clicked");

        public void OnExitButtonClicked() => 
            Application.Quit();
    }
}