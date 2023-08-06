using UnityEngine;

[RequireComponent(typeof(OutlineMesh))]
public class OutlineOnOff : MonoBehaviour
{
    private OutlineMesh _outline;

    private void Start()
    {
        _outline = GetComponent<OutlineMesh>();
        _outline.enabled = false;
    }

    private void OnMouseEnter()
    {
        if(_outline != null)
            _outline.enabled = true;
    }

    private void OnMouseExit()
    {
        if (_outline != null)
            _outline.enabled = false;
    }
}
