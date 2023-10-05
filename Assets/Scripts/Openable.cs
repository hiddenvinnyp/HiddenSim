using DG.Tweening;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

[RequireComponent(typeof(OpenableSound))]
public class Openable : Selectable
{
    enum AnimationType {UpAndDown, RotateY, BackAndForth, LeftAndRight}

    [SerializeField] private AnimationType _type;
    [SerializeField] private float _endPoint;
    [SerializeField] private float _time;
    [SerializeField] private float _cleavage = 1f;

    private OpenableSound _sound;
    private bool _isOpen = false;
    private Vector3 _initialPosition;
    private Vector3 _initialRotation;
    //private Coroutine coroutine;
    private int _layerMask;
    private Collider[] _hits = new Collider[1];

    private void Start()
    {
        OnStart();

        _sound = GetComponent<OpenableSound>();

        _layerMask = 1 << LayerMask.NameToLayer("Player");

        if (_type == AnimationType.RotateY)
            _initialRotation = transform.rotation.eulerAngles;
        else
            _initialPosition = transform.position;
    }

    private void OnDestroy()
    {
        DOTween.KillAll(transform);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        /*if (coroutine == null)
            coroutine = StartCoroutine(WaitUntilCharacterReached());
        else
        {
            StopCoroutine(coroutine);
            coroutine = StartCoroutine(WaitUntilCharacterReached());
        }        */

        Toggle(_isOpen);
    }

    public void Toggle(bool isOpen)
    {
        if (isOpen)
        {
            _sound.OnClose();
            Close();
            _isOpen = false;
        }
        else
        {
            _sound.OnOpen();
            Open();
            _isOpen = true;
        }
    }

    private IEnumerator WaitUntilCharacterReached()
    {        
        while (!Hit(out Collider hit))
        {
            yield return null;
        }
        Toggle(_isOpen);
    }

    private bool Hit(out Collider hit)
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _cleavage, _hits, _layerMask);

        hit = _hits.FirstOrDefault();
        return hitCount > 0;
    }

    private void Open()
    {
        switch (_type)
        {
            case AnimationType.UpAndDown:
                transform.DOMoveY(_endPoint, _time);
                break;
            case AnimationType.RotateY:
                transform.DORotate(new Vector3(0, _endPoint, 0), _time);
                break;
            case AnimationType.BackAndForth:
                transform.DOMoveZ(_endPoint, _time);
                break;
            case AnimationType.LeftAndRight:
                transform.DOMoveX(_endPoint, _time);
                break;
            default:
                break;
        }
    }

    private void Close()
    {
        //transform.DORewind();
        switch (_type)
        {
            case AnimationType.UpAndDown:
                transform.DOMoveY(_initialPosition.y, _time);
                break;
            case AnimationType.RotateY:
                transform.DORotate(new Vector3(0, _initialRotation.y, 0), _time);
                break;
            case AnimationType.BackAndForth:
                transform.DOMoveZ(_initialPosition.z, _time);
                break;
            case AnimationType.LeftAndRight:
                transform.DOMoveX(_initialPosition.x, _time);
                break;
            default:
                break;
        }
    }
}
