using UnityEngine;

[CreateAssetMenu(menuName = "Settings/GraphicsQualitySetting")]
public class GraphicsQualitySetting : Setting
{
    private int currentLevelIndex = 0;

    public override bool IsMinValue { get => currentLevelIndex == 0; }
    public override bool IsMaxValue { get => currentLevelIndex == QualitySettings.names.Length - 1; }

    public override void SetNextValue()
    {
        if (!IsMaxValue) currentLevelIndex++;
    }

    public override void SetPreviousValue()
    {
        if (!IsMinValue) currentLevelIndex--;
    }

    public override object GetValue()
    {
        return QualitySettings.names[currentLevelIndex];
    }

    public override string GetStringValue()
    {
        return QualitySettings.names[currentLevelIndex];
    }

    public override void Apply()
    {
        QualitySettings.SetQualityLevel(currentLevelIndex);
        Save();
    }

    public override void Load()
    {
        currentLevelIndex = PlayerPrefs.GetInt(title, QualitySettings.names.Length - 1);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(title, currentLevelIndex);
    }
}
