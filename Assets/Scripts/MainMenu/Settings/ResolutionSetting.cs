using UnityEngine;

[CreateAssetMenu(menuName = "Settings/ResolutionSetting")]
public class ResolutionSetting : Setting
{
    private Resolution[] resolutions = null;
    private Vector2Int[] availableResolutions = new Vector2Int[]
    {
        new Vector2Int(800, 600),
        new Vector2Int(1200, 720),
        new Vector2Int(1600, 900),
        new Vector2Int(1920, 1080),
    };

    private int currentResolurionIndex = 0;
    public override bool IsMinValue { get => currentResolurionIndex == 0; }
    public override bool IsMaxValue { get => currentResolurionIndex == resolutions.Length - 1; }

    public override void SetNextValue()
    {
        if (!IsMaxValue)
            currentResolurionIndex++;
    }

    public override void SetPreviousValue()
    {
        if (!IsMinValue)
            currentResolurionIndex--;
    }

    public override object GetValue()
    {
        return resolutions[currentResolurionIndex];
    }

    public override string GetStringValue()
    {
        if (resolutions == null) UpdateResolutions();
        return $"{resolutions[currentResolurionIndex].width} x {resolutions[currentResolurionIndex].height}";
    }

    public override void Apply()
    {
        if (resolutions == null) UpdateResolutions();
        Screen.SetResolution(resolutions[currentResolurionIndex].width, resolutions[currentResolurionIndex].height, true);
        Save();
    }

    public override void Load()
    {
        if (resolutions == null) UpdateResolutions();
        currentResolurionIndex = PlayerPrefs.GetInt(title, resolutions.Length - 1);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(title, currentResolurionIndex);
    }

    private void UpdateResolutions()
    {
        resolutions = Screen.resolutions;
    }
}
