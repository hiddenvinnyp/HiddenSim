using UnityEngine;

[RequireComponent(typeof(Openable))]
[RequireComponent(typeof(BoxCollider))]
public class TriggerOpenDoor : MonoBehaviour
{
    private Openable _openable;

    private void Start()
    {
        _openable = GetComponent<Openable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterAnimator>())
            _openable.Toggle(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterAnimator>())
            _openable.Toggle(true);
    }
}
