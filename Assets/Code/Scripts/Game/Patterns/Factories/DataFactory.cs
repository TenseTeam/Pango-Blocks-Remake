namespace ProjectPBR.Patterns.Factories
{
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Data.SaveDatas.Enums;

    /// <summary>
    /// Factory for Game's Data
    /// </summary>
    public static class DataFactory
    {
        public static LevelData Create(int saveIndex, LevelDifficulty difficulty)
        {
            return new LevelData(saveIndex, difficulty);
        }

        public static ProfileData Create(string name)
        {
            return new ProfileData(name);
        }
    }
}
