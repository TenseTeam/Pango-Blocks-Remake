namespace ProjectPBR.Patterns.Factories
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.Pooling;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.ScriptableObjects;

    public static class BlocksFactory
    {
        public static PlaceableBlock Create(PlaceableBlockData blockData)
        {
            GameObject goBlock = MainManager.Ins.PoolsManager.Pools[PoolKeys.BlockBase].Get();

            if (goBlock.TryGetComponent(out PlaceableBlock blockBase))
                blockBase.Init(blockData);

            return blockBase;
        }
    }
}
