using System;
using System.Collections.Generic;
using UnityEngine;

public interface IGameFactory : IService
{
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }

    GameObject CharacterGameObject { get; }
    event Action CharacterCreated;

    void Cleanup();
    GameObject CreateCharacter(GameObject initialPoint);
    GameObject CreateHud();

    public void Register(ISavedProgressReader progressReader) { }
}
