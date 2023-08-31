using UnityEngine;
using UnityEngine.UI;

public abstract class WindowBase : MonoBehaviour
{
    public Button CloseButton;

    private void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake()
    {
        CloseButton.onClick.AddListener(() => Destroy(gameObject));
    }
}

