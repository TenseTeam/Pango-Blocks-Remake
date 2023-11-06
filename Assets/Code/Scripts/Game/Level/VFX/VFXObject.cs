namespace ProjectPBR.Level.VFX
{
    using UnityEngine;
    using VUDK.Generic.Serializable;
    using VUDK.Patterns.Pooling;

    [RequireComponent(typeof(ParticleSystem))]
    public class VFXObject : PooledObject
    {
        private TimeDelay _waitDispose;
        private ParticleSystem _particles;

        private void Awake()
        {
            TryGetComponent(out _particles);
        }

        private void Start()
        {
            _particles.Play();

            if (!_particles.main.loop) // if the particle system is not looping, dispose it after the duration
            {
                _waitDispose = new TimeDelay(_particles.main.duration);
                _waitDispose.Start();
                _waitDispose.OnCompleted += Dispose;
            }
        }

        private void Update() => _waitDispose?.Process();
    }
}
