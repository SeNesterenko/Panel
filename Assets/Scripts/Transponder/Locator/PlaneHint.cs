using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Transponder.Locator
{
    public class PlaneHint : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public interface IEventReceiver
        {
            public void OnHintClicked();
            public void OnHintReleased(Vector3 position);
            public void OnHintStartDragging();
        }

        [SerializeField] private TextMeshProUGUI _responderCodeTitle;
        [SerializeField] private TextMeshProUGUI _dispatcherCodeTitle;
        [SerializeField] private TextMeshProUGUI _heightTitle;
        [SerializeField] private TextMeshProUGUI _unknownTitle;
        [SerializeField] private TextMeshProUGUI _speedTitle;
        [SerializeField] private TextMeshProUGUI _azimuthTitle;
        [SerializeField] private TextMeshProUGUI _rangeTitle;
        [SerializeField] private TextMeshProUGUI _dispatcherCommentTitle;

        [SerializeField] private Button _button;

        [SerializeField] private GameObject _hintContainer;
        [SerializeField] private Image _backgroundImage;
        
        [SerializeField] private Color _identResponderColor;

        [SerializeField] private Color _identColor;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _selectedColor;
        
        private IEventReceiver _eventReceiver;

        [field: SerializeField] public RectTransform LineRendererTarget { get; private set; }

        public void Initialize(string responderCode, string dispatcherCode, string dispatcherComment, IEventReceiver eventReceiver)
        {
            _eventReceiver = eventReceiver;
            _responderCodeTitle.text = responderCode;
            _dispatcherCodeTitle.text = dispatcherCode;
            
            _dispatcherCommentTitle.transform.parent.gameObject.SetActive(!string.IsNullOrEmpty(dispatcherComment));
            _dispatcherCommentTitle.text = dispatcherComment;

            _button.onClick.AddListener(eventReceiver.OnHintClicked);
        }

        public void SetStateHint(bool isActive)
        {
            _hintContainer.SetActive(isActive);
            _backgroundImage.enabled = isActive;
        }

        public void SetHeightTitle(string heightText) => 
            _heightTitle.text = heightText;
        
        public void SetSpeedTitle(string speedText) => 
            _speedTitle.text = speedText;

        public void SetState(bool isIDENT, bool isSelected)
        {
            _responderCodeTitle.color = isIDENT ? _identResponderColor : isSelected ? _selectedColor : _defaultColor;
            _dispatcherCodeTitle.color = isIDENT ? _identColor : _defaultColor;
            _heightTitle.color = isIDENT ? _identColor : _defaultColor;
            _unknownTitle.color = isIDENT ? _identColor : _defaultColor;
            _speedTitle.color = isIDENT ? _identColor : _defaultColor;
            _azimuthTitle.color = isIDENT ? _identColor : _defaultColor;
            _rangeTitle.color = isIDENT ? _identColor : _defaultColor;
        }

        public void SetResponderCode(string responderCode) => 
            _responderCodeTitle.text = responderCode;

        public void SetInteractable(bool isInteractable) => 
            _button.interactable = isInteractable;

        public void OnPointerDown(PointerEventData eventData) => 
            _eventReceiver.OnHintStartDragging();

        public void OnDrag(PointerEventData eventData) => 
            transform.position = new Vector3(eventData.position.x, eventData.position.y, 0);

        public void OnPointerUp(PointerEventData eventData) => 
            _eventReceiver.OnHintReleased(transform.position);
    }
}