using System.Collections;
using TMPro;
using UnityEngine;

public class RewardPiece : MonoBehaviour, ISavedProgress
{
    [SerializeField] private GameObject _coin;
    [SerializeField] private GameObject _pickupFXPrefab;
    [SerializeField] private TextMeshPro _rewardText;
    [SerializeField] private GameObject _pickupPopup;

    private Reward _reward;
    private bool _picked;
    private WorldData _worldData;
    private string _id;
    private bool _loadedFromProgress;

    public void Construct(WorldData worldData) => _worldData = worldData;

    public void Initialize(Reward reward)
    {
        _reward = reward;
    }

    private void Start()
    {
        if (!_loadedFromProgress) 
            _id = GetComponent<UniqueID>().Id;
    }
    private void OnTriggerEnter(Collider other) => Pickup();

    public void UpdateProgress(PlayerProgress progress)
    {
        if (_picked) return;

        RewardPieceDataDictionary rewardPieceDataDictionary = progress.WorldData.RewardData.RewardPiecesOnScene;

        if (!rewardPieceDataDictionary.Dictionary.ContainsKey(_id))
            rewardPieceDataDictionary.Dictionary.Add(_id, new RewardPieceData(transform.position.AsVectorData(), _reward));
    }

    public void LoadProgress(PlayerProgress progress)
    {
        _id = GetComponent<UniqueID>().Id;

        RewardPieceData data = progress.WorldData.RewardData.RewardPiecesOnScene.Dictionary[_id];
        Initialize(data.Reward);
        transform.position = data.Position.AsUnityVector();
        _loadedFromProgress = true;
    }

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

    private void UpdateWorldData()
    {
        _worldData.RewardData.Collect(_reward.Value);

        RemoveLootPieceFromSavedPieces();
    }

    private void RemoveLootPieceFromSavedPieces()
    {
        RewardPieceDataDictionary savedLootPieces = _worldData.RewardData.RewardPiecesOnScene;

        if (savedLootPieces.Dictionary.ContainsKey(_id))
            savedLootPieces.Dictionary.Remove(_id);
    }

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
