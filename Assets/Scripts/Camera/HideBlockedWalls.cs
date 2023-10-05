using UnityEngine;

public class HideBlockedWalls : MonoBehaviour
{
    public CharacterController CharacterController;
    private Camera _camera;
    private HideWall _wall;

    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterController == null) return;

        Vector3 direction = CharacterController.transform.position - _camera.transform.position;
        Ray ray = new Ray(_camera.transform.position, direction);

        RaycastHit hit;
        if (Physics.Linecast(_camera.transform.position, CharacterController.transform.position, out hit)
            && hit.collider.GetComponent<HideWall>())
        {
            _wall = hit.collider.GetComponent<HideWall>();
            _wall.MakeTransparent();
        }
        else
        {
            _wall?.MakeSolid();
        }
    }
}
