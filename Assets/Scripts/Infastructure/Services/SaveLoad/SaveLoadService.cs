using UnityEngine;

public class SaveLoadService : ISaveLoadService
{
    private const string ProgressKey = "Progress";
    private readonly IProgressService _progressService;
    private readonly IGameFactory _gameFactory;

    public SaveLoadService(IProgressService progressService, IGameFactory gameFactory) 
    {
        _progressService = progressService;
        _gameFactory = gameFactory;
    }

    public PlayerProgress LoadProgress() => 
        PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();

    public void SaveProgress()
    {
        foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            progressWriter.UpdateProgress(_progressService.Progress);

        PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
    }
}