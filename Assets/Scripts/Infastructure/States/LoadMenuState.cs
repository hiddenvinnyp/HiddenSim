using System.Collections.Generic;
using System.Linq;
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
        EpisodeStaticData[] episodes = _staticData.ForGamePlan().Episodes;
        for (int i = 0; i < episodes.Length; i++)
        {
            bool isLocked = true;
            string episodeName = episodes[i].EpisodeName;

            // TODO: добавить проверку на последний доступный уровень, ведь у него тоже скор == 0
            //if (!IsFirst
            //||(levelData.Dictionary.TryGetValue(Levels[0].Name, out LevelData level) && level.Score == 0)
            //|| (!IsPrevousEpisodeDone && !IsFirst))

            if (i == 0 
                || (i > 0 && (_progressService.Progress.LevelProgressData.Dictionary.TryGetValue(
                episodes[i - 1].Levels.Last().Name, out LevelData levelData) && levelData.Score > 0)))
            {
                isLocked = false;
            }
            EpisodeStaticData episodeData = episodes[i]; // _staticData.ForEpisode(episodeName);
            _gameFactory.CreateEpisodeHex(new Vector3(i, 0, 0), episodeName, 
                                            episodeData.EpisodeVisualModel, 
                                            episodeData.EpisodeScene, // doesn't use in this version
                                            episodeData.Levels,
                                            isLocked);
        }
    }

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            progressReader.LoadProgress(_progressService.Progress);
    }
}
