using System.Collections.Generic;
using UnityEngine;

namespace Transponder.Locator
{
    [CreateAssetMenu(menuName = "Configs/PlanesConfig", fileName = "PlanesConfig")]
    public class PlanesConfig : ScriptableObject
    {
        [SerializeField] private List<PlaneConfigData> _planes;
        
        public List<PlaneConfigData> Planes => _planes;
    }
}