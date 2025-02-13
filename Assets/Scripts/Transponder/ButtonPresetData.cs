using System;
using UnityEngine;

namespace Transponder
{
    [Serializable]
    public class ButtonPresetData
    {
        [SerializeField] private string _name;
        
        [SerializeField] private Color _highlightedBackgroundColor;
        [SerializeField] private Color _defaultBackgroundColor;
        
        [SerializeField] private Color _highlightedTextColor;
        [SerializeField] private Color _defaultTextColor;
        
        public string Name => _name;

        public (Color, Color) GetColorSettings(bool isHighlighted)
        {
            return isHighlighted
                ? (_highlightedTextColor, _highlightedBackgroundColor)
                : (_defaultTextColor, _defaultBackgroundColor);
        }
    }
}