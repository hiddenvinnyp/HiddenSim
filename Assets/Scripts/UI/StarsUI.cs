using UnityEngine;

public class StarsUI : MonoBehaviour
{
    [SerializeField] private UIHPBar _bar;

    private IHiddenItemsService _hiddenItemsService;
    private int _hiddenAmount = 0;
    private int _foundObjectsAmount;

    public void Construct(IHiddenItemsService hiddenItemsService)
    {
        _hiddenItemsService = hiddenItemsService;
        _hiddenAmount = _hiddenItemsService.SelectedItemsIds.Count;
        _foundObjectsAmount = _hiddenItemsService.TryGetFoundItemsAmount(out int foundAmount) ? foundAmount : 0;
        UpdateHPBar("");
        _hiddenItemsService.FoundItem += UpdateHPBar;
    }

    private void OnDestroy()
    {
        _hiddenItemsService.FoundItem -= UpdateHPBar;
    }

    private void UpdateHPBar(string id)
    {
        _foundObjectsAmount = _hiddenItemsService.TryGetFoundItemsAmount(out int foundAmount) ? foundAmount : 0;
        _bar.SetValue(_foundObjectsAmount, _hiddenAmount);
    }

}