using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _hover;
    [SerializeField] private AudioClip _click;

    public void OnClick()
    {
        _audioSource.PlayOneShot(_click);
    }

    public void OnHover()
    {
        _audioSource.PlayOneShot(_hover);
    }
}
