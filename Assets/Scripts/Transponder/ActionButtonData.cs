namespace Transponder
{
    public class ActionButtonData
    {
        public string Text { get; }
        public bool IsHighlighted { get; }
        public ActionButtonType Type { get; }

        public ActionButtonData(string text, bool isHighlighted, ActionButtonType type)
        {
            Text = text;
            IsHighlighted = isHighlighted;
            Type = type;
        }
    }
}