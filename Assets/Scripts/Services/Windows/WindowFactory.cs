using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Services.Windows;
using UnityEngine;

namespace DefaultNamespace.Services.Windows
{
    [UsedImplicitly] //Register in DI Container
    public class WindowFactory : IWindowFactory
    {
        private const string WINDOWS_CONFIG_PATH = "Configs/WindowsConfig";

        private readonly IReadOnlyDictionary<WindowType, BaseWindow> _windows;
        private readonly Dictionary<WindowType, BaseWindow> _instantiatedWindows = new();
        private readonly Transform _windowRoot;

        public WindowFactory(Transform windowRoot)
        {
            _windowRoot = windowRoot;
            
            _windows = Resources
                .Load<WindowsConfig>(WINDOWS_CONFIG_PATH)
                .Windows;
        }
        
        public T CreateWindow<T>(WindowType windowType) where T : BaseWindow
        {
            if (!_windows.ContainsKey(windowType))
                Debug.LogError($"Window prefab not found in config {nameof(WindowsConfig)}");
            
            var window = Object.Instantiate(_windows[windowType], _windowRoot);
            _instantiatedWindows.Add(windowType, window);
            return window as T;
        }

        public void Dispose()
        {
            foreach (var window in _instantiatedWindows)
                if (window.Value)
                    Object.Destroy(window.Value.gameObject);

            _instantiatedWindows.Clear();
        }
    }
}