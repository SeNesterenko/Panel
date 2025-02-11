using AYellowpaper.SerializedCollections;
using Services.Windows;
using UnityEngine;

namespace DefaultNamespace.Services.Windows
{
    [CreateAssetMenu(menuName = "Configs/Window", fileName = "Window")]
    public class WindowsConfig : ScriptableObject
    {
        [SerializeField] private SerializedDictionary<WindowType, BaseWindow> _windows;
        
        public SerializedDictionary<WindowType, BaseWindow> Windows => _windows;
    }
}