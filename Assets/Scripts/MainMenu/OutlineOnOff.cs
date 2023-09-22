using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(OutlineMesh))]
public class OutlineOnOff : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private OutlineMesh _outline;

    private void Start()
    {
        _outline = GetComponent<OutlineMesh>();
        _outline.enabled = false;
    }

    //private void OnMouseEnter()
    //{
    //    if(_outline != null)
    //        _outline.enabled = true;
    //}

    //private void OnMouseExit()
    //{
    //    if (_outline != null)
    //        _outline.enabled = false;
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_outline != null)
            _outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_outline != null)
            _outline.enabled = false;
    }
}
