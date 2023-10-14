namespace VUDK.Extensions.Audio
{
    using UnityEngine;
    using VUDK.Extensions.Audio.Interfaces;
    using VUDK.Patterns.Pooling;
    using VUDK.Patterns.Pooling.Interfaces;
    using VUDK.Extensions.Transform;

    [RequireComponent(typeof(AudioSource))]
    public class AudioSFX : MonoBehaviour, IPooledObject, IAudioSFX
    {
        private AudioSource _audioSource;

        public Pool RelatedPool { get; private set; }

        private void Awake()
        {
            TryGetComponent(out _audioSource);
        }

        public void Init(AudioClip clip)
        {
            _audioSource.clip = clip;
        }

        public void PlayClipAtPoint(Vector3 position)
        {
            _audioSource.Play();
            Invoke("Dispose", _audioSource.clip.length);
            transform.SetPosition(position);
        }

        public void AssociatePool(Pool associatedPool)
        {
            RelatedPool = associatedPool;
        }

        public void Clear()
        {
            _audioSource.Stop();
            _audioSource.clip = null;
        }

        public void Dispose()
        {
            RelatedPool.Dispose(gameObject);
        }
    }
}