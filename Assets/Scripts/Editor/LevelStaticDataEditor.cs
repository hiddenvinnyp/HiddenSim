using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(LevelStaticData))]
public class LevelStaticDataEditor : Editor
{ 
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelStaticData levelData = (LevelStaticData)target;

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