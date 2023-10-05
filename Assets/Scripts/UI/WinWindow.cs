using System.Collections.Generic;
using UnityEngine;

public class WinWindow : WindowBase
{
    [SerializeField] private GameObject _vfxPrefab;
    [SerializeField] private AudioSource _music;

    private ISaveLoadService _saveLoadService;
    private IStaticDataService _staticData;

    protected override void OnAwake()
    {
        base.OnAwake();
        _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        _staticData = AllServices.Container.Single<IStaticDataService>();

        CloseButton.onClick.AddListener(OnClosed);

        _music.Play();
        GameObject vfx = Instantiate(_vfxPrefab);
        vfx.transform.SetParent(transform.GetComponent<RectTransform>(), false);
    }

    public void LoadNextLevel()
    {
        _saveLoadService.SaveProgress();
        StateMachine.Enter<LoadLevelState, string>(NextLevel(_levelName));
    }

    private string NextLevel(string levelName)
    {
        List<string> levels = _staticData.GetAllLevels();
        int currentIndex = levels.IndexOf(levelName);

        if (currentIndex != -1 && currentIndex < levels.Count - 1)
        {
            return levels[currentIndex + 1];
        }

        return levelName;
    }

    public void LoadMainMenu()
    {
        _saveLoadService.SaveProgress();
        StateMachine.Enter<LoadMenuState>();
    }

    private void OnClosed()
    {
        _music.Stop();
    }
}
