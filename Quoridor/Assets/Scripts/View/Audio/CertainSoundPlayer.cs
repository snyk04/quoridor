using UnityEngine;

namespace Quoridor.View.Audio
{
    public sealed class CertainSoundPlayer : SoundPlayer
    {
        [SerializeField] private AudioClip _sound;

        protected override void Awake()
        {
            base.Awake();
            
            if (_sound == null)
            {
                Debug.LogError("No sound", this);
            }
            _audioSource.clip = _sound;
        }
    }
}
