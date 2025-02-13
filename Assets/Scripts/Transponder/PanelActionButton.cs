using UnityEngine;
using UnityEngine.UI;

namespace Transponder
{
    public class PanelActionButton : MonoBehaviour
    {
        public interface IEventReceiver
        {
            public void OnButtonPressed(PanelActionButton button);
        }

        [SerializeField] private Button _button;
        [SerializeField] private PanelActionButtonDisplay _display;

        [field: SerializeField] public ActionButtonType Type { get; private set; }

        public ActionButtonData Data { get; private set; }
        
        public void Initialize(IEventReceiver eventReceiver) => 
            _button.onClick.AddListener(() => eventReceiver.OnButtonPressed(this));

        public void UpdateData(ActionButtonData data)
        {
            Data = data;
            _display.SetState(data.PresetData, data.IsHighlighted);
        }
    }
}