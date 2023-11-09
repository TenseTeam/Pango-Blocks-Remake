namespace ProjectPBR.Data.ScriptableObjects.Blocks
{
    using ProjectPBR.Level.Blocks;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Level/Blocks/Single Block")]
    public class SingleBlockData : BlockDataBase
    {
        public BlockType BlockType;
        public BlockColliderData ColliderData;
        public Sprite Sprite;
    }
}