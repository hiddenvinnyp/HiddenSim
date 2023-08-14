using System;
using UnityEngine;
using UnityEngine.AI;

public class AgentMoveToPlayer : MonoBehaviour
{
    private const float MinimalDistance = 1;
    [SerializeField] private NavMeshAgent _agent;

    private IGameFactory _gameFactory;
    private Transform _characterTransform;

    private void Start()
    {
        _gameFactory = AllServices.Container.Single<IGameFactory>();

        if (_gameFactory.CharacterGameObject != null)
            InitializeHeroTransform();
        else
            _gameFactory.CharacterCreated += OnHeroCreated;
    }

    private void Update()
    {
        if (_characterTransform != null && Vector3.Distance(_agent.transform.position, _characterTransform.position) >= MinimalDistance)
            _agent.destination = _characterTransform.position;
    }

    private void OnHeroCreated() => InitializeHeroTransform();

    private void InitializeHeroTransform() => _characterTransform = _gameFactory.CharacterGameObject.transform;
}
