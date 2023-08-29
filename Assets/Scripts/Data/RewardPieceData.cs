using System;

[Serializable]
public class RewardPieceData 
{
    public Vector3Data Position;
    public Reward Reward;

    public RewardPieceData(Vector3Data position, Reward reward)
    {
        Position = position;
        Reward = reward;
    }
}