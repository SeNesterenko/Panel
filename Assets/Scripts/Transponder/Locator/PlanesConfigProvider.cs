using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Transponder.Locator
{
    [UsedImplicitly] //Register in DI Container
    public class PlanesConfigProvider : IPlanesConfigProvider
    {
        private const string PLANES_CONFIG_PATH = "Configs/PlanesConfig";
        
        private readonly PlanesConfig _planesConfig;
        
        public IReadOnlyList<PlaneConfigData> PlanesConfig => _planesConfig.Planes;
        public Vector3 HintOffset => _planesConfig.HintOffset;

        public PlanesConfigProvider() => 
            _planesConfig = Resources.Load<PlanesConfig>(PLANES_CONFIG_PATH);
    }
}