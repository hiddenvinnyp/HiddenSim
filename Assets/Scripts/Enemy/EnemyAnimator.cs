using System;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
{
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int Win = Animator.StringToHash("Win");
    private static readonly int Attack = Animator.StringToHash("Attack_1");

    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _attackStateHash = Animator.StringToHash("Attack_1");
    private readonly int _walkingStateHash = Animator.StringToHash("Walk");
    private readonly int _deathStateHash = Animator.StringToHash("Death");

    private Animator _animator;

    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;

    public AnimatorState State { get; private set; }

    private void Awake() => _animator = GetComponent<Animator>();

    public void PlayTakeDamage() => _animator.SetTrigger(TakeDamage);
    public void PlayDeath() => _animator.SetTrigger(Die);

    public void Move() => _animator.SetBool(IsMoving, true);
    public void StopMoving() => _animator.SetBool(IsMoving, false);
    public void PlayAttack() => _animator.SetTrigger(Attack);

    public void EnteredState(int stateHash)
    {
        State = StateFor(stateHash);
        StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash) =>
        StateExited?.Invoke(StateFor(stateHash));

    private AnimatorState StateFor(int stateHash)
    {
        AnimatorState state;
        if (stateHash == _idleStateHash)
            state = AnimatorState.Idle;
        else if (stateHash == _attackStateHash)
            state = AnimatorState.Attack;
        else if (stateHash == _walkingStateHash)
            state = AnimatorState.Walking;
        else if (stateHash == _deathStateHash)
            state = AnimatorState.Died;
        else
            state = AnimatorState.Unknown;

        return state;
    }
}
