using UnityEngine;
using Random = UnityEngine.Random;

namespace Quoridor.View.Audio
{
    public class RandomSoundPlayer : SoundPlayer
    {
        [SerializeField] private AudioClip[] _sounds;

        public void PlayNext()
        {
            _audioSource.clip = _sounds[Random.Range(0, _sounds.Length)];
            Play();
        }
    }
}
