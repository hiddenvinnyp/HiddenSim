using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHiddenItemButton : MonoBehaviour, IScriptableObjectProperty, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _hint;
    [SerializeField] private GameObject _namePanel;
    [SerializeField] private GameObject _hintPanel;
    [SerializeField] private Image _found;
    private HiddenItem _prefs;

    public string Name => _prefs.Name;

    private void Start()
    {
        _found.enabled = false;
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
        print("apply props to " + Name);
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

    public void OnItemFound()
    {
        _found.enabled = true;
    }
}
