using System;
using UnityEngine;

[RequireComponent(typeof(Attack))]
public class CheckAttackRange : MonoBehaviour
{
    [SerializeField] private Attack _attack;
    [SerializeField] private TriggerObserver _triggerObserver;

    private void Start()
    {
        _triggerObserver.TriggerEnter += OnTriggerEnter;
        _triggerObserver.TriggerExit += OnTriggerExit;

        _attack.DisableAttack();
    }

    private void OnDestroy()
    {
        _triggerObserver.TriggerEnter -= OnTriggerEnter;
        _triggerObserver.TriggerExit -= OnTriggerExit;
    }

    private void OnTriggerExit(Collider collider)
    {
        _attack.DisableAttack();
    }

    private void OnTriggerEnter(Collider collider)
    {
        _attack.EnableAttack();
}
}
