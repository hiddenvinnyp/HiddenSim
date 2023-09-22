using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Settings/AudioMixerFloatSetting")]
public class AudioMixerFloatSetting : Setting
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string nameSetting;

    [SerializeField] private float minRealValue;
    [SerializeField] private float maxRealValue;

    [SerializeField] private float virtualStep;
    [SerializeField] private float minVirtualValue;
    [SerializeField] private float maxVirtualValue;

    private float currentValue = 0;
    private float bufferValue;

    public override bool IsMaxValue { get => currentValue == maxRealValue; }
    public override bool IsMinValue { get => currentValue == minRealValue; }

    public override void SetNextValue()
    {
        AddValue(Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
    }

    public override void SetPreviousValue()
    {
        AddValue(-Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
    }

    public override string GetStringValue()
    {
        return Mathf.Lerp(minVirtualValue, maxVirtualValue, (currentValue - minRealValue) / (maxRealValue - minRealValue)).ToString();
    }

    public override object GetValue()
    {
        return currentValue;
    }

    public override void Apply()
    {
        audioMixer.SetFloat(nameSetting, currentValue);
        Save();
    }

    public override void Load()
    {
        currentValue = PlayerPrefs.GetFloat(title, 0);
    }
    public void TurnOn()
    {
        currentValue = bufferValue;
        Apply();
    }

    public void TurnOff()
    {
        bufferValue = currentValue;
        currentValue = minRealValue;
        Apply();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(title, currentValue);
    }

    private void AddValue(float value)
    {
        currentValue += value;
        currentValue = Mathf.Clamp(currentValue, minRealValue, maxRealValue);
    }
}
