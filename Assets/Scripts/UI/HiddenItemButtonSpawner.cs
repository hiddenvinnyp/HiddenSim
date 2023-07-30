using System.Collections.Generic;
using UnityEngine;

public class HiddenItemButtonSpawner : MonoBehaviour
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private HiddenItemsListGenerator _itemsListGenerator;

    private void Start()
    {
        GameObject[] allButtons = new GameObject[_container.childCount];

        for (int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i] = _container.GetChild(i).gameObject;
        }

        foreach (GameObject button in allButtons)
        {
            Destroy(button);
        }
        Debug.Log(_itemsListGenerator.HiddenItemsForSearch().Count);
        foreach (Selectable item in _itemsListGenerator.HiddenItemsForSearch())
        {
            GameObject button = Instantiate(_buttonPrefab, _container); // взять иконку, имя, подсказку
            IScriptableObjectProperty scriptableObjectProperty = button.GetComponent<IScriptableObjectProperty>();
            scriptableObjectProperty.ApplyProperty(item.Properties);
        }
    }
}
