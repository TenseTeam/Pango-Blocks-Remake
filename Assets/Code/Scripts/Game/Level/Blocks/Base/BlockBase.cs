namespace ProjectPBR.Level.Blocks
{
    using UnityEngine;

    [RequireComponent(typeof(Collider2D))]
    public abstract class BlockBase : MonoBehaviour
    {
        [field: SerializeField]
        public BlockType BlockType { get; protected set; }

        public bool IsClimbable => BlockType == BlockType.Climbable;
        public bool IsSlideable => BlockType == BlockType.Slideable;
        public bool IsFlat => BlockType == BlockType.Flat;
    }
}