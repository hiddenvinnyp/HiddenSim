using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HiddenItemsService : IHiddenItemsService
{
    public event Action<string> FoundItem;

    private readonly IProgressService _progressService;
    private readonly IStaticDataService _staticData;
    private readonly ISaveLoadService _saveLoadService;
    private string _levelName;
    private string _sceneName;
    private List<string> _selectedItemsIds = new List<string>();
    private List<HiddenItemData> hiddenItems = new List<HiddenItemData>();

    public List<string> SelectedItemsIds => _selectedItemsIds;


    public HiddenItemsService(IProgressService progressService, IStaticDataService staticDataService, ISaveLoadService saveLoadService)
    {
        _progressService = progressService;
        _staticData = staticDataService;
        _saveLoadService = saveLoadService;
    }

    public void InitHiddenItems(string levelName)
    {
        _levelName = levelName;
        _sceneName = _staticData.ForLevel(levelName).SceneName;
        hiddenItems = _staticData.ForLevelSpawners(_sceneName).HiddenItems; // все предметы, доступные для поиска
        
        if (_progressService.Progress.LevelProgressData.Dictionary.TryGetValue(levelName, out LevelData levelData) && levelData.HiddenObjectDataDictionary != null) // сохраненные, если есть
        {
            _selectedItemsIds = levelData.HiddenObjectDataDictionary.Dictionary.Select(x=> x.Key).ToList();
        }
        else
        {
            _selectedItemsIds = SelectItemsForSearch(hiddenItems, _staticData.ForLevel(levelName).HiddenAmount);
            SaveItems(_selectedItemsIds);
        }            
    }

    public bool IsItemInList(string id)
    {
        if (_selectedItemsIds.Contains(id))
        {
            FoundItem?.Invoke(id);
            Save(id);
            return true;
        }
        return false;
    }

    public bool IsItemFound(string id)
    {
        if (!IsItemInList(id)) return false;

        if (_progressService.Progress.LevelProgressData.Dictionary.TryGetValue(_levelName, out LevelData levelData))
        {
            levelData.HiddenObjectDataDictionary.Dictionary.TryGetValue(id, out bool found);
            return found;
        }
        return false;
    }

    public HiddenItem GetProperty(string id) //find in static data
    {
        Debug.Log("---------------");
        Debug.Log(_sceneName);
        Debug.Log(id);
        Debug.Log(_staticData.ForLevelSpawners(_sceneName));
        Debug.Log(_staticData.ForLevelSpawners(_sceneName).HiddenItems.Count);
        Debug.Log("---------------");
        foreach (var item in _staticData.ForLevelSpawners(_sceneName).HiddenItems)
        {
            if (item.Id == id)
                return item.Prefs;
        }
        return null;
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
        _saveLoadService.SaveProgress();
    }

    private void Save(string id)
    {
        Debug.Log("Need to save that " + id + " is found.");
    }

    private List<string> SelectItemsForSearch(List<HiddenItemData> hiddenItems, int amount)
    {
        List<string> items = new List<string>();
        for (int i = 0; i < ((hiddenItems.Count < amount) ? hiddenItems.Count : amount); i++)
        {
            if (hiddenItems.Count < amount)
            {
                items.Add(hiddenItems[i].Id);
            } else
                //TODO rework: elements should not repeat
                items.Add(hiddenItems[UnityEngine.Random.Range(0, hiddenItems.Count-1)].Id);
        }

        return items;
    }
}