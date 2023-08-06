using UnityEngine;

public class UISelectableContainer : MonoBehaviour
{
    [SerializeField] private Transform buttonContainer;

    public bool Interactible = true;
    public void SetInteractible(bool interactible) => Interactible = interactible;

    private UISelectableButton[] buttons;
    private int selectButtonIndex = 0;

    public void SelectNext()
    {
        // сделать управление с клавиатуры
    }

    public void SelectPrevious()
    {

    }

    private void Start()
    {
        buttons = buttonContainer.GetComponentsInChildren<UISelectableButton>();

        if (buttons == null) Debug.LogError("Button list is empty!");

        foreach (UISelectableButton button in buttons)
        {
            button.PointerEnter += OnPointerEnter;
        }

        if (!Interactible) return;

        buttons[selectButtonIndex].SetFocuse();
    }

    private void OnDestroy()
    {
        if (buttons == null) return;
        foreach (UISelectableButton button in buttons)
        {
            button.PointerEnter -= OnPointerEnter;
        }
    }

    private void OnPointerEnter(UIButton button)
    {
        SelectButton(button);
    }

    private void SelectButton(UIButton button)
    {
        if (!Interactible) return;

        buttons[selectButtonIndex].SetUnFocuse();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (button == buttons[i])
            {
                selectButtonIndex = i;
                button.SetFocuse();
                break;
            }
        }
    }
}
