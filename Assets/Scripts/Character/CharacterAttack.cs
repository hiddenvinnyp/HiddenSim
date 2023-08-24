using UnityEngine;

[RequireComponent (typeof(CharacterAnimator), typeof(CharacterController))]
public class CharacterAttack : MonoBehaviour, ISavedProgressReader
{
    [SerializeField] private CharacterAnimator _animator;
    [SerializeField] private CharacterController _characterController;

    private IInputService _input;
    private static int _layerMask;
    private CharacterWeaponStats _weaponStats;
    private Collider[] _hits = new Collider[3];
    private OnAttackAnimationEvent _onAttackAnimationEvent;

    private void Awake()
    {
        _input = AllServices.Container.Single<IInputService>();
        _layerMask = 1 << LayerMask.NameToLayer("Hittable");
    }

    private void Start()
    {
        _onAttackAnimationEvent = GetComponentInChildren<OnAttackAnimationEvent>();
        _onAttackAnimationEvent.Attack += OnAttack;
    }

    private void Update()
    {
        if (_input.IsAttackButtonUp() && !_animator.IsAttacking)
            _animator.PlayAttack();
    }

    private void OnDestroy()
    {
        _onAttackAnimationEvent.Attack -= OnAttack;
    }

    public void OnAttack()
    {
        if (_hits.Length == 0) return;
        for (int i = 0; i < Hit(); i++)
        {
            _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_weaponStats.Damage);
        }
    }

    private int Hit() =>
        Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _weaponStats.DamageRadius, _hits, _layerMask);

    private Vector3 StartPoint() =>
        new Vector3(transform.position.x, _characterController.center.y * 0.5f, transform.position.z);

    public void LoadProgress(PlayerProgress progress) => _weaponStats = progress.WeaponStats;
}
