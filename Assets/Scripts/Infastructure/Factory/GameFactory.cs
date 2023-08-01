using UnityEngine;

public class GameFactory
{
    private readonly IAssetProvider _assets;
    public GameFactory(IAssetProvider assets)
    {
        _assets = assets;
    }

    public GameObject CreateCharacter(GameObject initialPoint) =>
        _assets.Instantiate(AssetPath.CharacterPath, initialPoint.transform.position);

    public void CreateHud() =>
        _assets.Instantiate(AssetPath.HUDPath);
}