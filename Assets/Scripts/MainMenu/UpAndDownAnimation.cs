using UnityEngine;
using DG.Tweening;

public class UpAndDownAnimation : MonoBehaviour
{
    [SerializeField] private float _animationTime;
    [SerializeField] private float _maxY;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(-_maxY, _maxY), transform.position.z);
        transform.DOMoveY(Random.Range(-_maxY, _maxY), _animationTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
