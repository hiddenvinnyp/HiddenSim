using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, ISavedProgress
{
    [SerializeField] private EnemyTypeId _enemyTypeId;
    private string _id;
    public bool _slain;

    private void Awake()
    {
        _id = GetComponent<UniqueID>().Id;
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
        
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        if (_slain)
            progress.KillData.ClearedSpawners.Add(_id);
    }

}
