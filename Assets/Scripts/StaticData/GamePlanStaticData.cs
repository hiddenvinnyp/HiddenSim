using UnityEngine;

[CreateAssetMenu(menuName = "StaticData/GamePlan", fileName = "GamePlan")]
public class GamePlanStaticData : ScriptableObject
{
    public EpisodeStaticData[] Episodes;
}
