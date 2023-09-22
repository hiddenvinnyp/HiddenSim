using UnityEngine;
using UnityEngine.UI;

public abstract class WindowBase : MonoBehaviour
{
    public Button CloseButton;
    protected IProgressService ProgressService;
    protected IGameStateMachine StateMachine;
    protected PlayerProgress Progress => ProgressService.Progress;


    public void Construct(IProgressService progressService, IGameStateMachine stateMachine)
    {
        ProgressService = progressService;
        StateMachine = stateMachine;
    }

    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        Initialize();
        SubscribeUpdates();
    }

    private void OnDestroy()
    {
        UnSubscribeUpdates();
    }

    protected virtual void OnAwake()
    {
        CloseButton.onClick.AddListener(() => Destroy(gameObject));
    }

    protected virtual void Initialize(){}
    protected virtual void SubscribeUpdates(){}
    protected virtual void UnSubscribeUpdates(){}
}

