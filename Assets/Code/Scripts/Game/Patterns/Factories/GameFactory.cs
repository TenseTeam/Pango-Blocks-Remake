namespace ProjectPBR.Patterns.Factories
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.Pooling;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.Data.ScriptableObjects.Blocks;

    /// <summary>
    /// Factory for Game's GameObjects
    /// </summary>
    public static class GameFactory
    {
        /// <summary>
        /// Creates a new <see cref="PlaceableBlockBase"/> based on the provided <see cref="BlockDataBase"/>.
        /// </summary>
        /// <param name="blockData">The data defining the characteristics of the block.</param>
        /// <param name="isPartOfComplex">Specifies whether the block is part of a <see cref="ComplexPlaceableBlock"/>.</param>
        /// <returns>A new instance of <see cref="PlaceableBlockBase"/>.</returns>
        public static PlaceableBlockBase Create(BlockDataBase blockData, bool isPartOfComplex)
        {
            GameObject goBlock = null;

            if (blockData is ComplexBlockData)
                goBlock = MainManager.Ins.PoolsManager.Pools[PoolKeys.ComplexBlockBase].Get();

            if (blockData is SingleBlockData)
            {
                if (isPartOfComplex)
                    goBlock = MainManager.Ins.PoolsManager.Pools[PoolKeys.ComposedBlockBase].Get();
                else
                    goBlock = MainManager.Ins.PoolsManager.Pools[PoolKeys.SingleBlockBase].Get();
            }

            if (goBlock.TryGetComponent(out PlaceableBlockBase blockBase))
                blockBase.Init(blockData);

            return blockBase;
        }
    }
}
