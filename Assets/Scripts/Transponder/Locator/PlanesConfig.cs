using System.Collections.Generic;
using UnityEngine;

namespace Transponder.Locator
{
    [CreateAssetMenu(menuName = "Configs/PlanesConfig", fileName = "PlanesConfig")]
    public class PlanesConfig : ScriptableObject
    {
        [SerializeField] private List<PlaneConfigData> _planes;

        [field: SerializeField] public Vector3 HintOffset { get; private set; }
        
        public List<PlaneConfigData> Planes => _planes;
    }
}