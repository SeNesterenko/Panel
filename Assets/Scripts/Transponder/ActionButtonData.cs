namespace Transponder
{
    public class ActionButtonData
    {
        public ButtonPresetData PresetData { get; }
        public bool IsHighlighted { get; }
        public int Index { get; }

        public ActionButtonData(ButtonPresetData presetData, bool isHighlighted, int index)
        {
            PresetData = presetData;
            IsHighlighted = isHighlighted;
            Index = index;
        }
    }
}