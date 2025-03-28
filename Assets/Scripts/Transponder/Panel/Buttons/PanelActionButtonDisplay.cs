using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Transponder.Panel.Buttons
{
    public class PanelActionButtonDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _background;

        public void SetState(ButtonPresetData presetData, bool isHighlighted)
        {
            var (textColor, backgroundColor) = presetData.GetColorSettings(isHighlighted);
            
            _text.color = textColor;
            _background.color = backgroundColor;
            
            _text.text = presetData.Name;
        }
        
        public void ChangeDisplayText(string text) => 
            _text.text = text;
    }
}