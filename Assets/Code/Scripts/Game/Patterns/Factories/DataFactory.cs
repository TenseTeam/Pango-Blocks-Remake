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
        /// <summary>
        /// Creates a new <see cref="LevelData"/>
        /// </summary>
        /// <returns>A new <see cref="LevelData"/> instance.</returns>
        public static LevelData Create()
        {
            return new LevelData();
        }

        /// <summary>
        /// Creates a new <see cref="LevelKey"/>
        /// </summary>
        /// <param name="stageIndex">LevelKey stageIndex.</param>
        /// <param name="levelIndex">LevelKey levelIndex.</param>
        /// <param name="difficulty">LevelKey difficulty.</param>
        /// <returns>A new <see cref="LevelKey"/> instance.</returns>
        public static LevelKey Create(int stageIndex, int levelIndex, GameDifficulty difficulty)
        {
            return new LevelKey(stageIndex, levelIndex, difficulty);
        }

        /// <summary>
        /// Creates a new <see cref="ProfileData"/> instance with the specified attributes.
        /// </summary>
        /// <param name="name">The name of the new profile.</param>
        /// <param name="color">The color associated with the new profile.</param>
        /// <param name="profileIndex">The index of the new profile.</param>
        /// <param name="difficulty">The difficulty level of the new profile.</param>
        /// <returns>A new <see cref="ProfileData"/> instance.</returns>
        public static ProfileData Create(string name, Color color, int profileIndex, GameDifficulty difficulty)
        {
            return new ProfileData(name, color, profileIndex, difficulty);
        }
    }
}
