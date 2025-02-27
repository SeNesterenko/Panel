using DefaultNamespace.Services.Windows;
using MainMenu;
using Transponder;
using Transponder.Locator;
using Transponder.Panel;
using Transponder.Panel.Buttons;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace.Services
{
    public class DependencyFactory : LifetimeScope
    {
        [SerializeField] private Transform _mainCanvas;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MenuWindowService>();
            
            builder.RegisterComponent(_mainCanvas);

            builder.Register<ITransponderService, TransponderService>(Lifetime.Singleton);
            builder.Register<ILocatorController, LocatorController>(Lifetime.Singleton);
            builder.Register<IPanelController, PanelController>(Lifetime.Singleton);
            builder.Register<IPanelButtonsFactory, PanelButtonsFactory>(Lifetime.Singleton);
            builder.Register<IPanelConfigProvider, PanelConfigProvider>(Lifetime.Singleton);
            builder.Register<IWindowFactory, WindowFactory>(Lifetime.Singleton);
            builder.Register<IPlanesFactory, PlanesFactory>(Lifetime.Singleton);
            builder.Register<IPlanesConfigProvider, PlanesConfigProvider>(Lifetime.Singleton);
        }
    }
}