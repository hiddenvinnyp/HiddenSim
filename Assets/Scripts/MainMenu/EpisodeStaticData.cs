using UnityEngine;

[CreateAssetMenu(fileName = "Episode", menuName = "Scriptable Objects/Episode")]
public class EpisodeStaticData : ScriptableObject
{
    public string EpisodeName;
    public GameObject EpisodeVisualModel;
    public string EpisodeScene;
    public LevelStaticData[] Levels;
}
