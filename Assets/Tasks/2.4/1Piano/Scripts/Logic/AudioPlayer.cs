using UnityEngine;

namespace Piano
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;

        public void PlaySoundWithPitch(float pitch)
        {
            audioSource.pitch = pitch;
            audioSource.Play();
        }

        public void StopSound()
        {
            audioSource.Stop();
        }
    }
}