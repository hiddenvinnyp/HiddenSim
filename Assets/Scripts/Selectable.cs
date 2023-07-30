using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Outline))]
public class Selectable : MonoBehaviour
{
    [SerializeField] private HiddenItem _prefs;
    public HiddenItem Properties => _prefs;
    private string _name;

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
        Debug.Log(_name);
        _isFound = true;
    }

    private void OnMouseExit()
    {
        _outline.enabled = false;
    }

}
