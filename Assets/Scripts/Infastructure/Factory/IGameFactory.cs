﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IGameFactory : IService
{
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }

    void CleanUp();
    Task<GameObject> CreateCharacter(Vector3 initialPoint);
    Task<GameObject> CreateEnemy(EnemyTypeId enemyTypeId, Transform parent);
    Task CreateEpisodeHex(Vector3 position, string episodeName, GameObject episodeVisualModel, string episodeScene, LevelStaticData[] levels, bool isLocked);
    Task<GameObject> CreateHud();
    Task<RewardPiece> CreateReward();
    Task CreateSpawner(Vector3 position, string spawnerId, EnemyTypeId enemyTypeId);
    void RegisterItemService(IHiddenItemsService hiddenItemsService);
    void WarmUp();
}
