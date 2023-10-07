namespace VUDK.Features.Main.WeaponSystem.Bullets
{
    using System;
    using UnityEngine;
    using VUDK.Features.Main.EntitySystem.Interfaces;
    using VUDK.Patterns.ObjectPool;
    using VUDK.Patterns.ObjectPool.Interfaces;

    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Bullet : MonoBehaviour, IPooledObject
    {
        [Header("Bullet settings")]
        public float Damage = 1f;
        public float Speed = 1f;

        [SerializeField, Header("Dispose")]
        protected float TimeBeforeDispose;

        protected Rigidbody Rigidbody;

        public Pool RelatedPool { get; private set; }

        /// <summary>
        /// Initializes this <see cref="Bullet"/>.
        /// </summary>
        /// <param name="damage">Bullet damage.</param>
        /// <param name="speed">Bullet speed.</param>
        public virtual void Init(float damage, float speed)
        {
            Damage = damage;
            Speed = speed;
        }

        protected virtual void Awake()
        {
            TryGetComponent(out Collider collider);
            TryGetComponent(out Rigidbody);
            collider.isTrigger = true;
        }

        protected virtual void OnEnable()
        {
            CancelInvoke();
            Invoke("Dispose", TimeBeforeDispose);
        }

        /// <summary>
        /// Starts moving the bullet.
        /// </summary>
        public virtual void ShootBullet()
        {
            MoveBullet();
        }

        /// <summary>
        /// Moves the bullet by its rigidbody velocity.
        /// </summary>
        protected virtual void MoveBullet()
        {
            Rigidbody.velocity = transform.forward * Speed;
        }

        public void AssociatePool(Pool associatedPool)
        {
            RelatedPool = associatedPool;
        }

        public virtual void Dispose()
        {
            RelatedPool.Dispose(gameObject);
        }

        public void Clear()
        {
            Damage = 0f;
            Speed = 0f;
            Rigidbody.velocity = Vector3.zero;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent(out IVulnerable ent))
                ent.TakeDamage(Damage);

            Dispose();
        }
    }
}
