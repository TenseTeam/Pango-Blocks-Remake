namespace ProjectPBR.Data.SaveDatas
{
    using System.Collections.Generic;
    using VUDK.Features.Main.SaveSystem;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.Patterns.Factories;

    [System.Serializable]
    public class ProfileData : SaveDataBase
    {
        public string ProfileName;
        public LevelDifficulty CurrentDifficulty;
        public Dictionary<LevelKey, LevelData> LevelsData;

        public ProfileData(string name, LevelDifficulty difficulty) : base()
        {
            LevelsData = new Dictionary<LevelKey, LevelData>();
            ProfileName = name;
            CurrentDifficulty = difficulty;

            for (int i = 0; i < GameConstants.ProfileSaving.NumberOfLevels; i++)
            {
                LevelsData.Add(
                    DataFactory.CreateLevelKey(i, LevelDifficulty.Easy),
                    DataFactory.CreateLevelData(i, LevelDifficulty.Easy));
                LevelsData.Add(
                    DataFactory.CreateLevelKey(i, LevelDifficulty.Hard),
                    DataFactory.CreateLevelData(i, LevelDifficulty.Hard));
            }
        }

        public override string ToString()
        {
            return $"Name: {ProfileName} ; Current Difficulty: {CurrentDifficulty} -- {ClearedLevelsToString()}";
        }

        private string ClearedLevelsToString()
        {
            string result = string.Empty;

            foreach (var level in LevelsData)
                result += $"\n[Key: {level.Key.SaveIndex} - {level.Key.Difficulty}] == Status: {level.Value.Status}";

            return result;
        }
    }
}
