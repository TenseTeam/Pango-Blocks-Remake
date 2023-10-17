namespace ProjectPBR.ScriptableObjects
{
    using UnityEngine;
    using ProjectPBR.Level.Blocks;

    [CreateAssetMenu(menuName = "Level/Block")]
    public class BlockData : ScriptableObject
    {
        public BlockType Type;
        public BlockColliderData ColliderData;
        [Min(1)]
        public int UnitLength;
        public float zRotation;
        public Sprite Sprite;
    }
}