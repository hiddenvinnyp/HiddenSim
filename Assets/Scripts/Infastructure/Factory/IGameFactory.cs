using System.Collections.Generic;
using UnityEngine;

public interface IGameFactory : IService
{
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }

    void Cleanup();
    GameObject CreateCharacter(GameObject initialPoint);
    void CreateHud();
}
