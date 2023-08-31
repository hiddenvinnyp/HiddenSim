using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/LevelData")]
public class LevelStaticData : ScriptableObject
{
    public string LevelKey;
    public List<EnemySpawnerData> EnemySpawners;
}
