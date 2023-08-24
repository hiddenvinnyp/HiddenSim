using System;

public interface IHealth
{
    float CurrentHealth { get; set; }
    float MaxHealth { get; set; }

    event Action HealthChanged;

    void TakeDamage(float damage);
}