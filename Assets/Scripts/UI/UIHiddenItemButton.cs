using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHiddenItemButton : MonoBehaviour, IScriptableObjectProperty, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool IsFound = false;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _hint;
    [SerializeField] private GameObject _namePanel;
    [SerializeField] private GameObject _hintPanel;
    [SerializeField] private Image _found;
    [SerializeField] private ButtonSound _sound;
    private HiddenItem _prefs;

    public string Name => _prefs.Name;

    private void Start()
    {
        _found.enabled = IsFound;
        _namePanel.SetActive(false);
        _hintPanel.SetActive(false);
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
        _sound.OnClick();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _namePanel.SetActive(false);
        _hintPanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _namePanel.SetActive(true);
        _sound.OnHover();
    }

    public void OnItemFound()
    {
        IsFound = true;
        _found.enabled = true;
    }
}
