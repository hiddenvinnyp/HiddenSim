using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/LevelData")]
public class LevelSpawnersStaticData : ScriptableObject
{
    public string LevelKey;
    public List<EnemySpawnerData> EnemySpawners;
    public Vector3 InitialHeroPosition;
}
