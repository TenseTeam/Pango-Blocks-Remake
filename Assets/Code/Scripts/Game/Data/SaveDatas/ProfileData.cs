namespace ProjectPBR.Data.SaveDatas
{
    using System.Collections.Generic;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.Patterns.Factories;
    using ProjectPBR.Managers.Main.GameStats;
    using VUDK.Features.Main.SaveSystem;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Generic.Managers.Main;

    [System.Serializable]
    public class ProfileData : SaveDataBase, ICastGameStats<GameStats>
    {
        public string ProfileName;
        public GameDifficulty CurrentDifficulty;
        public Dictionary<LevelKey, LevelData> LevelsData;

        public GameStats GameStats => MainManager.Ins.GameStats as GameStats;

        public ProfileData(string name, GameDifficulty difficulty) : base()
        {
            LevelsData = new Dictionary<LevelKey, LevelData>();
            ProfileName = name;
            CurrentDifficulty = difficulty;

            for (int i = 0; i < GameStats.MappedLevels.Levels.Length; i++)
            {
                LevelsData.Add(
                    DataFactory.CreateLevelKey(i, GameDifficulty.Easy),
                    DataFactory.CreateLevelData(i));
                LevelsData.Add(
                    DataFactory.CreateLevelKey(i, GameDifficulty.Hard),
                    DataFactory.CreateLevelData(i));
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
                result += $"\n {{ Key: [{level.Key.SaveIndex} - {level.Key.Difficulty}] ;; Status: {level.Value.Status} }}";

            return result;
        }
    }
}
