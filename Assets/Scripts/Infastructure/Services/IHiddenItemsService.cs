using System;

public interface IHiddenItemsService : IService
{
    event Action<string> FoundItem;

    void InitHiddenItems(string sceneName);
    bool TryGetFoundItemsAmount(string sceneName, out int foundAmount);
}
