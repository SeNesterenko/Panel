using Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class MenuWindow : BaseWindow
    {
        public interface IEventReceiver
        {
            public void OnPlayButtonClicked();
            public void OnTheoryButtonClicked();
            public void OnExitButtonClicked();
        }
        
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _theoryButton;
        [SerializeField] private Button _exitButton;

        public void Initialize(IEventReceiver eventReceiver)
        {
            _playButton.onClick.AddListener(eventReceiver.OnPlayButtonClicked);
            _theoryButton.onClick.AddListener(eventReceiver.OnTheoryButtonClicked);
            _exitButton.onClick.AddListener(eventReceiver.OnExitButtonClicked);
        }
    }
}