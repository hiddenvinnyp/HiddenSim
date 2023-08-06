using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] protected bool Interactable;

    private bool isFocuse = false;
    public bool IsFocuse => isFocuse;

    public UnityEvent OnClick;

    public event UnityAction<UIButton> PointerEnter;
    public event UnityAction<UIButton> PointerExit;
    public event UnityAction<UIButton> PointerClick;

    public virtual void SetFocuse()
    {
        if (!Interactable) return;

        isFocuse = true;
    }

    public virtual void SetUnFocuse()
    {
        if (!Interactable) return;

        isFocuse = false;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (!Interactable) return;

        PointerEnter?.Invoke(this);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (!Interactable) return;

        PointerExit?.Invoke(this);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (!Interactable) return;
        
        PointerClick?.Invoke(this);
        OnClick?.Invoke();
    }
}
