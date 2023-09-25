using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HiddenItemsService : IHiddenItemsService, ISavedProgress
{
    public event Action<string> FoundItem;

    private readonly IProgressService _progressService;
    private readonly IStaticDataService _staticData;
    private readonly ISaveLoadService _saveLoadService;
    private string _levelName;
    private string _sceneName;
    private List<HiddenItemData> _allHiddenItems = new List<HiddenItemData>();
    private Dictionary<string, bool> _selectedItemsIds = new Dictionary<string, bool>();

    public List<string> SelectedItemsIds => _selectedItemsIds.Keys.ToList();


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
        _allHiddenItems = _staticData.ForLevelSpawners(_sceneName).HiddenItems; // all findable items on level

        if (_progressService.Progress.LevelProgressData.Dictionary.TryGetValue(levelName, out LevelData levelData) && levelData.HiddenObjectDataDictionary != null) // сохраненные, если есть
        {
            foreach (var item in levelData.HiddenObjectDataDictionary.Dictionary)
            {
                _selectedItemsIds.Add(item.Key, item.Value);
            }
            if (_selectedItemsIds.Count == 0)
            {
                Debug.Log("tyt1");
                AddNewItemsForSearch();
            }
        }
        else
        {
            Debug.Log("tyt2");
            AddNewItemsForSearch();
        }
    }

    public bool IsItemInList(string id)
    {
        if (SelectedItemsIds.Contains(id))
        {
            //FoundItem?.Invoke(id);
            return true;
        }
        return false;
    }

    public bool IsItemFound(string id)
    {
        if (!IsItemInList(id)) return false;

        if (_progressService.Progress.LevelProgressData.Dictionary.TryGetValue(_levelName, out LevelData levelData)
            && levelData.HiddenObjectDataDictionary != null)
        {
            levelData.HiddenObjectDataDictionary.Dictionary.TryGetValue(id, out bool found);
            return found;
        }
        return false;
    }

    public HiddenItem GetProperty(string id) //find in static data
    {
        foreach (var item in _staticData.ForLevelSpawners(_sceneName).HiddenItems)
        {
            if (item.Id == id)
                return item.Prefs;
        }
        return null;
    }

    public bool TryGetFoundItemsAmount(out int foundAmount)
    {
        foundAmount = 0;

        if (_progressService.Progress.LevelProgressData.Dictionary.TryGetValue(_levelName, out LevelData levelProgress) && levelProgress.HiddenObjectDataDictionary != null)
        {
            foundAmount = levelProgress.HiddenObjectDataDictionary.Dictionary.Select(x => x.Value == true).ToList().Count;
            return true;
        }        
        return false;
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        progress.LevelProgressData.Dictionary.TryGetValue(_levelName, out LevelData levelProgress);
        levelProgress.HiddenObjectDataDictionary?.Dictionary.Clear();
        foreach (var item in _selectedItemsIds)
        {
            levelProgress.HiddenObjectDataDictionary.Dictionary.Add(item.Key, item.Value);
        }        
    }

    public void LoadProgress(PlayerProgress progress)
    {
        progress.LevelProgressData.Dictionary.TryGetValue(_levelName, out LevelData levelProgress);
        if (levelProgress.HiddenObjectDataDictionary != null)
        {
            foreach (var item in levelProgress.HiddenObjectDataDictionary.Dictionary)
            {
                _selectedItemsIds.Clear();
                _selectedItemsIds.Add(item.Key, item.Value);
            }
        } else 
        { 
            _selectedItemsIds.Clear();
            Debug.Log("tyt3");
            AddNewItemsForSearch();
        }
        FoundItem?.Invoke("");
    }

    public void SignTo(Findable item)
    {
        item.ItemSelected += TryFindItem;
    }

    private void TryFindItem(string id)
    {
        if (IsItemInList(id))
        {
            //TODO write to Progress
            FoundItem?.Invoke(id);
        }
    }

    private void AddNewItemsForSearch()
    {
        _selectedItemsIds.Clear();
        foreach (string item in SelectItemsForSearch(_allHiddenItems, _staticData.ForLevel(_levelName).HiddenAmount))
        {
            _selectedItemsIds.Add(item, false);
        }
    }

    private List<string> SelectItemsForSearch(List<HiddenItemData> hiddenItems, int amount)
    {
        List<string> items = new List<string>();
        for (int i = 0; i < ((hiddenItems.Count < amount) ? hiddenItems.Count : amount); i++)
        {
            if (hiddenItems.Count < amount)
            {
                items.Add(hiddenItems[i].Id);
            }
            else
            {
                //TODO rework: elements should not repeat
                int randomIndex = UnityEngine.Random.Range(0, hiddenItems.Count - 1);
                if (items.Contains(hiddenItems[randomIndex].Id))
                {
                    i--;
                    continue;
                } else
                    items.Add(hiddenItems[randomIndex].Id);
            }
        }
        return items;
    }
}