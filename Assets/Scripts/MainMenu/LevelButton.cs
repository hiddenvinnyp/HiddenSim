using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelName;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _starsPlace;
    [SerializeField] private Image _lockImage;

    [SerializeField] private Sprite _noStars;
    [SerializeField] private Sprite _oneStars;
    [SerializeField] private Sprite _twoStars;
    [SerializeField] private Sprite _threeStars;

    private string _sceneName;
    private IGameStateMachine _stateMachine;
    private bool _isOpened = false;
    private bool _isFirst;
    private bool _isNextAfterLastOpened;

    public void ApplyProperty(IGameStateMachine stateMachine, LevelStaticData levelStaticData, LevelData levelProgress, bool isFirst, bool isNextAfterLastOpened = false)
    {
        _stateMachine = stateMachine;
        _levelName.text = levelStaticData.Name;
        _icon.sprite = levelStaticData.Icon;
        _sceneName = levelStaticData.SceneName;
        _starsPlace.sprite = _noStars;
        _isFirst = isFirst;
        _isNextAfterLastOpened = isNextAfterLastOpened;

        int stars = 0;
        //TODO проверить после добавления сохранения leveldata
        if (levelProgress != null)
        {
            stars = levelProgress.Score;
            print("stars " + stars);
            switch (stars)
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
                    stars = 0;
                    break;
            }
        }

        // TODO test _isNextAfterLastOpened after save/load levelProgress will be done
        if (stars > 0 || _isFirst || _isNextAfterLastOpened)
        {
            _isOpened = true;
            _lockImage.gameObject.SetActive(false);
        }

        if (_isOpened)
            gameObject.GetComponentInChildren<Button>().onClick.AddListener(OpenScene);
    }

    private void OpenScene()
    {
        _stateMachine.Enter<LoadLevelState, string>(_sceneName);
    }
}
