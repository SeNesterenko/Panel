using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Transponder.Panel.Buttons
{
    [CreateAssetMenu(menuName = "Configs/ButtonsPreset", fileName = "ButtonsPreset")]
    public class ButtonsPreset : ScriptableObject
    {
        [SerializeField] private SerializedDictionary<ActionButtonType, ButtonPresetData> _buttons;
        
        public IReadOnlyDictionary<ActionButtonType, ButtonPresetData> Buttons => _buttons;
    }
}