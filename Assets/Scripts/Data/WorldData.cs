using System;

[Serializable]
public class WorldData
{
    public PositionOnLevel PositionOnLevel;
    public RewardData RewardData;

    public WorldData(string initialLevel)
    {
        PositionOnLevel = new PositionOnLevel(initialLevel);
        RewardData = new RewardData();
    }
}