using UnityEngine;

public class BootstrapState : IState
{
    private const string Initial = "Initial";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _services = services;

        RegisterServices();
    }

    public void Enter()
    {
        _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
    }

    public void Exit()
    {
        
    }

    private void EnterLoadLevel()
    {
        _stateMachine.Enter<LoadProgressState>();
    }

    private void RegisterServices()
    {
        _services.RegisterSingle<IInputService>(InputService());
        _services.RegisterSingle<IAssets>(new AssetProvider());
        _services.RegisterSingle<IProgressService>(new ProgressService());
        _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>()));
        _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IProgressService>(), _services.Single<IGameFactory>()));
    }

    private static IInputService InputService()
    {
        if (Application.isMobilePlatform)
            return new MobileInputService();
        else
            return new StandaloneInputService();
    }
}