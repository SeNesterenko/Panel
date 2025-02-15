using TMPro;
using UnityEngine;

namespace Transponder
{
    public class PanelView : MonoBehaviour
    {
        [field: SerializeField] public Transform Container { get; private set; }
        [field: SerializeField] public TextMeshProUGUI ModeTitle { get; private set; }
        [field: SerializeField] public TextMeshProUGUI CodeTitle { get; private set; }
        [field: SerializeField] public GameObject RIndication { get; private set; }
        [field: SerializeField] public Color DefaultColor { get; private set; }
        [field: SerializeField] public Color STBYColor { get; private set; }
    }
}