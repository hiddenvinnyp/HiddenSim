using System.Collections.Generic;
using UnityEngine;

public class HiddenItemsListGenerator : MonoBehaviour
{
    [SerializeField] [Range(1, 20)] private int _hiddenItemAmount = 1; // teke it as LevelStaticData.HiddenAmount
    private Selectable[] _allHiddenItems;
    private List<Selectable> _hiddenItemsForSearch = new List<Selectable>();
    public List<Selectable> HiddenItemsForSearch() => _hiddenItemsForSearch.Count == 0? CreateHiddenItemsList() : _hiddenItemsForSearch;

    private void Start()
    {
        if (_hiddenItemsForSearch.Count == 0) // load LevelProgress.HiddenDictionary to _hiddenItemsForSearch
            CreateHiddenItemsList();
    }

    private List<Selectable> CreateHiddenItemsList()
    {
        _allHiddenItems = FindObjectsByType<Selectable>(FindObjectsSortMode.None);
        print(_allHiddenItems.Length);
        for (int i = 0; i < ((_allHiddenItems.Length < _hiddenItemAmount) ? _allHiddenItems.Length : _hiddenItemAmount); i++)
        {
            _hiddenItemsForSearch.Add(_allHiddenItems[i]);
        }
        return _hiddenItemsForSearch;
    }
}
