using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Transponder.Panel
{
    public class PanelView : MonoBehaviour
    {
        [field: SerializeField] public Transform Container { get; private set; }
        [field: SerializeField] public TextMeshProUGUI ModeTitle { get; private set; }
        [field: SerializeField] public TextMeshProUGUI CodeTitle { get; private set; }
        [field: SerializeField] public TextMeshProUGUI CodeInputTitle { get; private set; }
        [field: SerializeField] public Image InputBackground { get; private set; }
        [field: SerializeField] public GameObject RIndication { get; private set; }
        [field: SerializeField] public Color DefaultColor { get; private set; }
        [field: SerializeField] public Color STBYColor { get; private set; }

        public void ResetInformationSettings(bool isSTBY)
        {
            InputBackground.gameObject.SetActive(false);
            CodeTitle.gameObject.SetActive(true);
            
            CodeTitle.color = isSTBY ? STBYColor : DefaultColor;
            ModeTitle.color = isSTBY ? STBYColor : DefaultColor;
            InputBackground.color = isSTBY ? STBYColor : DefaultColor;
        }
    }
}