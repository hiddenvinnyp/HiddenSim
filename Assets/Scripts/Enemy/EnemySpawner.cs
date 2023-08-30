using UnityEngine;

public class EnemySpawner : MonoBehaviour, ISavedProgress
{
    [SerializeField] private EnemyTypeId _enemyTypeId;
    private string _id;
    public bool _slain;
    public EnemyTypeId EnemyTypeId 
    { 
        get { return _enemyTypeId; } 
        set { _enemyTypeId = value; } 
    }

    public string Id
    {
        get { return _id; }
        set { _id = value; }
    }

    private IGameFactory _factory;
    private EnemyDeath _enemyDeath;

    private void Awake()
    {
        _id = GetComponent<UniqueID>().Id;
        _factory = AllServices.Container.Single<IGameFactory>();
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        if (_slain)
            progress.KillData.ClearedSpawners.Add(_id);
    }

    public void LoadProgress(PlayerProgress progress)
    {
        if (progress.KillData.ClearedSpawners.Contains(_id))
            _slain = true;
        else
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        var enemy = _factory.CreateEnemy(_enemyTypeId, transform);
        _enemyDeath = enemy.GetComponent<EnemyDeath>();
        _enemyDeath.DeathHappend += Slay;
    }

    private void Slay()
    {
        if (_enemyDeath != null)
            _enemyDeath.DeathHappend -= Slay;
        _slain = true;
    }
}
