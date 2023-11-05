namespace ProjectPBR.Patterns.Factories
{
    using UnityEngine;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Data.SaveDatas.Enums;

    /// <summary>
    /// Factory for Game's Data
    /// </summary>
    public static class DataFactory
    {
        public static LevelData Create()
        {
            return new LevelData();
        }

        public static LevelKey Create(int stageIndex, int levelIndex, GameDifficulty difficulty)
        {
            return new LevelKey(stageIndex, levelIndex, difficulty);
        }

        public static ProfileData Create(string name, Color color, int profileIndex, GameDifficulty difficulty)
        {
            return new ProfileData(name, color, profileIndex, difficulty);
        }
    }
}
