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

    protected override void OnAwake()
    {
        base.OnAwake();
        _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        
        CloseButton.onClick.AddListener(Unpaused);

        _toggle.isOn = true;
        if ((float)_audioMixerSetting.GetValue() == -80f)
        {
            _toggle.isOn = false;
            _musicText.text = "Music off";
            _audioMixerSetting.TurnOff();
        }

        _isMusicOn = _toggle.isOn;
    }

    private void Start()
    {
        Paused();
        
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
        _audioMixerSetting.Apply();
    }

    public void LoadMainMenu()
    {
        // TODO: не сохраняются спавны врагов
        Unpaused();
        _saveLoadService.SaveProgress();
        StateMachine.Enter<LoadMenuState>();
    }

    private void Unpaused()
    {
        PauseService.UnPause();
    }

    private void Paused()
    {
        PauseService.Pause();
    }
}