using System.Linq;
using UnityEngine;

[RequireComponent (typeof(EnemyAnimator))]
public class Attack : MonoBehaviour
{
    [SerializeField] private EnemyAnimator _animator;
    public float AttackCooldown = 3f;
    public float Cleavage = 0.5f;
    public float EffectiveDistance = 0.5f;
    public float Damage = 5f;
    [SerializeField] private GameObject _damageFX;
    [SerializeField] private AudioSource _damageSFX;
    [SerializeField] private AudioClip[] _hitSounds;

    private Transform _characterTransform;
    private float _currentAttackCooldown;
    private bool _isAttacking;
    private int _layerMask;
    private Collider[] _hits = new Collider[1];
    private bool _attackIsActive;

    public void DisableAttack() => _attackIsActive = false;

    public void EnableAttack() => _attackIsActive = true;

    public void Construct(Transform characterTransform)
    {
        _characterTransform = characterTransform;
    }

    private void Awake()
    {
        _layerMask = 1 << LayerMask.NameToLayer("Player");
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
            PhysicsDebug.DrawDebug(StartPoint(), Cleavage, 2);
            hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
        }
    }

    private void OnAttackEnded()
    {
        Instantiate(_damageFX, StartPoint(), Quaternion.identity);
        _damageSFX.PlayOneShot(_hitSounds[Random.Range(0, _hitSounds.Length)]);
        _currentAttackCooldown = AttackCooldown;
        _isAttacking = false;
    }

    private bool Hit(out Collider hit)
    {
        int hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);

        hit = _hits.FirstOrDefault();
        return hitCount > 0;
    }

    private Vector3 StartPoint()
    {
        return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * EffectiveDistance;
    }

    private bool CooldownIsUp() => _currentAttackCooldown <= 0;

    private void StartAttack()
    {
        transform.LookAt(_characterTransform);
        _animator.PlayAttack();
        _isAttacking = true;
    }
}