using System;

[Serializable]
public class RewardData
{
    public int Collected;
    public Action Changed;
    public RewardPieceDataDictionary RewardPiecesOnScene = new RewardPieceDataDictionary();

    public void Collect(int value)
    {
        Collected += value;
        Changed?.Invoke();
    }
}
