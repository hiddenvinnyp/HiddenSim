using System;
using System.Collections;
using UnityEngine;

public class Aggro : MonoBehaviour
{
    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private AgentMoveToPlayer _follow;
    [SerializeField] private float _cooldown;

    private Coroutine _aggroCouriutine;
    private bool _hasAggroTarget;

    private void Start()
    {
        _triggerObserver.TriggerEnter += OnTriggerEntered;
        _triggerObserver.TriggerExit += OnTriggerExited;

        _follow.enabled = false;
    }

    private void OnDestroy()
    {
        _triggerObserver.TriggerEnter -= OnTriggerEntered;
        _triggerObserver.TriggerExit -= OnTriggerExited;
    }

    private void OnTriggerExited(Collider collider)
    {
        if (_hasAggroTarget)
        {
            _hasAggroTarget = false;

            _aggroCouriutine = StartCoroutine(SwitchFollowOffAfterCooldown());
        }
    }

    private void OnTriggerEntered(Collider collider)
    {
        if (!_hasAggroTarget)
        {
            _hasAggroTarget = true;
            if (_aggroCouriutine != null)
            {
                StopCoroutine(_aggroCouriutine);
                _aggroCouriutine = null;
            }

            _follow.enabled = true;
        }
    }

    private IEnumerator SwitchFollowOffAfterCooldown()
    {
        yield return new WaitForSeconds(_cooldown);
        _follow.enabled = false;
    }
}
