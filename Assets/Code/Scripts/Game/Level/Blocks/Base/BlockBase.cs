namespace ProjectPBR.Level.Blocks
{
    using UnityEngine;

    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public abstract class BlockBase : MonoBehaviour
    {
        protected SpriteRenderer Sprite;

        [field: SerializeField]
        public BlockType BlockType { get; protected set; }

        public bool IsClimbable => BlockType == BlockType.Climbable;
        public bool IsSlideable => BlockType == BlockType.Slideable;

        protected virtual void Awake()
        {
            TryGetComponent(out Sprite);
        }
    }
}