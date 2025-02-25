using System.Collections.Generic;

namespace Transponder.Locator
{
    public interface IPlanesConfigProvider
    {
        public IReadOnlyList<PlaneConfigData> PlanesConfig { get; }
    }
}