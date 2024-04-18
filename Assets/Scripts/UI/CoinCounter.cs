using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counter;
    private WorldData _worldData;

    public void Construct(WorldData worldData)
    {
        _worldData = worldData;
        _worldData.RewardData.Changed += UpdateCounter;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
            _counter.text = _worldData.RewardData.Collected.ToString();
    }
}
