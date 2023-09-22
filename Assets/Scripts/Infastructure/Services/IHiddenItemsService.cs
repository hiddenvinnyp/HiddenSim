using System;
using System.Collections.Generic;

public interface IHiddenItemsService : IService
{
    List<string> SelectedItemsIds { get; }

    event Action<string> FoundItem;

    HiddenItem GetProperty(string id);
    void InitHiddenItems(string sceneName);
    bool TryGetFoundItemsAmount(string sceneName, out int foundAmount);
}
