using JetBrains.Annotations;
using MainMenu;
using VContainer.Unity;

namespace DefaultNamespace.Services
{
    [UsedImplicitly] //Register in DI Container
    public class EntryPoint : IStartable
    {
        private readonly IMenuWindowService _menuWindowService;

        public EntryPoint(IMenuWindowService menuWindowService) => 
            _menuWindowService = menuWindowService;

        public void Start() => 
            _menuWindowService.ShowWindow();
    }
}