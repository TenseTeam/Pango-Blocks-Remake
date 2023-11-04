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

        public new SingleBlockData Data => base.Data as SingleBlockData;
        public PolygonCollider2D Collider { get; protected set; }

        public GameStats GameStats => MainManager.Ins.GameStats as GameStats;

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
            _sprite.sortingOrder = GameStats.PlacingBlockLayer;
        }

        public override void DecreaseRender()
        {
            _sprite.sortingOrder = GameStats.PlacedBlockLayer;
        }
    }
}
