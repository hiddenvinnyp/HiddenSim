public interface IStaticDataService : IService
{
    string[] Episodes { get; }

    EnemyStaticData ForEnemy(EnemyTypeId enemyTypeId);
    LevelSpawnersStaticData ForLevelSpawners(string sceneKey);
    WindowConfig ForWindow(WindowId windowId);
    EpisodeStaticData ForEpisode(string EpisodeName);
    LevelStaticData ForLevel(string SceneName);
    void LoadEnemies();
    void LoadEpisodes();
}