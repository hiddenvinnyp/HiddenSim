using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent (typeof(OutlineMesh))]
public class Selectable : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static event UnityAction<Selectable> ItemSelected;
    private OutlineMesh _outline;


    private void Start()
    {
        OnStart();
    }

    public virtual void OnStart()
    {
        _outline = GetComponent<OutlineMesh>();
        _outline.enabled = false;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Click on " + name);
        ItemSelected?.Invoke(this);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        _outline.enabled = true;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        _outline.enabled = false;
    }
}
