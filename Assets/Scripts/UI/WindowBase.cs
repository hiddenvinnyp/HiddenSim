using UnityEngine;
using UnityEngine.UI;

public abstract class WindowBase : MonoBehaviour
{
    public Button CloseButton;
    protected IProgressService ProgressService;
    protected IGameStateMachine StateMachine;
    protected IPauseService PauseService;
    protected PlayerProgress Progress => ProgressService.Progress;
    protected string _levelName;

    public void Construct(IProgressService progressService, IGameStateMachine stateMachine, string levelName)
    {
        ProgressService = progressService;
        StateMachine = stateMachine;
        _levelName = levelName;
    }

    public void Construct(IProgressService progressService, IGameStateMachine stateMachine, IPauseService pauseService)
    {
        ProgressService = progressService;
        StateMachine = stateMachine;
        PauseService = pauseService;
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

