using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, ISavedProgress
{
    [SerializeField] private EnemyTypeId _enemyTypeId;
    private string _id;
    private IGameFactory _factory;
    public bool _slain;
    private EnemyDeath _enemyDeath;

    private void Awake()
    {
        _id = GetComponent<UniqueID>().Id;
        _factory = AllServices.Container.Single<IGameFactory>();
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

    public void UpdateProgress(PlayerProgress progress)
    {
        if (_slain)
            progress.KillData.ClearedSpawners.Add(_id);
    }

}
