public interface IStaticDataService : IService
{
    EnemyStaticData ForEnemy(EnemyTypeId enemyTypeId);
    LevelStaticData ForLevel(string sceneKey);
    WindowConfig ForWindow(WindowId windowId);
    void LoadEnemies();
}