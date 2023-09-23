using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    private const string MonstersStaticDataPath = "StaticData/Enemies";
    private const string LevelsSpawnersStaticDataPath = "StaticData/Levels";
    private const string WindowsStaticDataPath = "StaticData/UI/WindowStaticData";
    private const string EpisodesStaticDataPath = "StaticData/MainMenu";
    private const string GamePlanStaticDataPath = "StaticData/MainMenu/GamePlan";
    private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
    private Dictionary<string, LevelSpawnersStaticData> _levelsSpawners;
    private Dictionary<string, EpisodeStaticData> _episodes;
    private GamePlanStaticData _gamePlan;
    private Dictionary<string, LevelStaticData> _levels;
    private Dictionary<WindowId, WindowConfig> _windowConfigs;

    public List<string> GetAllEpisodes()
    {
            List<string> episodes = new List<string>();
            foreach (var episode in _gamePlan.Episodes)
            {
                episodes.Add(episode.EpisodeName);
            }            
            return episodes;
    }

    public List<string> GetAllLevels ()
    {
        List<string> levels = new List<string>();
        foreach (var episode in _gamePlan.Episodes)
        {
            foreach (LevelStaticData level in episode.Levels)
            {
                levels.Add(level.Name);
            }                
        }
        return levels;
    }

    public void LoadEnemies()
    {
        _enemies = Resources.LoadAll<EnemyStaticData>(MonstersStaticDataPath).ToDictionary(x => x.EnemyTypeId, x => x);
        _levelsSpawners = Resources.LoadAll<LevelSpawnersStaticData>(LevelsSpawnersStaticDataPath).ToDictionary(x => x.LevelKey, x => x);
        _windowConfigs = Resources.Load<WindowStaticData>(WindowsStaticDataPath).Configs.ToDictionary(x => x.WindowId, x => x);
    }

    public void LoadEpisodes()
    {
        _levels = Resources.LoadAll<LevelStaticData>(EpisodesStaticDataPath).ToDictionary(x => x.Name, x => x);
        _episodes = Resources.LoadAll<EpisodeStaticData>(EpisodesStaticDataPath).ToDictionary(x => x.EpisodeName, x => x);
        _gamePlan = Resources.Load<GamePlanStaticData>(GamePlanStaticDataPath);
    }

    public EnemyStaticData ForEnemy(EnemyTypeId enemyTypeId) =>
        _enemies.TryGetValue(enemyTypeId, out EnemyStaticData staticData) ? staticData : null;

    public LevelSpawnersStaticData ForLevelSpawners(string sceneKey) => 
        _levelsSpawners.TryGetValue(sceneKey, out LevelSpawnersStaticData staticData) ? staticData : null;

    public WindowConfig ForWindow(WindowId windowId) => 
        _windowConfigs.TryGetValue(windowId, out WindowConfig staticData) ? staticData : null;

    public EpisodeStaticData ForEpisode(string episodeName) =>
        _episodes.TryGetValue(episodeName, out EpisodeStaticData staticData) ? staticData : null;

    public LevelStaticData ForLevel(string sceneName) =>
        _levels.TryGetValue(sceneName, out LevelStaticData staticData) ? staticData : null;

    public GamePlanStaticData ForGamePlan() => _gamePlan;
}
