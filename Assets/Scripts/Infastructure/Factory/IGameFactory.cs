using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IGameFactory : IService
{
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }

    void Cleanup();
    GameObject CreateCharacter(Vector3 initialPoint);
    Task<GameObject> CreateEnemy(EnemyTypeId enemyTypeId, Transform parent);
    void CreateEpisodeHex(Vector3 position, string episodeName, GameObject episodeVisualModel, string episodeScene, LevelStaticData[] levels, bool isLocked);
    GameObject CreateHud();
    RewardPiece CreateReward();
    void CreateSpawner(Vector3 position, string spawnerId, EnemyTypeId enemyTypeId);
    void RegisterItemService(IHiddenItemsService hiddenItemsService);
}
