using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public void ApplyProperty(LevelStaticData levelData)
    {
        _levelName.text = levelData.name;
        _icon.sprite = levelData.Icon;
        _sceneName = levelData.SceneName;

        switch (levelData.Score)
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

        gameObject.GetComponentInChildren<Button>().onClick.AddListener(OpenScene);
    }

    private void OpenScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
