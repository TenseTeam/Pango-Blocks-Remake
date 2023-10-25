namespace ProjectPBR.Level.VFX
{
    using UnityEngine;
    using VUDK.Patterns.Pooling;

    [RequireComponent(typeof(ParticleSystem))]
    public class VFXObject : PooledObject
    {
        private ParticleSystem _particles;

        private void Awake()
        {
            TryGetComponent(out _particles);
        }

        private void Start()
        {
            _particles.Play();
            CancelInvoke(nameof(Dispose));

            if(!_particles.main.loop) // if the particle system is not looping, dispose it after the duratio
                Invoke(nameof(Dispose), _particles.main.duration);
        }
    }
}
