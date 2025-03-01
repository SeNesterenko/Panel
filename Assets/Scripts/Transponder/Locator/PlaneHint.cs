using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Transponder.Locator
{
    public class PlaneHint : MonoBehaviour
    {
        public interface IEventReceiver
        {
            public void OnHintClicked();
        }
        
        [SerializeField] private TextMeshProUGUI _responderCodeTitle;
        [SerializeField] private TextMeshProUGUI _dispatcherCodeTitle;
        [SerializeField] private TextMeshProUGUI _heightTitle;
        [SerializeField] private TextMeshProUGUI _unknownTitle;
        [SerializeField] private TextMeshProUGUI _speedTitle;
        [SerializeField] private TextMeshProUGUI _azimuthTitle;
        [SerializeField] private TextMeshProUGUI _rangeTitle;

        [SerializeField] private Button _button;

        [SerializeField] private GameObject _hintContainer;
        [SerializeField] private Image _backgroundImage;
        
        [SerializeField] private Color _identResponderColor;

        [SerializeField] private Color _identColor;
        [SerializeField] private Color _defaultColor;

        public void Initialize(string responderCode, string dispatcherCode, IEventReceiver eventReceiver)
        {
            _responderCodeTitle.text = responderCode;
            _dispatcherCodeTitle.text = dispatcherCode;
            
            _button.onClick.AddListener(eventReceiver.OnHintClicked);
        }

        public void SetStateHint(bool isActive)
        {
            _hintContainer.SetActive(isActive);
            _backgroundImage.enabled = isActive;
        }

        public void SetHeightTitle(string heightText) => 
            _heightTitle.text = heightText;

        public void SetIDENTState(bool isActive)
        {
            _responderCodeTitle.color = isActive ? _identResponderColor : _defaultColor;
            _dispatcherCodeTitle.color = isActive ? _identColor : _defaultColor;
            _heightTitle.color = isActive ? _identColor : _defaultColor;
            _unknownTitle.color = isActive ? _identColor : _defaultColor;
            _speedTitle.color = isActive ? _identColor : _defaultColor;
            _azimuthTitle.color = isActive ? _identColor : _defaultColor;
            _rangeTitle.color = isActive ? _identColor : _defaultColor;
        }
    }
}