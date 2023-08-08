using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Level")]
public class Level : ScriptableObject
{
    [SerializeField] private string _name;
    public string Name => _name;
    [SerializeField][Range(0,3)] private int _score;
    public int Score => _score;
    [SerializeField] private Scene _scene;
}