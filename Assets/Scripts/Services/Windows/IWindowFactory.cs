using Services.Windows;

namespace DefaultNamespace.Services.Windows
{
    public interface IWindowFactory : IService
    {
        public T CreateWindow<T>(WindowType windowType) where T : BaseWindow;
    }
}