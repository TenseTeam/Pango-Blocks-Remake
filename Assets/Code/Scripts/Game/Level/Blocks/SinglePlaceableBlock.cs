namespace ProjectPBR.Level.Blocks
{
    using ProjectPBR.ScriptableObjects;
    using UnityEngine;

    [RequireComponent(typeof(PolygonCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class SinglePlaceableBlock : PlaceableBlock
    {
        public new SingleBlockData Data { get; private set; }
        protected new PolygonCollider2D Collider;

        private SpriteRenderer _sprite;

        protected override void Awake()
        {
            base.Awake();
            Collider = base.Collider as PolygonCollider2D;
            TryGetComponent(out _sprite);
        }

        public override void Init(BlockData data)
        {
            Data = data as SingleBlockData;
            _sprite.sprite = Data.Sprite;
            Collider.points = Data.ColliderData.Points;
            BlockType = Data.BlockType;
        }

        public override void EnableCollider()
        {
            Collider.enabled = true;
        }

        public override void DisableCollider()
        {
            Collider.enabled = false;
        }

        public override void Clear()
        {
            base.Clear();
            _sprite.sprite = null;
            Collider.points = null;
        }
    }
}
