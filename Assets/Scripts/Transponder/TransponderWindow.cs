using Services.Windows;
using UnityEngine;

namespace Transponder
{
    public class TransponderWindow : BaseWindow
    {
        [field: SerializeField] public PanelView PanelView { get; private set; }
    }
}