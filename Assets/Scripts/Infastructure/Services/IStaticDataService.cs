public interface IStaticDataService : IService
{
    EnemyStaticData ForEnemy(EnemyTypeId enemyTypeId);
    void LoadEnemies();
}