using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EpisodeClicked : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private EpisodeHex _episodeHex;
    [SerializeField] private GameObject _levelMenuPrefab;
    [SerializeField] private GameObject _levelPrefab;
    [SerializeField] private UpAndDownAnimation _animation;
    [SerializeField] private float _cameraSpeed = 3f;
    private Camera _camera;
    private Vector3 _cameraPosition;
    private Quaternion _cameraRotation;
    private bool _isSelected = false;
    private GameObject _levelMenu;

    void Start()
    {
        _camera = Camera.main;
        _cameraPosition = _camera.transform.position;
        _cameraRotation = _camera.transform.rotation;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isSelected) return; 

        _isSelected = true;
        Debug.Log("click " + gameObject.name);
        _animation.enabled = false;

        _camera.transform.DOMove(transform.position + new Vector3(-5f,3f,2f), _cameraSpeed);
        _camera.transform.DOLookAt(transform.position, _cameraSpeed);

        if (_levelMenu == null)
        {
            _levelMenu = Instantiate(_levelMenuPrefab);
            _levelMenu.GetComponentInChildren<Button>().onClick.AddListener(EpisodeMenuExit);
            int previousScore = 0;
            foreach(var level in _episodeHex.Levels)
            {
                var _levelButton = Instantiate(_levelPrefab);
                _levelButton.transform.SetParent(_levelMenu.GetComponentInChildren<LayoutGroup>().transform, false);
                _episodeHex.ProgressData.Dictionary.TryGetValue(level.Name, out LevelData levelData);
                _levelButton.GetComponentInChildren<LevelButton>().ApplyProperty(_episodeHex.StateMachine, level, levelData, (_episodeHex.IsFirst && level == _episodeHex.Levels[0]), previousScore > 0);
                print(levelData);
                previousScore = levelData.Score;
            }
        }
        else
            _levelMenu.SetActive(true);
    }

    public void EpisodeMenuExit()
    {
        _isSelected = false;
        _levelMenu.SetActive(false);
        Debug.Log("exit " + gameObject.name);
        _animation.enabled = true;
        _camera.transform.DOMove(_cameraPosition, _cameraSpeed);
        _camera.transform.DOLocalRotateQuaternion(_cameraRotation, _cameraSpeed);
    }
}
