using System.Threading.Tasks;

public interface IUIFactory : IService
{
    Task CreateUIRoot(string levelName);
    void CreateWindow(WindowId windowId);
}
