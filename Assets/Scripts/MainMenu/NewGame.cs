using UnityEngine;

public class NewGame : MonoBehaviour
{
    private IGameStateMachine _stateMachine;
    private IStaticDataService _staticData;
    private string _firstLevelName;

    private void Start()
    {
        _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        _staticData = AllServices.Container.Single<IStaticDataService>();
        GetFirstLevelName();
    }

    public void CleanUpProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void LoadFirstLevel()
    {
        _stateMachine.Enter<LoadLevelState, string>(_firstLevelName);
    }

    private string GetFirstLevelName()
    {
        foreach (var episode in _staticData.Episodes)
        {
            EpisodeStaticData episodeData = _staticData.ForEpisode(episode);
            if (episodeData.IsFirst)
            {
                _firstLevelName = episodeData.Levels[0].SceneName;
            }
        }
        return _firstLevelName;
    }
}
