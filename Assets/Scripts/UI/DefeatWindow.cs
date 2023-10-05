using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatWindow : WindowBase
{
    [SerializeField] private GameObject _vfxPrefab;
    [SerializeField] private AudioSource _music;

    private ISaveLoadService _saveLoadService;
    private IStaticDataService _staticData;
    private IProgressService _progressService;

    protected override void OnAwake()
    {
        base.OnAwake();
        _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        _staticData = AllServices.Container.Single<IStaticDataService>();
        _progressService = AllServices.Container.Single<IProgressService>();

        CloseButton.onClick.AddListener(OnClosed);

        _music.Play();

        GameObject vfx = Instantiate(_vfxPrefab, transform.position, Quaternion.identity);
        vfx.transform.SetParent(gameObject.transform);
        vfx.transform.Rotate(90, 0, 0);
        vfx.transform.localScale *= 100f;
    }

    public void Restart()
    {
        CleanProgressOnLevel(_levelName);
        StateMachine.Enter<LoadProgressState>();
        StateMachine.Enter<LoadLevelState, string>(_levelName);
    }

    public void LoadMainMenu()
    {
        _saveLoadService.SaveProgress();
        StateMachine.Enter<LoadMenuState>();
    }

    private void CleanProgressOnLevel(string levelName)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        _progressService.Progress.WorldData.PositionOnLevel = new PositionOnLevel(sceneName,
                                        _staticData.ForLevelSpawners(sceneName).InitialHeroPosition.AsVectorData()); 
        
        _progressService.Progress.CharacterState.ResetHP();
        if (_progressService.Progress.LevelProgressData.Dictionary.TryGetValue(levelName, out var data))
        {
            LevelData levelData = new LevelData();
            levelData.Score = 0;
            levelData.HiddenObjectDataDictionary = new HiddenObjectDataDictionary();
            _progressService.Progress.LevelProgressData.Dictionary[levelName] = levelData;
        }
        _saveLoadService.SaveProgress();
    }

    private void OnClosed()
    {
        _music.Stop();
    }
}

