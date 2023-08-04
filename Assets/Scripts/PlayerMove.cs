using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour, ISavedProgress
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    private CharacterController _characterController;
    private IInputService _inputService;
    private Camera _camera;
    private bool _isWalking;

    private void Awake()
    {        
        _characterController = GetComponent<CharacterController>();
        _inputService = AllServices.Container.Single<IInputService>();
    }

    private void Start()
    {
        _camera = Camera.main;        
    }

    void Update()
    {
        Vector3 _movementVector = Vector3.zero;

        if (_inputService.Axis.sqrMagnitude > 0.01f)
        {
            _movementVector = _camera.transform.TransformDirection(_inputService.Axis);
            _movementVector.y = 0;
            _movementVector.Normalize();

            transform.forward = _movementVector;
        }

        _movementVector += Physics.gravity;
        _characterController.Move(_movementVector * _speed * Time.deltaTime);

        if (_characterController.velocity.magnitude < 0.1f)
        {
            _isWalking = false;
        }
        else
        {
            _isWalking = true;
        }

        _animator.SetBool("IsWalking", _isWalking);
    }

    public void LoadProgress(PlayerProgress progress)
    {
        if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
        {
            Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
            if (savedPosition != null)              
                Warp(to: savedPosition);
        }
    }

    private void Warp(Vector3Data to)
    {
        _characterController.enabled = false;
        transform.position = to.AsUnityVector().AddY(_characterController.height);
        _characterController.enabled = true;
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
    }

    private static string CurrentLevel() =>
        SceneManager.GetActiveScene().name;
}
