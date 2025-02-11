using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PanelActionButton : MonoBehaviour
    {
        public interface IEventReceiver
        {
            public void OnButtonPressed(ActionButtonType type);
        }
    
        [field: SerializeField] public ActionButtonType Type { get; private set; }
        [SerializeField] private Button _button;

        public void Initialize(IEventReceiver eventReceiver) => 
            _button.onClick.AddListener(() => eventReceiver.OnButtonPressed(Type));
    }
}