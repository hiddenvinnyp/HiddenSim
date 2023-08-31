using UnityEngine;

public class UIFactory : IUIFactory
{
    private const string UIRootPath = "Prefabs/UI/RootUICanvas";
    private readonly IAssets _assets;
    private Transform _uiRoot;

    public UIFactory(IAssets assets)
    {
        _assets = assets;
    }

    public void CreateUIRoot() => 
        _uiRoot = _assets.Instantiate(UIRootPath).transform;

    public void CreateOptions()
    {
    }

    public void CreatePause()
    {
    }

    public void CreateShop()
    {
    }
}