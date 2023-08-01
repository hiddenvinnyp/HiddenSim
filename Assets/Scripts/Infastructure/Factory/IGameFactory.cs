using UnityEngine;

public interface IGameFactory
{
    GameObject CreateCharacter(GameObject initialPoint);
    void CreateHud();
}
