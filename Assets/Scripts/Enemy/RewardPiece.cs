using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RewardPiece : MonoBehaviour
{
    [SerializeField] private GameObject _coin;
    [SerializeField] private GameObject _pickupFXPrefab;
    [SerializeField] private TextMeshPro _rewardText;
    [SerializeField] private GameObject _pickupPopup;

    private Reward _reward;
    private bool _picked;
    private WorldData _worldData;

    public void Construct(WorldData worldData) => _worldData = worldData;

    public void Initialize(Reward reward)
    {
        _reward = reward;
    }

    private void OnTriggerEnter(Collider other) => Pickup();

    private void Pickup()
    {
        if (_picked) return;

        _picked = true;

        UpdateWorldData();
        HideReward();
        PlayPickupFX();
        ShowText();

        StartCoroutine(StartDestroyTimer());
    }

    private void PlayPickupFX() => 
        Instantiate(_pickupFXPrefab, transform.position, Quaternion.identity);

    private void HideReward() => 
        _coin.SetActive(false);

    private void UpdateWorldData() => 
        _worldData.RewardData.Collect(_reward.Value);

    private IEnumerator StartDestroyTimer()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void ShowText()
    {
        _rewardText.text = _reward.Value.ToString();
        _pickupPopup.SetActive(true);
    }
}