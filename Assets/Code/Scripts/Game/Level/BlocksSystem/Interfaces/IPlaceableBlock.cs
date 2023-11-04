namespace ProjectPBR.Level.Blocks.Interfaces
{
    using ProjectPBR.Data.ScriptableObjects.Blocks;

    public interface IPlaceableBlock
    {
        public void Init(BlockData data);

        public void Place();
    }
}