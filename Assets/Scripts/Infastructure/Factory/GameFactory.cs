using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameFactory : IGameFactory
{
    private readonly IAssets _assets;
    private readonly IStaticDataService _staticData;

    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    private GameObject _characterGameObject { get; set; }

    public GameFactory(IAssets assets, IStaticDataService staticDataService)
    {
        _assets = assets;
        _staticData = staticDataService;
    }

    public GameObject CreateCharacter(GameObject initialPoint)
    {
        _characterGameObject = InstantiateRegistered(AssetPath.CharacterPath, initialPoint.transform.position);
        return _characterGameObject;
    }

    public GameObject CreateHud() =>
        InstantiateRegistered(AssetPath.HUDPath);

    public GameObject CreateEnemy(EnemyTypeId enemyTypeId, Transform parent)
    {
        EnemyStaticData enemyData = _staticData.ForEnemy(enemyTypeId);
        GameObject enemy = UnityEngine.Object.Instantiate(enemyData.Prefab, parent.position, Quaternion.identity);

        var health = enemy.GetComponent<IHealth>();
        health.CurrentHealth = enemyData.HP;
        health.MaxHealth = enemyData.HP;

        enemy.GetComponent<ActorUI>().Construct(health);
        enemy.GetComponent<AgentMoveToPlayer>().Construct(_characterGameObject.transform);
        enemy.GetComponent<NavMeshAgent>().speed = enemyData.MoveSpeed;

        var attack = enemy.GetComponent<Attack>();
        attack.Construct(_characterGameObject.transform);
        attack.Damage = enemyData.Damage;
        attack.Cleavage = enemyData.Cleavege;
        attack.EffectiveDistance = enemyData.EffectiveDistance;


        return enemy;
    }

    public void Cleanup()
    {
        ProgressReaders.Clear();
        ProgressWriters.Clear();
    }

    private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
    {
        GameObject gameObject = _assets.Instantiate(prefabPath, position);
        RegisterProgressWatchers(gameObject);
        return gameObject;
    }

    private GameObject InstantiateRegistered(string prefabPath)
    {
        GameObject gameObject = _assets.Instantiate(prefabPath);
        RegisterProgressWatchers(gameObject);
        return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
        foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            Register(progressReader);
    }
    public void Register(ISavedProgressReader progressReader)
    {
        if (progressReader is ISavedProgress progressWriter)
            ProgressWriters.Add(progressWriter);
        ProgressReaders.Add(progressReader);
    }
}