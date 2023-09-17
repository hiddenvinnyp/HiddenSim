using TMPro;
using UnityEngine;

public class EpisodeHex : MonoBehaviour
{
    [HideInInspector] public string Name;
    [HideInInspector] public GameObject Model;
    [HideInInspector] public string MainScene;
    [HideInInspector] public LevelStaticData[] Levels;
    [HideInInspector] public EpisodeStaticData NextEpisode;
    [HideInInspector] public LevelProgressData ProgressData;
    [HideInInspector] public IGameStateMachine StateMachine;
    [HideInInspector] public bool IsFirst;

    [SerializeField] private TextMeshProUGUI _episodeNameText;
    [SerializeField] private Transform _modelParent;


    public void Construct(LevelProgressData levelData, IGameStateMachine stateMachine)
    {
        StateMachine = stateMachine;
        ProgressData = levelData;
        _episodeNameText.text = Name;
        GameObject model = Instantiate(Model, _modelParent);
        model.transform.SetParent(_modelParent, false);

        // TODO: добавить проверку на последний доступный уровень, ведь у него тоже скор == 0
        if (!IsFirst || (levelData.Dictionary.TryGetValue(Levels[0].Name, out int score) && score == 0))
            GetComponent<HexagonLock>().Locked(true);
    }
}
