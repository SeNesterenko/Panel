using System.Collections.Generic;
using UnityEngine;

namespace Transponder.Locator
{
    public interface IPlanesConfigProvider
    {
        public IReadOnlyList<PlaneConfigData> PlanesConfig { get; }
        public Vector3 HintOffset { get; }
        public int CountPathPointsObjects { get; }
    }
}