using System;
using System.Collections;
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
    public void ExecuteAfterSound(Action method)
    {
        StartCoroutine(ExecuteAfterSoundRoutine(method));
    }

    public void Stop()
    {
        _audioSource.Stop();
    }

    private IEnumerator ExecuteAfterSoundRoutine(Action method)
    {
        Play();

        //yield return new WaitForSeconds(_sound.length);

        while (_audioSource.isPlaying)
        {
            yield return null;
        }
        
        method();
    }
}
