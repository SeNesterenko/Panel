namespace Transponder
{
    public class ActionButtonData
    {
        public ButtonPresetData PresetData { get; }
        public bool IsHighlighted { get; }
        public ActionButtonType Type { get; }

        public ActionButtonData(ButtonPresetData presetData, bool isHighlighted, ActionButtonType type)
        {
            PresetData = presetData;
            IsHighlighted = isHighlighted;
            Type = type;
        }
    }
}