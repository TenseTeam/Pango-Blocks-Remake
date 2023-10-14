namespace VUDK.Patterns.Pooling
{
    using UnityEngine;
    using VUDK.Patterns.Pooling.Interfaces;

    /// <summary>
    /// A fundamental class for objects eligible for pooling.
    /// Inherit from this class or implement the <see cref="IPooledObject"/> interface to suit your requirements.
    /// </summary>
    public abstract class PooledObject : MonoBehaviour, IPooledObject
    {
        public Pool RelatedPool { get; private set; }

        public void AssociatePool(Pool associatedPool)
        {
            RelatedPool = associatedPool;
        }

        public void Dispose()
        {
            RelatedPool.Dispose(gameObject);
        }

        public virtual void Clear()
        {
        }
    }
}
