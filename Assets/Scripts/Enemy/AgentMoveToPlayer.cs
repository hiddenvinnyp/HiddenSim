using UnityEngine;
using UnityEngine.AI;

public class AgentMoveToPlayer : MonoBehaviour
{
    private const float MinimalDistance = 0.5f;
    [SerializeField] private NavMeshAgent _agent;

    private Transform _characterTransform;

    public void Construct(Transform characterTransform)
    {
        _characterTransform = characterTransform;
    }

    private void Update()
    {
        if (_characterTransform != null && Vector3.Distance(_agent.transform.position, _characterTransform.position) >= MinimalDistance)
            _agent.destination = _characterTransform.position;
    }
}
