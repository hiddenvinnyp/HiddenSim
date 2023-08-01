using UnityEngine;

public class PlayerMove : MonoBehaviour
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
        _inputService = Game.InputService;
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
}
