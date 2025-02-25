using Services.Windows;
using Transponder.Panel;
using UnityEngine;

namespace Transponder
{
    public class TransponderWindow : BaseWindow
    {
        [field: SerializeField] public PanelView PanelView { get; private set; }
        [field: SerializeField] public Transform PlaneContainer { get; private set; }
    }
}