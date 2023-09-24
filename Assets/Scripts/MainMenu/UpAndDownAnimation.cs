using UnityEngine;
using DG.Tweening;

public class UpAndDownAnimation : MonoBehaviour
{
    [SerializeField] private float _animationTime;
    [SerializeField] private float _maxY;

    private void Start()
    {
        StartLoopAnimation();
    }

    private void OnEnable()
    {
        StartLoopAnimation();
    }

    private void OnDisable()
    {
        transform.DOPause();
    }

    private void OnDestroy()
    {
        DOTween.KillAll(transform);
    }

    private void StartLoopAnimation()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(-_maxY, -_maxY * 0.5f), transform.position.z);
        transform.DOMoveY(Random.Range(0, _maxY), _animationTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(Random.Range(0, 1f));
    }
}
