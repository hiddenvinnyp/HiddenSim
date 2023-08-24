using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent (typeof(NavMeshAgent))]
public class CharacterMovement : MonoBehaviour
{
    public event UnityAction<Selectable> ItemReached;
    [SerializeField] private Animator _animator;
    private bool _isWalking;
    private NavMeshAgent _agent;
    private Camera _camera;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _camera = Camera.main;
        Selectable.ItemSelected += OnItemSelected;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _agent.SetDestination(hit.point);
            }
        }

        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _isWalking = false;
        } else
        {
            _isWalking = true;
        }

        _animator.SetBool("IsWalking", _isWalking);
    }

    private void OnDestroy()
    {
        Selectable.ItemSelected -= OnItemSelected;
    }

    private Coroutine coroutine;
    private void OnItemSelected(Selectable item)
    {
        float time = (transform.position - item.transform.position).magnitude / _agent.speed;
        if (coroutine ==  null) 
            coroutine = StartCoroutine(WaitUntilReachedItem(item, time));
        else
        {
            StopCoroutine(coroutine);
            coroutine = StartCoroutine(WaitUntilReachedItem(item, time));
        } 
    }

    private IEnumerator WaitUntilReachedItem(Selectable item, float time)
    {
        float timer = 0;
        //while ((transform.position - item.transform.position).magnitude > _agent.radius + item.GetComponent<MeshFilter>().sharedMesh.bounds.size.x && timer <= 3f)
        while (timer <= time)
        {
            timer += Time.deltaTime;
            print(timer);
            yield return null;
        }
        Debug.Log("FINALLY HERE");
        ItemReached?.Invoke(item);
    }
}