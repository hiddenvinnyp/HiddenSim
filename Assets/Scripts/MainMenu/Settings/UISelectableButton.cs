using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISelectableButton : UIButton
{
    [SerializeField] private Image selectImage;

    public UnityEvent OnSelect;
    public UnityEvent OnUnSelect;

    public override void SetFocuse()
    {
        base.SetFocuse();
        selectImage.enabled = true;
        OnSelect?.Invoke();
    }

    public override void SetUnFocuse()
    {
        base.SetUnFocuse();
        selectImage.enabled = false;
        OnUnSelect?.Invoke();
    }
}
