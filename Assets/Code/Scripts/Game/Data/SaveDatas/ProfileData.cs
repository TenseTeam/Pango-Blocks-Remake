namespace ProjectPBR.Data.SaveDatas
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Extensions.Colors;
    using VUDK.Features.Main.SaveSystem;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.Data.ScriptableObjects.Levels.Structs;
    using ProjectPBR.Managers.Main.GameStats;
    using ProjectPBR.Patterns.Factories;
    using ProjectPBR.Managers.Static;

    [System.Serializable]
    public class ProfileData : SaveDataBase
    {
        public string ProfileName;
        public int ProfileIndex;
        public GameDifficulty CurrentDifficulty;
        public float[] Rgba;
        public Dictionary<LevelKey, LevelData> LevelsData;

        public Guid Id { get; private set; }
        public GameStats GameStats => MainManager.Ins.GameStats as GameStats;

        public ProfileData(string name, Color color, int profileIndex, GameDifficulty difficulty) : base()
        {
            LevelsData = new Dictionary<LevelKey, LevelData>();
            ProfileName = name;
            ProfileIndex = profileIndex;
            CurrentDifficulty = difficulty;
            Rgba = color.Serialize();
            Id = Guid.NewGuid();

            Init();
        }

        public void Init()
        {
            List<StageMapData> stages = LevelMapper.ScenesMapping.Stages;

            foreach (var stage in stages)
            {
                int stageIndex = stages.IndexOf(stage);

                foreach (var level in stage.Levels)
                {
                    int levelIndex = stage.Levels.IndexOf(level);

                    LevelsData.Add(
                        DataFactory.Create(stageIndex, levelIndex, GameDifficulty.Easy),
                        DataFactory.Create());
                    LevelsData.Add(
                        DataFactory.Create(stageIndex, levelIndex, GameDifficulty.Hard),
                        DataFactory.Create());
                }
            }

            UnlockDefaultLevels();
            UnlockFirstLevels();
        }

        private void UnlockDefaultLevels()
        {
            for (int i = 0; i < LevelMapper.ScenesMapping.LevelsPerStage; i++)
            {
                LevelKey easyKey = DataFactory.Create(0, i, GameDifficulty.Easy);
                LevelKey hardKey = DataFactory.Create(0, i, GameDifficulty.Hard);
                LevelsData[easyKey].Status = LevelStatus.Unlocked;
                LevelsData[hardKey].Status = LevelStatus.Unlocked;
            }
        }

        private void UnlockFirstLevels()
        {
            List<StageMapData> stages = LevelMapper.ScenesMapping.Stages;

            foreach (var stage in stages)
            {
                int stageIndex = stages.IndexOf(stage);
                LevelKey easyKey = DataFactory.Create(stageIndex, 0, GameDifficulty.Easy);
                LevelKey hardKey = DataFactory.Create(stageIndex, 0, GameDifficulty.Hard);
                LevelsData[easyKey].Status = LevelStatus.Unlocked;
                LevelsData[hardKey].Status = LevelStatus.Unlocked;
            }
        }

        public override string ToString()
        {
            string profileNameColor = "<color=yellow>" + ProfileName + "</color>";
            string difficultyColor = "<color=green>" + CurrentDifficulty + "</color>";

            string result = $"<color=cyan>Profile Information:</color>\n";
            result += $"<color=white>- Profile Name:</color> {profileNameColor}\n";
            result += $"<color=white>- Profile Difficulty:</color> {difficultyColor}\n";
            result += "<color=cyan>Levels:</color>\n";

            foreach (var entry in LevelsData)
            {
                string stageColor = "<color=green>" + entry.Key.StageIndex + "</color>";
                string levelColor = "<color=yellow>" + entry.Key.LevelIndex + "</color>";
                string levelDiffColor = "<color=red>" + entry.Key.Difficulty + "</color>";
                string statusColor = entry.Value.Status switch
                {
                    LevelStatus.Locked => "<color=red>Locked</color>",
                    LevelStatus.Unlocked => "<color=green>Unlocked</color>",
                    LevelStatus.Completed => "<color=yellow>Completed</color>",
                    _ => "<color=white>Unknown</color>"
                };

                result += $"- Stage: {stageColor}, Level: {levelColor}, Difficulty: {levelDiffColor}, Status: {statusColor}\n";
            }

            return result;
        }
    }
}