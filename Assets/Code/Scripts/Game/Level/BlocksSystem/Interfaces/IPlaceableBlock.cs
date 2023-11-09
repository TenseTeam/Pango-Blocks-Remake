namespace ProjectPBR.Level.Blocks.Interfaces
{
    using ProjectPBR.Data.ScriptableObjects.Blocks;

    public interface IPlaceableBlock
    {
        /// <summary>
        /// Initializes the block by its data.
        /// </summary>
        /// <param name="data"><see cref="BlockDataBase"/> of the block.</param>
        public void Init(BlockDataBase data);

        /// <summary>
        /// On block placed.
        /// </summary>
        public void OnPlace();
    }
}