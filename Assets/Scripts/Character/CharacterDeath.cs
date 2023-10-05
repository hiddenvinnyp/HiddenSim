using System;
using UnityEngine;

[RequireComponent (typeof(CharacterHealth))]
public class CharacterDeath : MonoBehaviour
{
    [SerializeField] private CharacterHealth _health;
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private PlayerMove _move;
    [SerializeField] private CharacterAttack _attack;
    [SerializeField] private CharacterAnimator _animator;
    [SerializeField] private GameObject _deathFX;
    private bool _isDead;
    private IWindowService _windowService;

    private void Start()
    {
        _health.HealthChanged += OnHealthChanged;
        _windowService = AllServices.Container.Single<IWindowService>();
    }

    private void OnDestroy()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        if (!_isDead && _health.CurrentHealth <= 0)
            Die();
    }

    private void Die()
    {
        _isDead = true;
        _move.enabled = false;
        _movement.enabled = false;
        _attack.enabled = false;

        _animator.PlayDeath();

        Instantiate(_deathFX, transform.position, Quaternion.identity);

        _windowService.Open(WindowId.Defeat);
    }
}
