using System;
using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{
    private const string InitialPointTag = "InitialPoint";
    private const string EnemySpawnerTag = "EnemySpawner";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IProgressService _progressService;

    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, 
        IGameFactory gameFactory, IProgressService progressService)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _curtain = curtain;
        _gameFactory = gameFactory;
        _progressService = progressService;
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
        InitGameWorld();
        InformProgressReaders();
        _stateMachine.Enter<GameLoopState>();
    }

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            progressReader.LoadProgress(_progressService.Progress);
    }

    private void InitGameWorld()
    {
        InitSpawners();

        GameObject character = InitCharacter();
        GameObject hud = _gameFactory.CreateHud();
        hud.GetComponentInChildren<ActorUI>().Construct(character.GetComponent<CharacterHealth>());
        CamerFollow(character);
    }

    private GameObject InitCharacter()
    {
        return _gameFactory.CreateCharacter(GameObject.FindWithTag(InitialPointTag));
    }

    private void InitSpawners()
    {
        foreach (GameObject spawnerObject in GameObject.FindGameObjectsWithTag(EnemySpawnerTag))
        {
            var spawner = spawnerObject.GetComponent<EnemySpawner>();
            _gameFactory.Register(spawner);
        }
    }

    private static void CamerFollow(GameObject target)
    {
        Camera.main.GetComponent<CameraFollow>().Follow(target);
    }
}