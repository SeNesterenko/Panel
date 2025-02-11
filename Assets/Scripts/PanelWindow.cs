using UnityEngine;

namespace DefaultNamespace
{
    public class PanelWindow : BaseWindow
    {
        [field: SerializeField] public PanelContainerView PanelContainerView { get; private set; }
    }
}