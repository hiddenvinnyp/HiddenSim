using UnityEngine;

public class UIFactory : IUIFactory
{
    private const string UIRootPath = "Prefabs/UI/RootUICanvas";
    private readonly IAssets _assets;
    private readonly IStaticDataService _staticData;
    private readonly IProgressService _progressService;
    private Transform _uiRoot;

    public UIFactory(IAssets assets, IStaticDataService staticData, IProgressService progressService)
    {
        _assets = assets;
        _staticData = staticData;
        _progressService = progressService;
    }

    public void CreateUIRoot() => 
        _uiRoot = _assets.Instantiate(UIRootPath).transform;

    public void CreateWindow(WindowId windowId)
    {
        WindowConfig config = _staticData.ForWindow(windowId);
        WindowBase window = Object.Instantiate(config.WindowPrefab, _uiRoot);
        window.Construct(_progressService);
    }
}
