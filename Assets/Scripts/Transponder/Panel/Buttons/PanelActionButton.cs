using UnityEngine;
using UnityEngine.UI;

namespace Transponder.Panel.Buttons
{
    public class PanelActionButton : MonoBehaviour
    {
        public interface IEventReceiver
        {
            public void OnButtonPressed();
        }

        [SerializeField] private Button _button;
        [SerializeField] private PanelActionButtonDisplay _display;

        public void SetEventReceiver(IEventReceiver eventReceiver) => 
            _button.onClick.AddListener(eventReceiver.OnButtonPressed);
        
        public void RemoveEventReceiver() => 
            _button.onClick.RemoveAllListeners();

        public void UpdateState(ActionButtonData data) => 
            _display.SetState(data.PresetData, data.IsHighlighted);

        public void SetInteractable(bool isInteractable) => 
            _button.interactable = isInteractable;
        
        public void ChangeDisplayText(string text) =>
            _display.ChangeDisplayText(text);
    }
}