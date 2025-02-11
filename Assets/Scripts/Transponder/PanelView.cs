using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Transponder
{
    public class PanelView : MonoBehaviour
    {
        [field: SerializeField] public List<PanelActionButton> Buttons { get; private set; }
        [field: SerializeField] public TextMeshProUGUI InputDisplay { get; private set; }
    }
}