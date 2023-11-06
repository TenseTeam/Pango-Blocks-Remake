namespace ProjectPBR.Level.Blocks
{
    using UnityEngine;
    using VUDK.Features.Main.DragSystem;
    using VUDK.Patterns.Pooling;
    using VUDK.Patterns.Pooling.Interfaces;

    public abstract class DraggableBlockBase : BlockBase, IPooledObject, IDraggable
    {
        public Pool RelatedPool { get; private set; }

        public virtual void OnDragObject()
        {
        }

        public virtual void OnEndDragObject()
        {
        }

        public Transform GetDragTransform() => transform;

        public void AssociatePool(Pool associatedPool) => RelatedPool = associatedPool;

        public virtual void Dispose() => RelatedPool.Dispose(gameObject);

        public virtual void Clear()
        {
        }
    }
}
