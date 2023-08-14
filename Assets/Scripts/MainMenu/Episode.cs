using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Episode", menuName = "Scriptable Objects/Episode")]
public class Episode : ScriptableObject
{
    public string Name;
    public GameObject EpisodeVisualModel;
    public string EpisodeScene;
    public Level[] Levels;
    public Episode NextEpisode;
}
