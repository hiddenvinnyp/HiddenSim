using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private bool _isWalking;
    private NavMeshAgent _agent;
    private Camera _camera;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _agent.SetDestination(hit.point);
            }
        }

        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _isWalking = false;
        } else
        {
            _isWalking = true;
        }

        _animator.SetBool("IsWalking", _isWalking);
    }
}
