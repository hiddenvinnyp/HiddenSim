using System.Collections.Generic;
public interface IStaticDataService : IService
{
    List<string> GetAllEpisodes();
    List<string> GetAllLevels();
    EnemyStaticData ForEnemy(EnemyTypeId enemyTypeId);
    LevelSpawnersStaticData ForLevelSpawners(string sceneKey);
    WindowConfig ForWindow(WindowId windowId);
    EpisodeStaticData ForEpisode(string episodeName);
    LevelStaticData ForLevel(string sceneName);
    void LoadEnemies();
    void LoadEpisodes();
    GamePlanStaticData ForGamePlan();
}