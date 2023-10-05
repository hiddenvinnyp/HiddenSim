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
    private readonly IWindowService _windowService;
    private string _levelName;
    private string _sceneName;
    private List<HiddenItemData> _allHiddenItems = new List<HiddenItemData>();
    private Dictionary<string, bool> _selectedItemsIds = new Dictionary<string, bool>();
    private int _score;

    public List<string> SelectedItemsIds() => _selectedItemsIds.Keys.ToList();


    public HiddenItemsService(IProgressService progressService, IStaticDataService staticDataService, ISaveLoadService saveLoadService, IWindowService windowService)
    {
        _progressService = progressService;
        _staticData = staticDataService;
        _saveLoadService = saveLoadService;
        _windowService = windowService;
    }

    public void InitHiddenItems(string levelName)
    {
        _levelName = levelName;
        _sceneName = _staticData.ForLevel(levelName).SceneName;
        _allHiddenItems = _staticData.ForLevelSpawners(_sceneName).HiddenItems; // all findable items on level

        if (_progressService.Progress.LevelProgressData.Dictionary.TryGetValue(levelName, out LevelData levelData) && levelData.HiddenObjectDataDictionary != null) // сохраненные, если есть
        {
            bool isAllItemsFound = false;

            _selectedItemsIds.Clear();
            foreach (var item in levelData.HiddenObjectDataDictionary.Dictionary)
            {
                Debug.Log("Write keys&value from Progress");
                _selectedItemsIds.Add(item.Key, item.Value);
            }

            if (_selectedItemsIds.Values.Count(value => value == true) == _selectedItemsIds.Keys.Count)
                isAllItemsFound = true;

            if (_selectedItemsIds.Count == 0 || isAllItemsFound)
            {
                Debug.Log("Dict is empty. Add new items");
                AddNewItemsForSearch();                
            }
        }
        else
        {
            Debug.Log("No HiddenObjectDataDictionary. Add new");
            AddNewItemsForSearch();            
        }
        _saveLoadService.SaveProgress();
        Debug.Log("InitHiddenItems" + string.Join(", ", SelectedItemsIds()));
    }

    public bool IsItemInList(string id)
    {
        if (SelectedItemsIds().Contains(id))
        {
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
            foundAmount = levelProgress.HiddenObjectDataDictionary.Dictionary.Values.Count(value => value == true);
            return true;
        }        
        return false;
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        progress.LevelProgressData.Dictionary.TryGetValue(_levelName, out LevelData levelProgress);
        levelProgress.Score = _score;
        levelProgress.HiddenObjectDataDictionary?.Dictionary.Clear();
        foreach (var item in _selectedItemsIds)
        {
            levelProgress.HiddenObjectDataDictionary.Dictionary.Add(item.Key, item.Value);
            Debug.Log($"Update Progress: save {item.Key} - {item.Value}");
        }
        Debug.Log("UpdateProgress" + string.Join(", ", SelectedItemsIds()));
    }

    public void LoadProgress(PlayerProgress progress)
    {        
        progress.LevelProgressData.Dictionary.TryGetValue(_levelName, out LevelData levelProgress);
        _score = levelProgress.Score;
        Debug.Log("Score " + _score);
        if (levelProgress.HiddenObjectDataDictionary != null)
        {
            _selectedItemsIds.Clear();
            foreach (var item in levelProgress.HiddenObjectDataDictionary.Dictionary)
            {
                _selectedItemsIds.Add(item.Key, item.Value);
                Debug.Log($"LOADPROGRESS: add {item.Key} - {item.Value}");
            }
        } else 
        { 
            _selectedItemsIds.Clear();
            Debug.Log("Load Progress is failed");
            AddNewItemsForSearch();
        }
        Debug.Log("LoadProgress" + string.Join(", ", SelectedItemsIds()));
    }

    public void SignTo(Findable item) => 
        item.ItemSelected += TryFindItem;

    private void OnLevelCompleted() => 
        _windowService.Open(WindowId.LevelCompleted);

    private void TryFindItem(string id)
    {
        Debug.Log("TRYFIND " + id);
        if (IsItemInList(id))
        {
            //TODO write to Progress
            _selectedItemsIds[id] = true;
            _saveLoadService.SaveProgress();
            ScoreUpdate();
            FoundItem?.Invoke(id);
        }
        Debug.Log("TryFindItem" + string.Join(", ", SelectedItemsIds()));
    }

    private void ScoreUpdate()
    {
        _saveLoadService.LoadProgress();
        float foundItems = TryGetFoundItemsAmount(out int foundAmount) ? foundAmount : 0;

        Debug.Log($"{foundItems} / {SelectedItemsIds().Count}");
        float rank = foundItems / SelectedItemsIds().Count;
        if (rank >= 0.95f)
        {
            _score = 3;
            OnLevelCompleted();
        }
        else if (rank >= 0.7f)
            _score = 2;
        else if (rank >= 0.4f)
            _score = 1;
        else
            _score = 0;
        _saveLoadService.SaveProgress();
        Debug.Log($"Score {_score}. Rank {rank}");
    }

    private void AddNewItemsForSearch()
    {
        _selectedItemsIds.Clear();
        foreach (string item in SelectItemsForSearch(_allHiddenItems, _staticData.ForLevel(_levelName).HiddenAmount))
        {
            _selectedItemsIds.Add(item, false);
        }
        Debug.Log("AddNewItemsForSearch" + string.Join(", ", SelectedItemsIds()));
    }

    private List<string> SelectItemsForSearch(List<HiddenItemData> hiddenItems, int amount)
    {
        List<string> items = new List<string>();

        if (hiddenItems.Count < amount)
        {
            for (int i = 0; i < hiddenItems.Count; i++)
            {
                items.Add(hiddenItems[i].Id);
            }
        }
        else
        {
            for (int i = 0; i < amount; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, hiddenItems.Count - 1);
                if (items.Contains(hiddenItems[randomIndex].Id))
                {
                    i--;
                    continue;
                }
                else
                    items.Add(hiddenItems[randomIndex].Id);
            }
        }
        Debug.Log("SelectItemsForSearch" + string.Join(", ", SelectedItemsIds()));
        return items;
    }
}