using System;
using UnityEngine;

public class ItemsPanelUI : MonoBehaviour
{
    private IHiddenItemsService _hiddenItemsService;

    public void Construct(IHiddenItemsService hiddenItemsService)
    {
        _hiddenItemsService = hiddenItemsService;
        _hiddenItemsService.FoundItem += UpdateButton;
    }
    private void OnDestroy()
    {
        _hiddenItemsService.FoundItem -= UpdateButton;
    }

    private void UpdateButton(string id)
    {
        throw new NotImplementedException();
    }
}
