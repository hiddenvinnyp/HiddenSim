using UnityEngine;

public class RewardSpawner : MonoBehaviour
{
    [SerializeField] private EnemyDeath _enemyDeath;
    private IGameFactory _factory;
    private int _rewardMin;
    private int _rewardMax;

    public void Construct(IGameFactory factory)
    {
        _factory = factory;

    }

    public void SetReward(int min, int max) 
    { 
        _rewardMin = min;
        _rewardMax = max;
    }

    private void Start()
    {
        _enemyDeath.DeathHappend += SpawnReward;
    }

    private void SpawnReward()
    {
        RewardPiece reward = _factory.CreateReward();
        reward.transform.position = transform.position;

        var rewardItem = new Reward
        {
            Value = UnityEngine.Random.Range(_rewardMax, _rewardMin)
        };

        reward.Initialize(rewardItem);
    }
}
