using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class OpenableSound : MonoBehaviour
{
    [SerializeField] private AudioClip _openSound;
    [SerializeField] private AudioClip _closeSound;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false; 
        _audioSource.loop = false;
    }

    public void OnOpen()
    {
        if (_openSound == null) return;
        _audioSource.PlayOneShot(_openSound);
    }

    public void OnClose()
    {
        if (_closeSound == null) return;
        _audioSource.PlayOneShot(_closeSound);
    }
}
