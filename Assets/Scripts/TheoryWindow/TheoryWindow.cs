using Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace TheoryWindow
{
    public class TheoryWindow : BaseWindow
    {
        public interface IEventReceiver
        {
            public void OnCloseButtonClicked();
        }

        [SerializeField] private Button _closeButton;
        [SerializeField] private ScrollRect _scroll;

        public void Initialize(IEventReceiver eventReceiver)
        {
            _closeButton.onClick.AddListener(eventReceiver.OnCloseButtonClicked);
            _scroll.verticalNormalizedPosition = 1;
        }
    }
}