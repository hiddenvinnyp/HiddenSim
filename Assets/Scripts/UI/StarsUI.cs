using UnityEngine;

public class StarsUI : MonoBehaviour
{
    [SerializeField] private UIHPBar _bar;

    private IHiddenItemsService _hiddenItemsService;
    private int _hiddenAmount = 0;
    private int _foundObjectsAmount;

    public void Construct(IHiddenItemsService hiddenItemsService, int hiddenAmount, int found = 0)
    {
        _hiddenItemsService = hiddenItemsService;
        _hiddenAmount = hiddenAmount;
        _foundObjectsAmount = found;

        _hiddenItemsService.FoundItem += UpdateHPBar;
    }

    private void OnDestroy()
    {
        _hiddenItemsService.FoundItem -= UpdateHPBar;
    }

    private void UpdateHPBar(string id)
    {
        _bar.SetValue(_foundObjectsAmount, _hiddenAmount);
    }

}