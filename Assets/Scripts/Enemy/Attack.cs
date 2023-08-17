
using System;
using System.Linq;
using UnityEngine;

[RequireComponent (typeof(EnemyAnimator))]
public class Attack : MonoBehaviour
{
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private float _attackCooldown = 3f;
    [SerializeField] private float _cleavage = 0.5f;
    [SerializeField] private float _effectiveDistance = 0.5f;

    private IGameFactory _gameFactory;
    private Transform _characterTransform;
    private float _currentAttackCooldown;
    private bool _isAttacking;
    private int _layerMask;
    private Collider[] _hits = new Collider[1];
    private bool _attackIsActive;

    public void DisableAttack() => _attackIsActive = false;

    public void EnableAttack() => _attackIsActive = true;

    private void Awake()
    {
        _gameFactory = AllServices.Container.Single<IGameFactory>();

        _layerMask = 1 << LayerMask.NameToLayer("Player");
        _gameFactory.CharacterCreated += OnCharacterCreated;
    }

    private void Update()
    {
        if (!CooldownIsUp())
            _currentAttackCooldown -= Time.deltaTime;

        if (_attackIsActive && !_isAttacking && CooldownIsUp())
            StartAttack();
    }

    private void OnAttack() 
    { 
        if (Hit(out Collider hit))
        {
            PhysicsDebug.DrawDebug(StartPoint(), _cleavage, 2);
            Debug.Log("hit");
        }
    }

    private void OnAttackEnded()
    {
        _currentAttackCooldown = _attackCooldown;
        _isAttacking = false;
    }

    private bool Hit(out Collider hit)
    {
        int hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), _cleavage, _hits, _layerMask);

        hit = _hits.FirstOrDefault();
        return hitCount > 0;
    }

    private Vector3 StartPoint()
    {
        return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * _effectiveDistance;
    }

    private bool CooldownIsUp() => _currentAttackCooldown <= 0;

    private void StartAttack()
    {
        transform.LookAt(_characterTransform);
        _animator.PlayAttack();

        _isAttacking = true;
    }

    private void OnCharacterCreated()
    {
        _characterTransform = _gameFactory.CharacterGameObject.transform;
    }
}