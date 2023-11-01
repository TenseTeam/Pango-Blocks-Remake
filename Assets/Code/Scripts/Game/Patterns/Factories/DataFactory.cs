namespace ProjectPBR.Patterns.Factories
{
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Data.SaveDatas.Enums;

    /// <summary>
    /// Factory for Game's Data
    /// </summary>
    public static class DataFactory
    {
        public static LevelData CreateLevelData(int levelNumber)
        {
            return new LevelData(levelNumber);
        }

        public static LevelKey CreateLevelKey(int saveIndex, GameDifficulty difficulty)
        {
            return new LevelKey(saveIndex, difficulty);
        }

        public static ProfileData Create(string name, GameDifficulty difficulty)
        {
            return new ProfileData(name, difficulty);
        }
    }
}
