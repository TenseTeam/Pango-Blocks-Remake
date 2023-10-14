namespace ProjectPBR.Patterns.Factories
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.Pooling;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.ScriptableObjects;

    public static class BlocksFactory
    {
        public static BlockBase Create(BlockData blockData)
        {
            GameObject goBlock = MainManager.Ins.PoolsManager.Pools[PoolKeys.SquareBase].Get();

            if (goBlock.TryGetComponent(out BlockBase blockBase))
                blockBase.Init(blockData);

            return blockBase;
        }
    }
}
