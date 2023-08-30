using System;
using System.Collections.Generic;
using UnityEngine;

public interface IGameFactory : IService
{
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }

    void Cleanup();
    GameObject CreateCharacter(GameObject initialPoint);
    GameObject CreateEnemy(EnemyTypeId enemyTypeId, Transform parent);
    GameObject CreateHud();
    RewardPiece CreateReward();
    void CreateSpawner(Vector3 position, string spawnerId, EnemyTypeId enemyTypeId);
}
