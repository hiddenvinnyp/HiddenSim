using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.EventSystems;

[RequireComponent (typeof(NavMeshAgent))]
public class CharacterMovement : MonoBehaviour
{
    //public event Action<Selectable> ItemReached;
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterAttack _attack;
    [SerializeField] private NavMeshAgent _agent;
    private bool _isWalking;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        //Selectable.ItemSelected += OnItemSelected;
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject) return;
        
        if (Input.GetMouseButton(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                NavMeshHit navHit;
                float maxDistance = 0.5f;
                while(!NavMesh.SamplePosition(hit.point, out navHit, maxDistance, NavMesh.AllAreas))
                {
                    maxDistance += 0.5f;
                } 
                _agent.SetDestination(navHit.position);       
                
                if (hit.collider.gameObject.GetComponent<Findable>())
                {
                }

                if (hit.collider.gameObject.GetComponentInParent<EnemyHealth>())
                {
                    //transform.LookAt(hit.point);
                    var lookDirection = hit.point - transform.position;

                    var lookOrientation = Quaternion.LookRotation(lookDirection);

                    transform.rotation = Quaternion.Lerp(transform.rotation, lookOrientation, 3f *
                    Time.deltaTime);
                    _attack.IsEnemy = true;
                } else
                    _attack.IsEnemy = false;
            }
        }

        if (_agent.remainingDistance <= _agent.stoppingDistance)        
            _isWalking = false;
         else        
            _isWalking = true;        

        _animator.SetBool("IsWalking", _isWalking);
    }

    private void OnDestroy()
    {
        //Selectable.ItemSelected -= OnItemSelected;
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
            yield return null;
        }
        Debug.Log("FINALLY HERE");
        //ItemReached?.Invoke(item);
    }
}