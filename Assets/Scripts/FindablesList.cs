using UnityEngine;

public class FindablesList : MonoBehaviour
{
    private IHiddenItemsService _hiddenItemsService;
    private Findable[] _findables;

    private void Awake()
    {
        _hiddenItemsService = AllServices.Container.Single<IHiddenItemsService>();        
    }

    private void Start()
    {
        _findables = GetComponentsInChildren<Findable>();
        foreach (var item in _findables)
        {
            _hiddenItemsService.SignTo(item);
        }
    }
}
