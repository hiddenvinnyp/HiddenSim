using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class LevelStaticData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    [Range(0,3)] public int Score;
    public string SceneName;
}