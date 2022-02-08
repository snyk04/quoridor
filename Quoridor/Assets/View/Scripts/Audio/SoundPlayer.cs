using System;
using System.Collections;
using UnityEngine;

namespace Quoridor.View.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        protected AudioSource _audioSource;

        protected virtual void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play()
        {
            _audioSource.Play();
        }
        public void Stop()
        {
            _audioSource.Stop();
        }
        
        public void ExecuteAfterSound(Action method)
        {
            StartCoroutine(ExecuteAfterSoundRoutine(method));
        }
        private IEnumerator ExecuteAfterSoundRoutine(Action method)
        {
            Play();
            
            while (_audioSource.isPlaying)
            {
                yield return null;
            }
        
            method();
        }
    }
}
