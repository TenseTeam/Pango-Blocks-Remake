namespace ProjectPBR.ScriptableObjects
{
    using ProjectPBR.Level.Blocks;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Level/Block")]
    public class PlaceableBlockData : ScriptableObject
    {
        public BlockType BlockType;
        public BlockColliderData ColliderData;
        [Min(1)]
        public int UnitLength;
        public Sprite Sprite;
    }
}