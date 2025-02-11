using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PanelContainerView : MonoBehaviour
    {
        [field: SerializeField] public List<PanelActionButton> _buttons;
    }

    public class PanelContainerPresenter
    {
        private PanelContainerView _view;
    }
}