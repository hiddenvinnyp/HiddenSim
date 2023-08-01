using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{
    private const string initialPointTag = "InitialPoint";
    private const string characterPath = "Prefabs/Player";
    private const string HUDPath = "Prefabs/UI/HUD";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;

    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _curtain = curtain;
    }

    public void Enter(string sceneName)
    {
        _curtain.Show();
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {
        _curtain.Hide();
    }

    private void OnLoaded()
    {
        var initialPoint = GameObject.FindWithTag(initialPointTag);
        GameObject character = Instantiate(characterPath, initialPoint.transform.position);
        Instantiate(HUDPath);

        CamerFollow(character);
        _stateMachine.Enter<GameLoopState>();
    }

    private GameObject Instantiate(string path)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab);
    }

    private GameObject Instantiate(string path, Vector3 point)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab, point, Quaternion.identity);
    }

    private void CamerFollow(GameObject target)
    {
        Camera.main.GetComponent<CameraFollow>().Follow(target);
    }
}