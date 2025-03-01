using UnityEngine;
using UnityEngine.UI;

namespace Transponder.Locator
{
    public class PlaneView : MonoBehaviour
    {
        [SerializeField] private Image _planeImage;
        [SerializeField] private Image _arrowImage;
        
        [SerializeField] private Color _identColor;
        [SerializeField] private Color _defaultColor;
        
        [field: SerializeField] public GameObject PlaneContainer { get; private set; }

        public void SetIDENTState(bool isActive) => 
            _planeImage.color = isActive ? _identColor : _defaultColor;
    }
}