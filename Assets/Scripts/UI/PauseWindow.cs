using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : WindowBase
{
    [SerializeField] private AudioMixerFloatSetting _audioMixerSetting;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private TextMeshProUGUI _musicText;
    private bool _isMusicOn;
    private ISaveLoadService _saveLoadService;

    public event Action GamePaused;
    public event Action GameUnpaused;

    protected override void OnAwake()
    {
        base.OnAwake();
        _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        GamePaused?.Invoke();
        CloseButton.onClick.AddListener(Unpaused);
        _isMusicOn = _toggle.isOn;
    }

    public void ToggleMusic()
    {
        _isMusicOn = !_isMusicOn;

        if (_isMusicOn)
        {
            _musicText.text = "Music on";
            _audioMixerSetting.TurnOn();
        }
        else
        {
            _musicText.text = "Music off";
            _audioMixerSetting.TurnOff();
        }
    }

    public void LoadMainMenu()
    {
        // TODO: не сохраняются спавны врагов
        _saveLoadService.SaveProgress();
        StateMachine.Enter<LoadMenuState>();
    }

    private void Unpaused()
    {
        GameUnpaused?.Invoke();
    }
}