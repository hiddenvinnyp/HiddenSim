using System;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(UniqueID))]
public class UniqueIdEditor : Editor
{
    private void OnEnable()
    {
        var uniqueId = (UniqueID)target;

        if (IsPrefab(uniqueId)) return;

        if (string.IsNullOrEmpty(uniqueId.Id))
            Generate(uniqueId);
        else
        {
            UniqueID[] uniqueIds = FindObjectsOfType<UniqueID>();

            if (uniqueIds.Any(other => other != uniqueId && other.Id == uniqueId.Id))
                Generate(uniqueId);
        }
    }

    private bool IsPrefab(UniqueID uniqueId) => 
        uniqueId.gameObject.scene.rootCount == 0;

    private void Generate(UniqueID uniqueId)
    {
        uniqueId.Id = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";

        if (!Application.isPlaying)
        {
            EditorUtility.SetDirty(uniqueId);
            EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
        }
    }
}
