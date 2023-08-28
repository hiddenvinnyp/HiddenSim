using System;

[Serializable]
public class RewardData
{
    public int Collected;
    public Action Changed;

    internal void Collect(int value)
    {
        Collected += value;
        Changed?.Invoke();
    }
}