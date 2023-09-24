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
        _gameFactory.Cleanup();
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {
        _curtain.Hide();
    }

    private void OnLoaded()
    {
        InitUIRoot();
        InitGameWorld();
        InformProgressReaders();
        _stateMachine.Enter<GameLoopState>();
    }

    private void InitUIRoot() => 
        _uiFactory.CreateUIRoot();

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            progressReader.LoadProgress(_progressService.Progress);
    }

    private void InitGameWorld()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        LevelSpawnersStaticData levelData = _staticData.ForLevelSpawners(sceneName);


        InitSpawners(levelData);
        InitRewardPieces();

        _hiddenItemsService.InitHiddenItems(_levelName);

        GameObject character = InitCharacter(levelData);
        GameObject hud = _gameFactory.CreateHud();
        hud.GetComponentInChildren<ActorUI>().Construct(character.GetComponent<CharacterHealth>());
        hud.GetComponentInChildren<StarsUI>().Construct(_hiddenItemsService, _staticData.ForLevel(_levelName).HiddenAmount, _hiddenItemsService.TryGetFoundItemsAmount(_levelName, out int foundAmount)? foundAmount : 0);
        hud.GetComponentInChildren<ItemsPanelUI>().Construct(_hiddenItemsService);
        CamerFollow(character);
    }

    private void InitRewardPieces()
    {
        foreach (string key in _progressService.Progress.WorldData.RewardData.RewardPiecesOnScene.Dictionary.Keys)
        {
            RewardPiece rewardPiece = _gameFactory.CreateReward();
            rewardPiece.GetComponent<UniqueID>().Id = key;
        }
    }

    private GameObject InitCharacter(LevelSpawnersStaticData levelData) =>
        _gameFactory.CreateCharacter(levelData.InitialHeroPosition);

    private void InitSpawners(LevelSpawnersStaticData levelData)
    {
        foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
        {
            _gameFactory.CreateSpawner(spawnerData.Position, spawnerData.Id, spawnerData.EnemyTypeId);
        }
    }

    private static void CamerFollow(GameObject target)
    {
        Camera.main.GetComponent<CameraFollow>().Follow(target);
    }
}