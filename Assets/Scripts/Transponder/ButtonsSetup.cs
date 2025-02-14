using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Transponder
{
    [CreateAssetMenu(menuName = "Configs/ButtonsSetup", fileName = "ButtonsSetup")]
    public class ButtonsSetup : ScriptableObject
    {
        [SerializeField] private SerializedDictionary<PanelState, List<ActionButtonType>> _buttonsOrderByState;

        public IReadOnlyDictionary<PanelState, List<ActionButtonType>> ButtonsOrderByState => _buttonsOrderByState;
    }
}