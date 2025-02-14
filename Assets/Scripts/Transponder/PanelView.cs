using TMPro;
using UnityEngine;

namespace Transponder
{
    public class PanelView : MonoBehaviour
    {
        [field: SerializeField] public Transform Container { get; private set; }
        [field: SerializeField] public TextMeshProUGUI InputDisplay { get; private set; }
    }
}