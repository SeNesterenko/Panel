using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Transponder.Locator
{
    [UsedImplicitly] //Register in DI Container
    public class PlanesConfigProvider : IPlanesConfigProvider
    {
        private const string PLANES_CONFIG_PATH = "Configs/PlanesConfig";
        
        public IReadOnlyList<PlaneConfigData> PlanesConfig { get; }

        public PlanesConfigProvider() => 
            PlanesConfig = Resources.Load<PlanesConfig>(PLANES_CONFIG_PATH).Planes;
    }
}