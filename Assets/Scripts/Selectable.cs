using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Outline))]
public class Selectable : MonoBehaviour
{
    public static event UnityAction<Selectable> ItemSelected;

    [SerializeField] private HiddenItem _prefs;
    public HiddenItem Properties => _prefs;
    private string _name;
    public string Name => _prefs.Name;

    private Outline _outline;
    private bool _isFound = false;
    public bool IsFound => _isFound;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        _name = _prefs.name;
    }

    private void OnMouseEnter()
    {
        _outline.enabled = true;
    }

    private void OnMouseDown()
    {
        // Ждать, когда герой подойдет, включить анимацию захвата предмета
        // Удалять предмет, помечать найденным в UI списке
        Debug.Log("Click on " + _name);
        _isFound = true;
        ItemSelected?.Invoke(this);
    }

    private void OnMouseExit()
    {
        _outline.enabled = false;
    }

}
