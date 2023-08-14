using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/Enemy")]
public class EnemyStaticData : ScriptableObject
{
    public EnemyTypeId EnemyTypeId;
    [Range(1, 100)] public int HP;
    [Range(1f, 30f)] public float Damage;
    [Range(0.5f, 1f)] public float EffectiveDistance;
    [Range(0.5f, 1f)] public float Cleavege;

    public GameObject Prefab;
}
