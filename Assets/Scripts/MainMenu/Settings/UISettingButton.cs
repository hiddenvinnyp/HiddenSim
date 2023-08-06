using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettingButton : UISelectableButton, IScriptableObjectProperty
{
    [SerializeField] private Setting setting;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private Image prevImage;
    [SerializeField] private Image nextImage;

    private void Start()
    {
        ApplyProperty(setting);
    }

    private void UpdateInfo()
    {
        titleText.text = setting.Title;
        valueText.text = setting.GetStringValue();

        prevImage.enabled = !setting.IsMinValue;
        nextImage.enabled = !setting.IsMaxValue;
    }

    public void SetNextValueSetting() 
    { 
        setting?.SetNextValue();
        setting?.Apply();
        UpdateInfo();
    }

    public void SetPreviousValueSetting() 
    { 
        setting?.SetPreviousValue();
        setting?.Apply();
        UpdateInfo();
    }

    public void ApplyProperty(ScriptableObject property)
    {
        if (property == null) return;

        if (property is Setting == false) return;
        setting = property as Setting;
        UpdateInfo();
    }
}
