using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    private CharacterController _characterController;
    private IInputService _inputService;
    private Camera _camera;
    private Vector3 _velocity;
    private bool _isGrounded;
    private float _gravityValue = -9.81f;
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
        //_isGrounded = _characterController.isGrounded;
        //if (_isGrounded && _velocity.y < 0)
        //{
        //    _velocity.y = 0f;
        //}


        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //_characterController.Move(move * Time.deltaTime * _speed);

        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}

        //_velocity.y += _gravityValue * Time.deltaTime;
        //_characterController.Move(_velocity * Time.deltaTime);

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
