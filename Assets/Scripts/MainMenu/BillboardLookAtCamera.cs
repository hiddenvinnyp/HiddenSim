using UnityEngine;

public class BillboardLookAtCamera : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _canvas.worldCamera = _camera;
    }

    private void LateUpdate()
    {
        if (_camera != null)
        {
            Quaternion cameraRotation = _camera.transform.rotation;

            _canvas.transform.LookAt(_canvas.transform.position + cameraRotation * Vector3.forward, cameraRotation * Vector3.up);
        }
    }
}
