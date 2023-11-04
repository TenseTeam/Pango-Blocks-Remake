namespace ProjectPBR.Data.SaveDatas
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
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
        private float[] _serColor;

        public Guid Id { get; private set; }
        public Color Color => new Color(_serColor[0], _serColor[1], _serColor[2]);
        public GameStats GameStats => MainManager.Ins.GameStats as GameStats;

        public ProfileData(string name, Color color, int profileIndex, GameDifficulty difficulty) : base()
        {
            LevelsData = new Dictionary<LevelKey, LevelData>();
            ProfileName = name;
            ProfileIndex = profileIndex;
            CurrentDifficulty = difficulty;
            ColorSerialize(color);
            Id = Guid.NewGuid();

            for (int i = 0; i < GameStats.LevelMapping.Levels.Length; i++)
            {
                LevelsData.Add(
                    DataFactory.Create(i, GameDifficulty.Easy),
                    DataFactory.Create(i));
                LevelsData.Add(
                    DataFactory.Create(i, GameDifficulty.Hard),
                    DataFactory.Create(i));
            }

            UnlockDefaultLevels();
            UnlockFirstLevels();
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

        private void ColorSerialize(Color color)
        {
            _serColor = new float[3];
            _serColor[0] = color.r;
            _serColor[1] = color.g;
            _serColor[2] = color.b;
        }

        private void UnlockDefaultLevels()
        {
            for (int i = 0; i < GameStats.LevelMapping.LevelsPerStage; i++)
            {
                LevelKey easyKey = DataFactory.Create(i, GameDifficulty.Easy);
                LevelKey hardKey = DataFactory.Create(i, GameDifficulty.Hard);
                LevelsData[easyKey].Status = LevelStatus.Unlocked;
                LevelsData[hardKey].Status = LevelStatus.Unlocked;
            }
        }

        private void UnlockFirstLevels()
        {
            int each = GameStats.LevelMapping.LevelsPerStage;
            for (int i = each; i < GameStats.LevelMapping.Levels.Length; i += each)
            {
                LevelKey easyKey = DataFactory.Create(i, GameDifficulty.Easy);
                LevelKey hardKey = DataFactory.Create(i, GameDifficulty.Hard);
                LevelsData[easyKey].Status = LevelStatus.Unlocked;
                LevelsData[hardKey].Status = LevelStatus.Unlocked;
            }
        }
    }
}
