namespace ProjectPBR.Factories
{
    using UnityEngine;
    using ProjectPBR.Level.Player.PlayerHandler.Blocks;
    using ProjectPBR.ScriptableObjects;

    public static class BlocksFactory
    {
        public static BlockBase Create(BlockData blockData)
        {
            GameObject.Instantiate(blockData.BlockPrefabBase).TryGetComponent(out BlockBase block);
            return block;
        }
    }
}
