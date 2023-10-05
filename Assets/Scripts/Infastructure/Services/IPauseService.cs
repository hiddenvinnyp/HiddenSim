using System;

public interface IPauseService : IService
{
    bool IsPaused { get; }

    event Action<bool> PauseStateChanged;

    void ChangePauseState();
    void Pause();
    void UnPause();
}