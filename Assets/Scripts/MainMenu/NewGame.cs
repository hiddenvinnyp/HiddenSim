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
        _firstLevelName = GetFirstLevelName();
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

    private string GetFirstLevelName() =>
        _staticData.ForGamePlan().Episodes[0].Levels[0].Name;
}
