using System;
using System.Collections.Generic;
using System.Linq;

public class HiddenItemsService : IHiddenItemsService
{
    public event Action<string> FoundItem;

    private readonly IProgressService _progressService;
    private readonly IStaticDataService _staticData;

    private List<string> _selectedItemsIds = new List<string>();

    public List<string> SelectedItemsIds => _selectedItemsIds;

    public HiddenItemsService(IProgressService progressService, IStaticDataService staticDataService)
    {
        _progressService = progressService;
        _staticData = staticDataService;
    }

    public void InitHiddenItems(string sceneName)
    {
        List<HiddenItemData> hiddenItems = _staticData.ForLevelSpawners(sceneName).HiddenItems; // все предметы, доступные для поиска
        
        if (_progressService.Progress.LevelProgressData.Dictionary.TryGetValue(sceneName, out LevelData levelData)) // сохраненные, если есть
        {
            _selectedItemsIds = levelData.HiddenObjectDataDictionary.Dictionary.Select(x=> x.Key).ToList();
        }
        else
        {
            _selectedItemsIds = SelectItemsForSearch(hiddenItems, _staticData.ForLevel(sceneName).HiddenAmount);
            SaveItems(_selectedItemsIds);
        }
            
    }

    public bool IsItemInList(string id)
    {
        if (_selectedItemsIds.Contains(id))
        {
            FoundItem?.Invoke(id);
            return true;
        }
        return false;
    }

    public bool IsItemFound(string id)
    {
        if (!IsItemInList(id)) return false;

        if ()
    }

    public bool TryGetFoundItemsAmount(string sceneName, out int foundAmount)
    {
        foundAmount = 0;

        if (_progressService.Progress.LevelProgressData.Dictionary.TryGetValue(sceneName, out LevelData levelProgress))
        {
            foundAmount = levelProgress.HiddenObjectDataDictionary.Dictionary.Select(x => x.Value == true).ToList().Count;
            return true;
        }
        
        return false;
    }

    private void SaveItems(List<string> selectedItemsIds)
    {
        throw new NotImplementedException();
    }

    private List<string> SelectItemsForSearch(List<HiddenItemData> hiddenItems, int amount)
    {
        Random rnd = new Random();
        List<string> items = new List<string>();
        for (int i = 0; i < ((hiddenItems.Count < amount) ? hiddenItems.Count : amount); i++)
        {
            items.Add(hiddenItems[rnd.Next(hiddenItems.Count)].Id);
        }

        return items;
    }
}