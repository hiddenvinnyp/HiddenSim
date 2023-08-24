using System;

public class LoadProgressState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly IProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;

    public LoadProgressState(GameStateMachine gameStateMachine, IProgressService progressService, ISaveLoadService saveLoadService)
    {
        _gameStateMachine = gameStateMachine;
        _progressService = progressService;
        _saveLoadService = saveLoadService;
    }

    public void Enter()
    {
        LoadProgressOrInitNew();
        _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
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
        var progress = new PlayerProgress(initialLevel: "SampleScene");

        progress.CharacterState.MaxHP = 100;
        progress.WeaponStats.Damage = 5f;
        progress.WeaponStats.DamageRadius = 0.5f;
        progress.CharacterState.ResetHP();

        return progress;
    }
}