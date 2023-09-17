using UnityEngine;
public class LoadMenuState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IProgressService _progressService;
    private readonly IStaticDataService _staticData;

    public LoadMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IProgressService progressService, IStaticDataService staticData)
    {
        _stateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _curtain = curtain;
        _gameFactory = gameFactory;
        _progressService = progressService;
        _staticData = staticData;
    }

    public void Enter()
    {
        _curtain.Show();
        _gameFactory.Cleanup();
        _sceneLoader.Load("MainMenu", OnLoaded);
    }

    public void Exit()
    {
        _curtain.Hide();
    }

    private void OnLoaded()
    {
        InitEpisodes();
        InformProgressReaders();
        _stateMachine.Enter<GameLoopState>();
    }

    private void InitEpisodes()
    {
        for (int i = 0; i < _staticData.Episodes.Length; i++)
        {
            string episodeName = _staticData.Episodes[i];
 
            EpisodeStaticData episodeData = _staticData.ForEpisode(episodeName);
            _gameFactory.CreateEpisodeHex(new Vector3(i, 0, 0), episodeName, episodeData.EpisodeVisualModel, episodeData.EpisodeScene, episodeData.Levels, episodeData.NextEpisode, episodeData.IsFirst);
        }
    }

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            progressReader.LoadProgress(_progressService.Progress);
    }
}
