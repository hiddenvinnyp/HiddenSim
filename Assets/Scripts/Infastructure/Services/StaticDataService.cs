using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    private const string MonstersStaticDataPath = "StaticData/Enemies";
    private const string LevelsStaticDataPath = "StaticData/Levels";
    private const string WindowsStaticDataPath = "StaticData/UI/WindowStaticData";
    private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
    private Dictionary<string, LevelStaticData> _levels;
    private Dictionary<WindowId, WindowConfig> _windowConfigs;

    public void LoadEnemies()
    {
        _enemies = Resources.LoadAll<EnemyStaticData>(MonstersStaticDataPath).ToDictionary(x => x.EnemyTypeId, x => x);
        _levels = Resources.LoadAll<LevelStaticData>(LevelsStaticDataPath).ToDictionary(x => x.LevelKey, x => x);
        _windowConfigs = Resources.Load<WindowStaticData>(WindowsStaticDataPath).Configs.ToDictionary(x => x.WindowId, x => x);
    }

    public EnemyStaticData ForEnemy(EnemyTypeId enemyTypeId) =>
        _enemies.TryGetValue(enemyTypeId, out EnemyStaticData staticData) ? staticData : null;

    public LevelStaticData ForLevel(string sceneKey) => 
        _levels.TryGetValue(sceneKey, out LevelStaticData staticData) ? staticData : null;

    public WindowConfig ForWindow(WindowId windowId) => 
        _windowConfigs.TryGetValue(windowId, out WindowConfig staticData) ? staticData : null;
}
