using System;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof(EnemyHealth), typeof(EnemyAnimator))]
public class EnemyDeath : MonoBehaviour
{
    public event Action DeathHappend;

    [SerializeField] private EnemyHealth _health;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private GameObject _deathFx;

    private void Start()
    {
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnDestroy()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        if (_health.CurrentHealth <= 0)
            Die();
    }

    private void Die()
    {
        _health.HealthChanged -= OnHealthChanged;
        _animator.PlayDeath();
        Instantiate(_deathFx, transform.position, Quaternion.identity);
        StartCoroutine(DestoyTimer());

        DeathHappend?.Invoke();
    }

    private IEnumerator DestoyTimer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
