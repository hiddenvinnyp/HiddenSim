using UnityEngine;

public class SpawnPoint : MonoBehaviour, ISavedProgress
{
    [SerializeField] private EnemyTypeId _enemyTypeId;
    public string Id { get; set; }
    public bool _slain;
    public EnemyTypeId EnemyTypeId 
    { 
        get { return _enemyTypeId; } 
        set { _enemyTypeId = value; } 
    }

    private IGameFactory _factory;
    private EnemyDeath _enemyDeath;

    public void Construct(IGameFactory factory) => _factory = factory;

    public void UpdateProgress(PlayerProgress progress)
    {
        if (_slain)
            progress.KillData.ClearedSpawners.Add(Id);
    }

    public void LoadProgress(PlayerProgress progress)
    {
        if (progress.KillData.ClearedSpawners.Contains(Id))
            _slain = true;
        else
        {
            Spawn();
        }
    }

    private async void Spawn()
    {
        var enemy = await _factory.CreateEnemy(_enemyTypeId, transform);
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
