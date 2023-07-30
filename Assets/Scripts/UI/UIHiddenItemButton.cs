using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHiddenItemButton : MonoBehaviour, IScriptableObjectProperty, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private HiddenItem _prefs;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _hint;
    [SerializeField] private GameObject _namePanel;
    [SerializeField] private GameObject _hintPanel;

    private void Start()
    {
        _namePanel.SetActive(false);
        _hintPanel.SetActive(false);
        ApplyProperty(_prefs);
    }    

    public void ApplyProperty(ScriptableObject property)
    {
        if (property == null || property is HiddenItem == false) return;

        _prefs = property as HiddenItem;
        _icon.sprite = _prefs.Icon;
        _name.text = _prefs.Name;
        _hint.text = _prefs.Hint;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _hintPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _namePanel.SetActive(false);
        _hintPanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _namePanel.SetActive(true);
    }
}
