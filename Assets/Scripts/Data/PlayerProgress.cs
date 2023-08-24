using System;

[Serializable]
public class PlayerProgress
{
    public CharacterState CharacterState;
    public WorldData WorldData;
    public CharacterWeaponStats WeaponStats;
    public KillData KillData;

    public PlayerProgress(string initialLevel)
    {
        WorldData = new WorldData(initialLevel);
        CharacterState = new CharacterState();
        WeaponStats = new CharacterWeaponStats();
        KillData = new KillData();
    }
}
