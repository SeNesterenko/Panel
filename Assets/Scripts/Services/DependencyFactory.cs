using DefaultNamespace.Services.Windows;
using Transponder;
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
            builder.RegisterEntryPoint<TransponderService>();
            
            builder.RegisterComponent(_mainCanvas);
            
            builder.Register<ILocatorController, LocatorController>(Lifetime.Singleton);
            builder.Register<IPanelController, PanelController>(Lifetime.Singleton);
            builder.Register<IWindowFactory, WindowFactory>(Lifetime.Singleton);
        }
    }
}