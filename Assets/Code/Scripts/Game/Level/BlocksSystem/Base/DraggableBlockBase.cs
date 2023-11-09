namespace ProjectPBR.Level.Blocks
{
    using UnityEngine;
    using VUDK.Features.Main.DragSystem;
    using VUDK.Patterns.Pooling;
    using VUDK.Patterns.Pooling.Interfaces;

    public abstract class DraggableBlockBase : BlockBase, IPooledObject, IDraggable
    {
        public Pool RelatedPool { get; private set; }

        /// <inheritdoc/>
        public virtual void OnStartDragObject()
        {
        }

        /// <inheritdoc/>
        public virtual void OnEndDragObject()
        {
        }

        /// <inheritdoc/>
        public Transform GetDragTransform() => transform;

        /// <inheritdoc/>
        public void AssociatePool(Pool associatedPool) => RelatedPool = associatedPool;

        /// <inheritdoc/>
        public virtual void Dispose() => RelatedPool.Dispose(gameObject);

        /// <inheritdoc/>
        public virtual void Clear()
        {
        }
    }
}
