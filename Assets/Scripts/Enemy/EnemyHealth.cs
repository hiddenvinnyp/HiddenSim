using System;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class EnemyHealth : MonoBehaviour, IHealth
{
    public event Action HealthChanged;

    [SerializeField] EnemyAnimator _animator;
    [SerializeField] float _maxHealth;
    [SerializeField] float _currentHealth;

    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _animator.PlayTakeDamage();
        HealthChanged?.Invoke();
    }
}
