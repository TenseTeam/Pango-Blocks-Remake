namespace ProjectPBR.ScriptableObjects
{
    using ProjectPBR.Level.Blocks;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Level/Blocks/Single Block")]
    public class SingleBlockData : BlockData
    {
        public BlockType BlockType;
        public BlockColliderData ColliderData;
        public Sprite Sprite;
    }
}