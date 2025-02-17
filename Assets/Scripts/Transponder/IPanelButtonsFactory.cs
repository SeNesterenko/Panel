using System.Collections.Generic;
using DefaultNamespace.Services;
using Transponder.Buttons.Presenters;

namespace Transponder
{
    public interface IPanelButtonsFactory : IService
    {
        public List<BasePanelButtonPresenter> CreateButtons(PanelState state, PanelView panelView,
            PanelController.IEventReceiver eventReceiver);
        public List<BasePanelButtonPresenter> UpdateButtonsState(List<BasePanelButtonPresenter> activeButtons,
            PanelState state, PanelView panelView, PanelController.IEventReceiver eventReceiver);
    }
}