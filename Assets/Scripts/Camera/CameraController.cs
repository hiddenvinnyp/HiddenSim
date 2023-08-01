using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _speedX = 300f;
    CinemachineFreeLook _freeLookComponent;

    private void Awake()
    {
        _freeLookComponent = GetComponent<CinemachineFreeLook>();
    }

    private void Start()
    {
        _freeLookComponent.m_XAxis.m_MaxSpeed = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _freeLookComponent.m_XAxis.m_MaxSpeed = _speedX;
        }

        if (Input.GetMouseButtonUp(1))
        {
            _freeLookComponent.m_XAxis.m_MaxSpeed = 0;
        }
    }
}
