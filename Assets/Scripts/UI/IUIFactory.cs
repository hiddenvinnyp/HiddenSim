public interface IUIFactory : IService
{
    void CreateUIRoot();
    void CreateWindow(WindowId windowId);
}
