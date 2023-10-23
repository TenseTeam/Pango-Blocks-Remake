namespace ProjectPBR.Level.Blocks
{
    using ProjectPBR.ScriptableObjects;
    using UnityEngine;

    [RequireComponent(typeof(PolygonCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class SinglePlaceableBlock : PlaceableBlock
    {
        private SpriteRenderer _sprite;

        public new SingleBlockData Data => base.Data as SingleBlockData;
        public PolygonCollider2D Collider { get; protected set; }

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out _sprite);
            TryGetComponent(out PolygonCollider2D collider);
            Collider = collider;
        }

        public override void Init(BlockData data)
        {
            base.Init(data);
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

        public override void IncreaseRender()
        {
            _sprite.sortingOrder++;
        }

        public override void DecreaseRender()
        {
            _sprite.sortingOrder--;
        }
    }
}
