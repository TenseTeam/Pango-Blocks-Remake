namespace ProjectPBR.Level.PathSystem
{
    using UnityEngine;
    using ProjectPBR.Level.Blocks;

    [System.Serializable]
    public class Node
    {
        public BlockType BlockType;
        public Vector3 Position { get; private set; }

        public Node(Vector3 position, BlockType blockType)
        {
            Position = position;
            BlockType = blockType;
        }
    }
}
