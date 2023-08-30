public interface IStaticDataService : IService
{
    EnemyStaticData ForEnemy(EnemyTypeId enemyTypeId);
    LevelStaticData ForLevel(string sceneKey);
    void LoadEnemies();
}