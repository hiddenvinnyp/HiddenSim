﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelState : IPayloadedState<string>
{
    private const string InitialPointTag = "InitialPoint";
    private const string EnemySpawnerTag = "EnemySpawner";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IProgressService _progressService;
    private readonly IStaticDataService _staticData;
    private readonly IUIFactory _uiFactory;

    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
        IGameFactory gameFactory, IProgressService progressService, IStaticDataService staticDataService, IUIFactory uiFactory)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _curtain = curtain;
        _gameFactory = gameFactory;
        _progressService = progressService;
        _staticData = staticDataService;
        _uiFactory = uiFactory;
    }

    public void Enter(string sceneName)
    {
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
        InitSpawners();
        InitRewardPieces();

        GameObject character = InitCharacter();
        GameObject hud = _gameFactory.CreateHud();
        hud.GetComponentInChildren<ActorUI>().Construct(character.GetComponent<CharacterHealth>());
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

    private GameObject InitCharacter()
    {
        return _gameFactory.CreateCharacter(GameObject.FindWithTag(InitialPointTag));
    }

    private void InitSpawners()
    {
        string sceneKey = SceneManager.GetActiveScene().name;
        LevelStaticData levelData = _staticData.ForLevel(sceneKey);

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