using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelState : IPayloadedState<string>
{
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IProgressService _progressService;
    private readonly IStaticDataService _staticData;
    private readonly IUIFactory _uiFactory;
    private readonly IHiddenItemsService _hiddenItemsService;
    private string _levelName;

    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
        IGameFactory gameFactory, IProgressService progressService, IStaticDataService staticDataService, IUIFactory uiFactory, IHiddenItemsService hiddenItemsService)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _curtain = curtain;
        _gameFactory = gameFactory;
        _progressService = progressService;
        _staticData = staticDataService;
        _uiFactory = uiFactory;
        _hiddenItemsService = hiddenItemsService;
    }

    public void Enter(string levelName)
    {
        _levelName = levelName;
        string sceneName = _staticData.ForLevel(levelName).SceneName;
        _curtain.Show();
        _gameFactory.CleanUp();
        _gameFactory.WarmUp();
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {
        _curtain.Hide();
    }

    private async void OnLoaded()
    {
        await InitUIRootAsync();
        await InitGameWorld();
        InformProgressReaders();
        _stateMachine.Enter<GameLoopState>();
    }

    private async Task InitUIRootAsync() => 
        await _uiFactory.CreateUIRoot(_levelName);

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            progressReader.LoadProgress(_progressService.Progress);
    }

    private async Task InitGameWorld()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        LevelSpawnersStaticData levelData = _staticData.ForLevelSpawners(sceneName);

        await InitSpawners(levelData);
        await InitRewardPieces();

        _gameFactory.RegisterItemService(_hiddenItemsService);
        _hiddenItemsService.InitHiddenItems(_levelName);

        GameObject character = await InitCharacter(levelData);


        GameObject hud = await _gameFactory.CreateHud();
        hud.GetComponentInChildren<ActorUI>().Construct(character.GetComponent<CharacterHealth>());
        hud.GetComponentInChildren<StarsUI>().Construct(_hiddenItemsService);
        hud.GetComponentInChildren<ItemsPanelUI>().Construct(_hiddenItemsService);
        CamerFollow(character);
    }

    private async Task InitRewardPieces()
    {
        foreach (string key in _progressService.Progress.WorldData.RewardData.RewardPiecesOnScene.Dictionary.Keys)
        {
            RewardPiece rewardPiece = await _gameFactory.CreateReward();
            rewardPiece.GetComponent<UniqueID>().Id = key;
        }
    }

    private async Task<GameObject> InitCharacter(LevelSpawnersStaticData levelData) =>
        await _gameFactory.CreateCharacter(levelData.InitialHeroPosition);

    private async Task InitSpawners(LevelSpawnersStaticData levelData)
    {
        foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
            await _gameFactory.CreateSpawner(spawnerData.Position, spawnerData.Id, spawnerData.EnemyTypeId);        
    }

    private static void CamerFollow(GameObject target)
    {
        Camera.main.GetComponent<CameraFollow>().Follow(target);
        Camera.main.GetComponent<HideBlockedWalls>().CharacterController = target.GetComponent<CharacterController>();
    }
}