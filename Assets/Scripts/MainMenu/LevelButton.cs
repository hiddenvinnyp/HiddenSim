using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelNameText;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _starsPlace;
    [SerializeField] private Image _lockImage;

    [SerializeField] private Sprite _noStars;
    [SerializeField] private Sprite _oneStars;
    [SerializeField] private Sprite _twoStars;
    [SerializeField] private Sprite _threeStars;

    private string _levelName;
    private IGameStateMachine _stateMachine;
    private bool _isLocked;

    public void ApplyProperty(IGameStateMachine stateMachine, LevelStaticData levelStaticData, LevelData levelProgress, bool isLocked)
    {
        _stateMachine = stateMachine;
        _levelName = levelStaticData.Name;
        
        _levelNameText.text = _levelName;
        _icon.sprite = levelStaticData.Icon;
        _starsPlace.sprite = _noStars;
        _isLocked = isLocked;

        int stars = 0;
        //TODO проверить после добавления сохранения leveldata
        if (levelProgress != null)
        {
            stars = levelProgress.Score;

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

        if (!_isLocked)
        {
            _lockImage.gameObject.SetActive(false);
            gameObject.GetComponentInChildren<Button>().onClick.AddListener(OpenScene);
        }
    }

    private void OpenScene()
    {
        _stateMachine.Enter<LoadLevelState, string>(_levelName);
    }
}
