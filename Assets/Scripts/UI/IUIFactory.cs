public interface IUIFactory : IService
{
    void CreateUIRoot(string levelName);
    void CreateWindow(WindowId windowId);
}
