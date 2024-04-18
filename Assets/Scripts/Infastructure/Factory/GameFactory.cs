using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class GameFactory : IGameFactory
{
    private readonly IAssets _assets;
    private readonly IStaticDataService _staticData;
    private readonly IProgressService _progressService;
    private readonly IWindowService _windowService;
    private readonly IGameStateMachine _stateMachine;

    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    private GameObject _characterGameObject { get; set; }

    public GameFactory(IAssets assets, IStaticDataService staticDataService, IProgressService progressService, IWindowService windowService, IGameStateMachine stateMachine)
    {
        _assets = assets;
        _staticData = staticDataService;
        _progressService = progressService;
        _windowService = windowService;
        _stateMachine = stateMachine;
    }

    public async void WarmUp()
    {
        await _assets.Load<GameObject>(AssetAddress.RewardCoin);
        await _assets.Load<GameObject>(AssetAddress.Spawner);
    }

    public async Task<RewardPiece> CreateReward()
    {
        GameObject prefab = await _assets.Load<GameObject>(AssetAddress.RewardCoin);
        RewardPiece rewardPiece = InstantiateRegistered(prefab).GetComponent<RewardPiece>();
        rewardPiece.Construct(_progressService.Progress.WorldData);
        return rewardPiece;
    }

    public async Task<GameObject> CreateCharacter(Vector3 initialPoint)
    {
        _characterGameObject = await InstantiateRegisteredAsync(AssetAddress.CharacterPath, initialPoint);
        return _characterGameObject;
    }

    public async Task<GameObject> CreateHud()
    {
        GameObject hud = await InstantiateRegisteredAsync(AssetAddress.HUDPath);
        hud.GetComponentInChildren<CoinCounter>().Construct(_progressService.Progress.WorldData);

        foreach (OpenWindowButton button in hud.GetComponentsInChildren<OpenWindowButton>())
            button.Construct(_windowService);

        return hud;
    }

    public async Task<GameObject> CreateEnemy(EnemyTypeId enemyTypeId, Transform parent)
    {
        EnemyStaticData enemyData = _staticData.ForEnemy(enemyTypeId);

        GameObject prefab = await _assets.Load<GameObject>(enemyData.PrefabReference);
        GameObject enemy = Object.Instantiate(prefab, parent.position, Quaternion.identity);

        IHealth health = enemy.GetComponent<IHealth>();
        health.CurrentHealth = enemyData.HP;
        health.MaxHealth = enemyData.HP;

        enemy.GetComponent<ActorUI>().Construct(health);
        enemy.GetComponent<AgentMoveToPlayer>().Construct(_characterGameObject.transform);
        enemy.GetComponent<NavMeshAgent>().speed = enemyData.MoveSpeed;

        RewardSpawner rewardSpawner = enemy.GetComponentInChildren<RewardSpawner>();
        rewardSpawner.SetReward(enemyData.MinLoot, enemyData.MaxLoot);
        rewardSpawner.Construct(this);

        Attack attack = enemy.GetComponent<Attack>();
        attack.Construct(_characterGameObject.transform);
        attack.Damage = enemyData.Damage;
        attack.Cleavage = enemyData.Cleavege;
        attack.EffectiveDistance = enemyData.EffectiveDistance;

        return enemy;
    }

    public async Task CreateSpawner(Vector3 position, string spawnerId, EnemyTypeId enemyTypeId)
    {
        GameObject prefab = await _assets.Load<GameObject>(AssetAddress.Spawner);
        SpawnPoint spawner = InstantiateRegistered(prefab, position).GetComponent<SpawnPoint>();
        spawner.Construct(this);
        spawner.Id = spawnerId;
        spawner.EnemyTypeId = enemyTypeId;
    }

    public async Task CreateEpisodeHex(Vector3 position, string episodeName, GameObject episodeVisualModel, string episodeScene, LevelStaticData[] levels, bool isLocked)
    {
        if (position.x % 2 != 0)
            position.z = 3;
        position.x *= 1.73f;

        var episodeHexPrefab = await InstantiateRegisteredAsync(AssetAddress.EpisodeHex, position);
        EpisodeHex episodeHex = episodeHexPrefab.GetComponent<EpisodeHex>();
        //Debug.Log(episodeHex.GetComponentInChildren<MeshCollider>().bounds.size.x);

        episodeHex.Name = episodeName;
        episodeHex.Model = episodeVisualModel;
        episodeHex.MainScene = episodeScene;
        episodeHex.Levels = levels;
        episodeHex.IsLocked = isLocked;

        episodeHex.Construct(_progressService.Progress.LevelProgressData, _stateMachine);
    }

    public void RegisterItemService(IHiddenItemsService hiddenItemsService)
    {
        if (hiddenItemsService is ISavedProgress progress)
            Register(progress);
    }

    public void CleanUp()
    {
        ProgressReaders.Clear();
        ProgressWriters.Clear();

        _assets.CleanUp();
    }

    private GameObject InstantiateRegistered(GameObject prefab)
    {
        GameObject gameObject = Object.Instantiate(prefab);
        RegisterProgressWatchers(gameObject);
        return gameObject;
    }

    private GameObject InstantiateRegistered(GameObject prefab, Vector3 position)
    {
        GameObject gameObject = Object.Instantiate(prefab, position, Quaternion.identity);
        RegisterProgressWatchers(gameObject);
        return gameObject;
    }

    private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 position)
    {
        GameObject gameObject = await _assets.Instantiate(prefabPath, position);
        RegisterProgressWatchers(gameObject);
        return gameObject;
    }

    private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
    {
        GameObject gameObject = await _assets.Instantiate(prefabPath);
        RegisterProgressWatchers(gameObject);
        return gameObject;
    }

    private void Register(ISavedProgressReader progressReader)
    {
        if (progressReader is ISavedProgress progressWriter)
            ProgressWriters.Add(progressWriter);
        ProgressReaders.Add(progressReader);
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
        foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            Register(progressReader);
    }
}