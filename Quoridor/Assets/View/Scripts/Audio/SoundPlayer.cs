using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _sound;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_sound == null)
        {
            Debug.LogError("No sound", this);
        }
        _audioSource.clip = _sound;
    }

    public void Play()
    {
        _audioSource.Play();
    }
    public void Stop()
    {
        _audioSource.Stop();
    }
}
