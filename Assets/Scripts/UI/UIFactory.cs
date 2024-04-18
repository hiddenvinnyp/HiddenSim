using System.Threading.Tasks;
using UnityEngine;

public class UIFactory : IUIFactory
{
    private const string UIRootPath = "RootUICanvas";
    private readonly IAssets _assets;
    private readonly IStaticDataService _staticData;
    private readonly IProgressService _progressService;
    private readonly IGameStateMachine _stateMachine;
    private readonly IPauseService _pauseService;
    private Transform _uiRoot;
    private string _levelName;

    public UIFactory(IAssets assets, IStaticDataService staticData, IProgressService progressService, IGameStateMachine stateMachine, IPauseService pauseService)
    {
        _assets = assets;
        _staticData = staticData;
        _progressService = progressService;
        _stateMachine = stateMachine;
        _pauseService = pauseService;
    }

    public async Task CreateUIRoot(string levelName)
    {
        _levelName = levelName;
        GameObject root = await _assets.Instantiate(UIRootPath);
        _uiRoot = root.transform;
    }

    public void CreateWindow(WindowId windowId)
    {
        WindowConfig config = _staticData.ForWindow(windowId);
        WindowBase window = Object.Instantiate(config.WindowPrefab, _uiRoot);
        
        if (windowId == WindowId.Pause)
        {
            Debug.Log("PAUSE WINDOW");
            window.Construct(_progressService, _stateMachine, _pauseService);
        } else        
            window.Construct(_progressService, _stateMachine, _levelName);
    }
}
