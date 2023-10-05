using System.Collections.Generic;
using UnityEngine;

public class LoadProgressState : IState
{
    private const string InitialLevel = "FirstFloor";
    private readonly GameStateMachine _gameStateMachine;
    private readonly IProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;
    private readonly IStaticDataService _staticData;

    public LoadProgressState(GameStateMachine gameStateMachine, IProgressService progressService, ISaveLoadService saveLoadService, IStaticDataService staticDataService)
    {
        _gameStateMachine = gameStateMachine;
        _progressService = progressService;
        _saveLoadService = saveLoadService;
        _staticData = staticDataService;
    }

    public void Enter()
    {
        LoadProgressOrInitNew();
        _gameStateMachine.Enter<LoadMenuState>();
        //_gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
    }

    public void Exit()
    {
        
    }

    private void LoadProgressOrInitNew()
    {
        _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
    }

    private PlayerProgress NewProgress()
    {
        var progress = new PlayerProgress(initialLevel: InitialLevel);

        progress.CharacterState.MaxHP = 100;
        progress.CharacterState.ResetHP();
        progress.WeaponStats.Damage = 5f;
        progress.WeaponStats.DamageRadius = 1f;
        progress.LevelProgressData = new LevelProgressData();

        foreach (string level in _staticData.GetAllLevels())
        {
            LevelData levelData = new LevelData();
            levelData.Score = 0;
            levelData.HiddenObjectDataDictionary = new HiddenObjectDataDictionary();
            progress.LevelProgressData.Dictionary.Add(level, levelData);
        }

        return progress;
    }
}