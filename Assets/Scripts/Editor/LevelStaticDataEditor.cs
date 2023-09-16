using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(LevelSpawnersStaticData))]
public class LevelStaticDataEditor : Editor
{ 
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelSpawnersStaticData levelData = (LevelSpawnersStaticData)target;

        if (GUILayout.Button("Collect"))
        {
            levelData.EnemySpawners = FindObjectsOfType<SpawnMarker>()
                .Select(x=>new EnemySpawnerData(x.GetComponent<UniqueID>().Id, x.EnemyTypeId, x.transform.position))
                .ToList();

            levelData.LevelKey = SceneManager.GetActiveScene().name;
        }

        EditorUtility.SetDirty(target);
    }
}