using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Openable : MonoBehaviour, IPointerDownHandler
{
    enum AnimationType {UpAndDown, RotateY, BackAndForth}

    [SerializeField] private AnimationType _type;
    [SerializeField] private float _endPoint;
    [SerializeField] private float _time;

    private bool _isOpen = false;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    private void Start()
    {
        if (_type == AnimationType.RotateY)
            _initialRotation = transform.rotation;
        else
            _initialPosition = transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isOpen)
        {
            Close();
            _isOpen = false;
        }
        else
        {
            Open();
            _isOpen = true;
        }
    }

    private void Open()
    {
        switch (_type)
        {
            case AnimationType.UpAndDown:
                transform.DOMoveY(_endPoint, _time);
                break;
            case AnimationType.RotateY:
                transform.DOLocalRotate(_initialRotation.eulerAngles.AddY(_endPoint), _time);
                break;
            case AnimationType.BackAndForth:
                transform.DOMoveZ(_endPoint, _time);
                break;
            default:
                break;
        }
    }

    private void Close()
    {
        transform.DORewind();
    }
}
