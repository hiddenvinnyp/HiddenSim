using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private CinemachineFreeLook _camera;

    private void Start()
    {
        _camera = FindObjectOfType<CinemachineFreeLook>();
    }
    public void Follow(GameObject target)
    {
        Transform cameraTarget = target.GetComponent<CameraTarget>() == null ? target.transform : target.GetComponent<CameraTarget>().Target;
        _camera.m_Follow = cameraTarget;
        _camera.m_LookAt = cameraTarget;

        _camera.GetRig(2).m_LookAt = target.transform;
    }
}
