using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour, ISavedProgress, IHealth
{
    [SerializeField] private CharacterAnimator _animator;
    private CharacterState _state;

    public event Action HealthChanged;
    public float CurrentHealth
    {
        get => _state.CurrentHP;
        set
        {
            if (_state.CurrentHP != value)
            {
                _state.CurrentHP = value;
                HealthChanged?.Invoke();
            }
        }
    }
    public float MaxHealth 
    {
        get => _state.MaxHP;
        set => _state.MaxHP = MaxHealth;
    }

    public void LoadProgress(PlayerProgress progress)
    {
        _state = progress.CharacterState;
        HealthChanged?.Invoke();
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        progress.CharacterState.CurrentHP = CurrentHealth;
        progress.CharacterState.MaxHP = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (CurrentHealth <= 0) return;

        CurrentHealth -= damage;
        _animator.PlayHit();
    }
}
