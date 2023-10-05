using System;
using System.Collections.Generic;

public interface IHiddenItemsService : IService
{
    List<string> SelectedItemsIds();

    event Action<string> FoundItem;

    HiddenItem GetProperty(string id);
    void InitHiddenItems(string sceneName);
    bool IsItemFound(string id);
    bool IsItemInList(string id);
    void SignTo(Findable item);
    bool TryGetFoundItemsAmount(out int foundAmount);
}
