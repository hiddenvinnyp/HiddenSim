using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelName;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _starsPlace;

    [SerializeField] private Sprite _noStars;
    [SerializeField] private Sprite _oneStars;
    [SerializeField] private Sprite _twoStars;
    [SerializeField] private Sprite _threeStars;

    private string _sceneName;
    private IGameStateMachine _stateMachine;
    private bool _isOpened = false;
    private bool _isFirst;

    public void ApplyProperty(IGameStateMachine stateMachine, LevelStaticData levelData, LevelProgressData levelProgressData, bool isFirst)
    {
        _stateMachine = stateMachine;
        _levelName.text = levelData.name;
        _icon.sprite = levelData.Icon;
        _sceneName = levelData.SceneName;
        _starsPlace.sprite = _noStars;
        _isFirst = isFirst;
        if (levelProgressData.Dictionary.TryGetValue(levelData.name, out int score))
        {
            switch (score)
            {
                case 0:
                    _starsPlace.sprite = _noStars;
                    break;
                case 1:
                    _starsPlace.sprite = _oneStars;
                    break;
                case 2:
                    _starsPlace.sprite = _twoStars;
                    break;
                case 3:
                    _starsPlace.sprite = _threeStars;
                    break;
                default:
                    _starsPlace.sprite = _noStars;
                    break;
            }
        }

        // TODO
        if (score > 0 || _isFirst /*|| _isNextAfterLastOpened*/)
            _isOpened = true;

        if (_isOpened)
            gameObject.GetComponentInChildren<Button>().onClick.AddListener(OpenScene);
    }

    private void OpenScene()
    {
        _stateMachine.Enter<LoadLevelState, string>(_sceneName);
    }
}
