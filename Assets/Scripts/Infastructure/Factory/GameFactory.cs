using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameFactory : IGameFactory
{
    private readonly IAssets _assets;
    private readonly IStaticDataService _staticData;
    private readonly IProgressService _progressService;

    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    private GameObject _characterGameObject { get; set; }

    public GameFactory(IAssets assets, IStaticDataService staticDataService, IProgressService progressService)
    {
        _assets = assets;
        _staticData = staticDataService;
        _progressService = progressService;
    }

    public void Register(ISavedProgressReader progressReader)
    {
        if (progressReader is ISavedProgress progressWriter)
            ProgressWriters.Add(progressWriter);
        ProgressReaders.Add(progressReader);
    }

    public RewardPiece CreatReward()
    {
        var rewardPiece = InstantiateRegistered(AssetPath.RewardCoin).GetComponent<RewardPiece>();
        rewardPiece.Construct(_progressService.Progress.WorldData);
        return rewardPiece;
    }

    public GameObject CreateCharacter(GameObject initialPoint)
    {
        _characterGameObject = InstantiateRegistered(AssetPath.CharacterPath, initialPoint.transform.position);
        return _characterGameObject;
    }

    public GameObject CreateHud()
    {
        GameObject hud = InstantiateRegistered(AssetPath.HUDPath);
        hud.GetComponentInChildren<CoinCounter>().Construct(_progressService.Progress.WorldData);
        return hud;
    }

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

        var rewardSpawner = enemy.GetComponentInChildren<RewardSpawner>();
        rewardSpawner.SetReward(enemyData.MinLoot, enemyData.MaxLoot);
        rewardSpawner.Construct(this);

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
}