namespace ProjectPBR.Level.Player.PlayerHandler.Blocks
{
    using ProjectPBR.Level.Player.PlayerHandler.Interfaces;
    using ProjectPBR.ScriptableObjects;
    using UnityEngine;

    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public abstract class BlockBase : MonoBehaviour, IBlock
    {
        public BlockData BlockData { get; private set; }
        public Collider2D Collider { get; private set; }

        private SpriteRenderer _sprite;

        protected virtual void Awake()
        {
            TryGetComponent(out Collider2D _collider);
            TryGetComponent(out _sprite);

            Collider = _collider;
        }

        public void Init(BlockData data)
        {
            BlockData = data;
            transform.rotation = Quaternion.Euler(0, 0, data.zRotation);
            _sprite.sprite = data.Sprite;
        }
    }
}