using System;
using UnityEngine;

public class OnAttackAnimationEvent : MonoBehaviour
{
    public event Action Attack;

    public void OnAttack() => Attack?.Invoke();
}