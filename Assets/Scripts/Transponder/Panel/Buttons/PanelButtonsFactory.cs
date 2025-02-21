using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Transponder.Panel.Buttons.Presenters;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Transponder.Panel.Buttons
{
    [UsedImplicitly] //Register in DI Container
    public class PanelButtonsFactory : IPanelButtonsFactory
    {
        private const string BUTTON_PREFAB_PATH = "Prefabs/PanelActionButton";
        
        private readonly IPanelConfigProvider _configProvider;
        
        private PanelActionButton _buttonPrefab;
        private readonly List<PanelActionButton> _buttonsViews = new();
        private readonly Dictionary<ActionButtonType, List<BasePanelButtonPresenter>> _presentersPoolByType = new();

        public PanelButtonsFactory(IPanelConfigProvider configProvider) => 
            _configProvider = configProvider;

        public List<BasePanelButtonPresenter> CreateButtons(PanelState state, PanelView panelView,
            PanelController.IEventReceiver eventReceiver)
        {
            var presenters = new List<BasePanelButtonPresenter>();

            for (var index = 0; index < _configProvider.ButtonsSetup.ButtonsOrderByState[state].Count; index++)
            {
                var view = Object.Instantiate(GetPrefab(), panelView.Container);
                presenters.Add(TryCreatePresenter(state, panelView, eventReceiver, index, view));
                _buttonsViews.Add(view);
            }

            return presenters;
        }

        public List<BasePanelButtonPresenter> UpdateButtonsState(List<BasePanelButtonPresenter> activeButtons,
            PanelState state, PanelView panelView, PanelController.IEventReceiver eventReceiver)
        {
            AddPresentersToPool(activeButtons);
            var presenters = new List<BasePanelButtonPresenter>();

            for (var index = 0; index < _configProvider.ButtonsSetup.ButtonsOrderByState[state].Count; index++)
            {
                var view = _buttonsViews[index];
                var presenter = TryCreatePresenter(state, panelView, eventReceiver, index, view);
                presenters.Add(presenter);
            }

            return presenters;
        }

        private void AddPresentersToPool(List<BasePanelButtonPresenter> activeButtons)
        {
            foreach (var button in activeButtons) 
                _presentersPoolByType[button.Type].Add(button);
        }

        private BasePanelButtonPresenter TryCreatePresenter(PanelState state, PanelView panelView, PanelController.IEventReceiver eventReceiver, int index,
            PanelActionButton view)
        {
            var type = _configProvider.ButtonsSetup.ButtonsOrderByState[state][index];
            var presetData = _configProvider.ButtonsPreset.Buttons[type];
            var actionButtonData = new ActionButtonData(presetData, false, index);
            var presenter = GetPresenter(type, view, panelView, actionButtonData, eventReceiver);
            presenter.UpdateData(actionButtonData, view);

            return presenter;
        }

        private PanelActionButton GetPrefab() =>
            _buttonPrefab ? _buttonPrefab : _buttonPrefab = Resources.Load<PanelActionButton>(BUTTON_PREFAB_PATH);

        private BasePanelButtonPresenter GetPresenter(
            ActionButtonType type,
            PanelActionButton view,
            PanelView panelView,
            ActionButtonData data,
            PanelController.IEventReceiver receiver)
        {
            BasePanelButtonPresenter result;
            var isTypeExist = false;
            
            if (_presentersPoolByType.TryGetValue(type, out var presenters))
            {
                if (presenters.Count > 0)
                {
                    result = presenters.Last();
                    presenters.Remove(result);
                    return result;
                }

                isTypeExist = true;
            }
            
            switch (type)
            {
                case ActionButtonType.None:
                case ActionButtonType.INSET:
                case ActionButtonType.PFD:
                case ActionButtonType.OBS:
                case ActionButtonType.CDI:
                case ActionButtonType.ADF:
                case ActionButtonType.TMR:
                case ActionButtonType.NRST:
                case ActionButtonType.ALERTS:
                    result = new BasePanelButtonPresenter(view, panelView, type, receiver);
                    break;
                case ActionButtonType.XPDR:
                    result = new XPDRButtonPresenter(view, panelView, type, receiver);
                    break;
                case ActionButtonType.IDENT:
                    result = new IDENTButtonPresenter(view, panelView, type, receiver);
                    break;
                case ActionButtonType.STBY:
                    result = new STBYButtonPresenter(view, panelView, type, receiver);
                    break;
                case ActionButtonType.ON:
                    result = new ONButtonPresenter(data, view, panelView, type, receiver);
                    break;
                case ActionButtonType.ALT:
                    result = new ALTButtonPresenter(view, panelView, type, receiver);
                    break;
                case ActionButtonType.GND:
                    result = new GNDButtonPresenter(view, panelView, type, receiver);
                    break;
                case ActionButtonType.VFR:
                    result = new VFRButtonPresenter(view, panelView, type, receiver);
                    break;
                case ActionButtonType.CODE:
                    result = new CODEButtonPresenter(data, view, panelView, type, receiver);
                    break;
                case ActionButtonType.BACK:
                    result = new BACKButtonPresenter(view, panelView, type, receiver);
                    break;
                case ActionButtonType.BKSP:
                    result = new BKSPButtonPresenter(view, panelView, type, receiver);
                    break;
                case ActionButtonType.Press:
                    result = new PressButtonPresenter(view, panelView, type, receiver);
                    break;
                default:
                    Debug.LogError("Invalid button type");
                    return null;
            }
            
            if (!isTypeExist)
                _presentersPoolByType.Add(type, new List<BasePanelButtonPresenter>());
            
            return result;
        }

        public void Dispose()
        {
            if (_buttonsViews is null)
                return;
            
            foreach (var button in _buttonsViews)
                Object.Destroy(button);

            _buttonsViews.Clear();
        }
    }
}