using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    private const string MonstersStaticDataPath = "StaticData/Enemies";
    private const string LevelsSpawnersStaticDataPath = "StaticData/Levels";
    private const string WindowsStaticDataPath = "StaticData/UI/WindowStaticData";
    private const string EpisodesStaticDataPath = "StaticData/MainMenu";
    private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
    private Dictionary<string, LevelSpawnersStaticData> _levelsSpawners;
    private Dictionary<string, EpisodeStaticData> _episodes;
    private Dictionary<string, LevelStaticData> _levels;
    private Dictionary<WindowId, WindowConfig> _windowConfigs;

    public string[] Episodes => _episodes.Keys.ToArray();

    public void LoadEnemies()
    {
        _enemies = Resources.LoadAll<EnemyStaticData>(MonstersStaticDataPath).ToDictionary(x => x.EnemyTypeId, x => x);
        _levelsSpawners = Resources.LoadAll<LevelSpawnersStaticData>(LevelsSpawnersStaticDataPath).ToDictionary(x => x.LevelKey, x => x);
        _windowConfigs = Resources.Load<WindowStaticData>(WindowsStaticDataPath).Configs.ToDictionary(x => x.WindowId, x => x);
    }

    public void LoadEpisodes()
    {
        _episodes = Resources.LoadAll<EpisodeStaticData>(EpisodesStaticDataPath).ToDictionary(x => x.EpisodeName, x => x);
        _levels = Resources.LoadAll<LevelStaticData>(EpisodesStaticDataPath).ToDictionary(x => x.Name, x => x);
    }

    public EnemyStaticData ForEnemy(EnemyTypeId enemyTypeId) =>
        _enemies.TryGetValue(enemyTypeId, out EnemyStaticData staticData) ? staticData : null;

    public LevelSpawnersStaticData ForLevelSpawners(string sceneKey) => 
        _levelsSpawners.TryGetValue(sceneKey, out LevelSpawnersStaticData staticData) ? staticData : null;

    public WindowConfig ForWindow(WindowId windowId) => 
        _windowConfigs.TryGetValue(windowId, out WindowConfig staticData) ? staticData : null;

    public EpisodeStaticData ForEpisode(string EpisodeName) =>
        _episodes.TryGetValue(EpisodeName, out EpisodeStaticData staticData) ? staticData : null;

    public LevelStaticData ForLevel(string SceneName) =>
        _levels.TryGetValue(SceneName, out LevelStaticData staticData) ? staticData : null;
}
