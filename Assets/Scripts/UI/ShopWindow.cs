using TMPro;
using UnityEngine;

public class ShopWindow : WindowBase
{
    [SerializeField] private TextMeshProUGUI _amountText;

    protected override void Initialize()
    {
        RefreshSkullText();
    }

    protected override void SubscribeUpdates()
    {
        Progress.WorldData.RewardData.Changed += RefreshSkullText;
    }

    protected override void UnSubscribeUpdates()
    {
        base.UnSubscribeUpdates();
        Progress.WorldData.RewardData.Changed -= RefreshSkullText;
    }

    private void RefreshSkullText() => 
        _amountText.text = Progress.WorldData.RewardData.Collected.ToString();
}
