using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Episode")]
public class Episode : ScriptableObject
{
    [SerializeField] private string _name;
    public string Name => _name;
    [SerializeField] private GameObject _episodeVisualModel;
    [SerializeField] private Scene _scene;
    [SerializeField] private Level[] _levels;
    [SerializeField] private Episode _nextEpisode;
}
