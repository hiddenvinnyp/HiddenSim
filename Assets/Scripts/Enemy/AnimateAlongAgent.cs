using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyAnimator))]
public class AnimateAlongAgent : MonoBehaviour
{
    [SerializeField] private float MinVelocity = 0.01f;
    
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyAnimator _animator;

    private void Update()
    {
        if (ShouldMove())
            _animator.Move();
        else
            _animator.StopMoving();
    }

    private bool ShouldMove() => 
        _agent.velocity.magnitude > MinVelocity && _agent.remainingDistance > _agent.radius;
}
