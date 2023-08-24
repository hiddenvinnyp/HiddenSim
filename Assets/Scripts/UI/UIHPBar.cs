using UnityEngine;
using UnityEngine.UI;

public class UIHPBar : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void SetValue(float current, float max) => 
        _image.fillAmount = current / max;
}