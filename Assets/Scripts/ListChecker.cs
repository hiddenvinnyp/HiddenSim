using System.Collections.Generic;
using UnityEngine;

public class ListChecker : MonoBehaviour
{
    [SerializeField] private CharacterMovement _character;
    [SerializeField] private HiddenItemsListGenerator _listGenerator;
    [SerializeField] private RectTransform _buttonContainer;

    private UIHiddenItemButton[] _buttons;
    private List<Selectable> _items;

    private void Start()
    {
        _character.ItemReached += OnItemReached;
        _items = _listGenerator.HiddenItemsForSearch();
        _buttons = _buttonContainer.GetComponentsInChildren<UIHiddenItemButton>();
        print(_buttons.Length);
    }

    private void OnDestroy()
    {
        _character.ItemReached -= OnItemReached;
    }

    private void OnItemReached(Selectable item)
    {
        if (_items.Contains(item))
        {
            //найти нужную кнопку в контейнере и вызвать метод OnItemFound()
            foreach (UIHiddenItemButton button in _buttons)
            {
                print(button.Name);
                if (button.Name == item.Name)
                {
                    button.OnItemFound();
                    return;
                }
            }
        }
    }
}
