namespace ProjectPBR.ScriptableObjects
{
    using UnityEngine;
    using ProjectPBR.Level.Player.PlayerHandler.Blocks;

    [CreateAssetMenu(menuName = "Level/Block")]
    public class BlockData : ScriptableObject
    {
        public BlockType Type;
        [Min(1)]
        public int UnitLength;
        public float zRotation;
        public Sprite Sprite;
        public GameObject BlockPrefabBase;
    }
}