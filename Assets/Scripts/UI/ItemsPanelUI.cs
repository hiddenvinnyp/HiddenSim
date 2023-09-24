using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPrefab;
    private IHiddenItemsService _hiddenItemsService;
    private Dictionary<string, UIHiddenItemButton> _buttons = new Dictionary<string, UIHiddenItemButton>();

    public void Construct(IHiddenItemsService hiddenItemsService)
    {
        _hiddenItemsService = hiddenItemsService;
        _hiddenItemsService.FoundItem += UpdateButton;

        SpawnItemButtons();
    }

    private void SpawnItemButtons()
    {
        foreach (string id in _hiddenItemsService.SelectedItemsIds)
        {
            HiddenItem property = _hiddenItemsService.GetProperty(id);
            GameObject button = Instantiate(_buttonPrefab, transform);
            UIHiddenItemButton buttonUI = button.GetComponent<UIHiddenItemButton>();
            buttonUI.ApplyProperty(property);
            buttonUI.IsFound = _hiddenItemsService.IsItemFound(id);
            _buttons.Add(id, buttonUI);
        }  
    }

    private void OnDestroy()
    {
        _hiddenItemsService.FoundItem -= UpdateButton;
    }

    private void UpdateButton(string id)
    {
        if(_buttons.TryGetValue(id, out UIHiddenItemButton button))
        {
            button.OnItemFound();
        }
    }
}
