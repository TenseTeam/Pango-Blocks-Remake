namespace ProjectPBR.Level.Blocks
{
    using UnityEngine;
    using VUDK.Patterns.Pooling;
    using ProjectPBR.Level.Blocks.Interfaces;
    using ProjectPBR.ScriptableObjects;

    /// <summary>
    /// Base class that provides flexibility for adding new types of blocks, if needed.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(PolygonCollider2D))]
    public abstract class BlockBase : PooledObject, IBlock
    {
        public BlockData BlockData { get; private set; }
        public PolygonCollider2D Collider { get; private set; }

        private SpriteRenderer _sprite;

        protected virtual void Awake()
        {
            TryGetComponent(out PolygonCollider2D _collider);
            TryGetComponent(out _sprite);
            Collider = _collider;
        }

        public void Init(BlockData data)
        {
            BlockData = data;
            transform.rotation = Quaternion.Euler(0, 0, data.zRotation);
            _sprite.sprite = data.Sprite;
            Collider.points = data.ColliderData.Points;
        }

        public override void Clear()
        {
            BlockData = null;
            _sprite.sprite = null;
            Collider.points = null;
            transform.rotation = Quaternion.identity;
        }
    }
}