using System.Collections.Generic;

public interface IHiddenItemsService : IService
{
    void InitHiddenItems(LevelProgressData levelProgressData, List<HiddenItemData> hiddenItems);
}

public class HiddenItemsService : IHiddenItemsService
{
    public void InitHiddenItems(LevelProgressData levelProgressData, List<HiddenItemData> hiddenItems)
    {
        throw new System.NotImplementedException();
    }
}