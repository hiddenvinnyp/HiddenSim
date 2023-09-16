using TMPro;
using UnityEngine;

public class EpisodeHex : MonoBehaviour
{
    [HideInInspector] public string Name;
    [HideInInspector] public GameObject Model;
    [HideInInspector] public string MainScene;
    [HideInInspector] public LevelStaticData[] Levels;
    [HideInInspector] public EpisodeStaticData NextEpisode;

    [SerializeField] private TextMeshProUGUI _episodeNameText;
    [SerializeField] private Transform _modelParent;

    private LevelProgressData _progressData;

    public void Construct(LevelProgressData levelData)
    {
        _progressData = levelData;
        _episodeNameText.text = Name;
        GameObject model = Instantiate(Model, _modelParent);
        model.transform.SetParent(_modelParent, false);
    }
}
