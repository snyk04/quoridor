using UnityEngine;
using Random = UnityEngine.Random;

namespace Quoridor.View.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class RandomSoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _sounds;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayNext()
        {
            _audioSource.clip = _sounds[Random.Range(0, _sounds.Length)];
            _audioSource.Play();
        }
    }
}
