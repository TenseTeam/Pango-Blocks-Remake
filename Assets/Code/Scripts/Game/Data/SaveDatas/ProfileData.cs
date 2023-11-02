namespace ProjectPBR.Data.SaveDatas
{
    using System;
    using System.Collections.Generic;
    using VUDK.Features.Main.SaveSystem;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.Patterns.Factories;
    using ProjectPBR.Managers.Main.GameStats;

    [System.Serializable]
    public class ProfileData : SaveDataBase, ICastGameStats<GameStats>
    {
        public string ProfileName;
        public int ProfileIndex;
        public GameDifficulty CurrentDifficulty;
        public Dictionary<LevelKey, LevelData> LevelsData;

        public Guid Id { get; private set; }
        public GameStats GameStats => MainManager.Ins.GameStats as GameStats;

        public ProfileData(string name, int profileIndex, GameDifficulty difficulty) : base()
        {
            LevelsData = new Dictionary<LevelKey, LevelData>();
            ProfileName = name;
            ProfileIndex = profileIndex;
            CurrentDifficulty = difficulty;
            Id = Guid.NewGuid();

            for (int i = 0; i < GameStats.MappedLevels.Levels.Length; i++)
            {
                LevelsData.Add(
                    DataFactory.Create(i, GameDifficulty.Easy),
                    DataFactory.Create(i));
                LevelsData.Add(
                    DataFactory.Create(i, GameDifficulty.Hard),
                    DataFactory.Create(i));
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
                result += $"\n {{ Key: [{level.Key.LevelIndex} - {level.Key.Difficulty}] ;; Status: {level.Value.Status} }}";

            return result;
        }
    }
}
