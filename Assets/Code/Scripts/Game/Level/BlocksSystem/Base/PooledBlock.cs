namespace ProjectPBR.Level.Blocks
{
    using VUDK.Patterns.Pooling;
    using VUDK.Patterns.Pooling.Interfaces;

    public abstract class PooledBlock : BlockBase, IPooledObject
    {
        public Pool RelatedPool { get; private set; }

        public void AssociatePool(Pool associatedPool)
        {
            RelatedPool = associatedPool;
        }

        public virtual void Dispose()
        {
            RelatedPool.Dispose(gameObject);
        }

        public virtual void Clear()
        {
        }
    }
}
