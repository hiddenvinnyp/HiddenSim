using System;

[Serializable]
public class CharacterState
{
    public float CurrentHP;
    public float MaxHP;

    public void ResetHP() => CurrentHP = MaxHP;
}