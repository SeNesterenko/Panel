using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Services.Windows;
using UnityEngine;

namespace DefaultNamespace.Services.Windows
{
    [CreateAssetMenu(menuName = "Configs/Window", fileName = "Window")]
    public class WindowsConfig : ScriptableObject
    {
        [field: SerializeField] private SerializedDictionary<WindowType, BaseWindow> _windows;
        public IReadOnlyDictionary<WindowType, BaseWindow> Windows => _windows;
    }
}