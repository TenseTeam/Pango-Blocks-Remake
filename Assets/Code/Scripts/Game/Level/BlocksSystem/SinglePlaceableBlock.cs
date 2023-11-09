namespace ProjectPBR.Level.Blocks
{
    using ProjectPBR.Data.ScriptableObjects.Blocks;
    using ProjectPBR.Managers.Main.GameStats;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;

    [RequireComponent(typeof(PolygonCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class SinglePlaceableBlock : PlaceableBlockBase, ICastGameStats<GameStats>
    {
        private SpriteRenderer _sprite;

        public PolygonCollider2D Collider { get; protected set; }

        public new SingleBlockData Data => base.Data as SingleBlockData;
        public GameStats GameStats => MainManager.Ins.GameStats as GameStats;

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out _sprite);
            TryGetComponent(out PolygonCollider2D collider);
            Collider = collider;
        }

        /// <inheritdoc/>
        public override void Init(BlockDataBase data)
        {
            base.Init(data);
            _sprite.sprite = Data.Sprite;
            Collider.points = Data.ColliderData.Points;
            BlockType = Data.BlockType;
        }

        /// <inheritdoc/>
        public override void EnableCollider()
        {
            Collider.enabled = true;
        }

        /// <inheritdoc/>
        public override void DisableCollider()
        {
            Collider.enabled = false;
        }

        /// <inheritdoc/>
        public override void Clear()
        {
            base.Clear();
            _sprite.sprite = null;
            Collider.points = null;
        }

        /// <inheritdoc/>
        public override void IncreaseRender()
        {
            _sprite.sortingOrder = GameStats.PlacingBlockLayer;
        }

        /// <inheritdoc/>
        public override void DecreaseRender()
        {
            _sprite.sortingOrder = GameStats.PlacedBlockLayer;
        }
    }
}
