namespace ProjectPBR.Patterns.Factories
{
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Data.SaveDatas.Enums;

    /// <summary>
    /// Factory for Game's Data
    /// </summary>
    public static class DataFactory
    {
        public static LevelData CreateLevelData(int levelNumber, LevelDifficulty difficulty)
        {
            return new LevelData(levelNumber, difficulty);
        }

        public static LevelKey CreateLevelKey(int saveIndex, LevelDifficulty difficulty)
        {
            return new LevelKey(saveIndex, difficulty);
        }

        public static ProfileData Create(string name, LevelDifficulty difficulty)
        {
            return new ProfileData(name, difficulty);
        }
    }
}
