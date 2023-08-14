using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    public string Name;
    [Range(0,3)] public int Score;
    public string SceneName;
}