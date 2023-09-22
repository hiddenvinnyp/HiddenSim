using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class LevelStaticData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public string SceneName;
    public int HiddenAmount;
}