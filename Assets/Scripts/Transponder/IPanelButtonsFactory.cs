using System.Collections.Generic;
using DefaultNamespace.Services;
using UnityEngine;

namespace Transponder
{
    public interface IPanelButtonsFactory : IService
    {
        public List<PanelActionButton> CreateButtons(PanelState state, Transform root, PanelActionButton.IEventReceiver receiver);
    }
}