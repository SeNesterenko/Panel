using Services.Windows;
using Transponder.Panel;
using UnityEngine;
using UnityEngine.UI;

namespace Transponder
{
    public class TransponderWindow : BaseWindow
    {
        public interface IEventReceiver
        {
            public void OnCloseButtonClicked();
        }
        
        [SerializeField] private Button _closeButton;
        
        [field: SerializeField] public PanelView PanelView { get; private set; }
        [field: SerializeField] public Transform PlaneContainer { get; private set; }
        
        public void SetEventReceiver(IEventReceiver eventReceiver) =>
            _closeButton.onClick.AddListener(eventReceiver.OnCloseButtonClicked);
    }
}