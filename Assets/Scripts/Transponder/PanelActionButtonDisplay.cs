using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Transponder
{
    public class PanelActionButtonDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _background;
        
        [Header("Color Settings")]
        [SerializeField] private Color _defaultTextColor;
        [SerializeField] private Color _highlightedTextColor;
        
        [SerializeField] private Color _defaultBackgroundColor;
        [SerializeField] private Color _highlightedBackgroundColor;
        
        public void SetState(string text, bool isHighlighted)
        {
            _text.color = isHighlighted ? _highlightedTextColor : _defaultTextColor;
            _background.color = isHighlighted ? _highlightedBackgroundColor : _defaultBackgroundColor;
            
            _text.text = text;
        }
    }
}