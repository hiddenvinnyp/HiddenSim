using TMPro;
using UnityEngine;

public class EpisodeHex : MonoBehaviour
{
    [HideInInspector] public string Name;
    [HideInInspector] public GameObject Model;
    [HideInInspector] public string MainScene;
    [HideInInspector] public LevelStaticData[] Levels;
    [HideInInspector] public LevelProgressData ProgressData;
    [HideInInspector] public IGameStateMachine StateMachine;
    [HideInInspector] public bool IsLocked;

    [SerializeField] private TextMeshProUGUI _episodeNameText;
    [SerializeField] private Transform _modelParent;


    public void Construct(LevelProgressData levelData, IGameStateMachine stateMachine)
    {
        StateMachine = stateMachine;
        ProgressData = levelData;
        _episodeNameText.text = Name;
        GameObject model = Instantiate(Model, _modelParent);
        model.transform.SetParent(_modelParent, false);
        GetComponent<HexagonLock>().Locked(IsLocked);
    }
}
