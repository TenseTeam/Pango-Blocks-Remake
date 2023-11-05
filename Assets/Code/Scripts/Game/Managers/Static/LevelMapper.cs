namespace ProjectPBR.Managers.Static
{
    using System.Collections.Generic;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Data.ScriptableObjects.Levels;
    using ProjectPBR.Patterns.Factories;
    using ProjectPBR.Managers.Main.GameStats;
    using ProjectPBR.Managers.Static.Profiles;
    using ProjectPBR.Data.ScriptableObjects.Levels.Structs;

    public static class LevelMapper
    {
        public static int CurrentStageIndex { get; private set; }
        public static ScenesMappingData ScenesMapping { get; private set; }
        private static GameDifficulty s_CurrDifficulty => ProfileSelector.SelectedProfile.CurrentDifficulty;

        static LevelMapper()
        {
            ScenesMapping = (MainManager.Ins.GameStats as GameStats).ScenesMapping;
        }

        public static void SetCurrentStageIndex(int currentStageIndex)
        {
            CurrentStageIndex = currentStageIndex;
        }

        public static int GetCutsceneBuildIndex()
        {
            return ScenesMapping.Stages[CurrentStageIndex].CutsceneBuildIndex;
        }

        public static int GetBuildIndexByLevelIndex(int levelIndex)
        {
            List<LevelMapData> levels = ScenesMapping.Stages[CurrentStageIndex].Levels;

            for(int i = 0; i < levels.Count; i++)
            {
                if (i == levelIndex)
                {
                    if (s_CurrDifficulty == GameDifficulty.Hard)
                        return ScenesMapping.Stages[CurrentStageIndex].Levels[i].HardBuildIndex;

                    if (s_CurrDifficulty == GameDifficulty.Easy)
                        return ScenesMapping.Stages[CurrentStageIndex].Levels[i].EasyBuildIndex;
                }
            }

            // LevelIndex not found
            return -1;
        }

        public static int GetLevelIndexByBuildIndex(int buildIndex)
        {
            if (s_CurrDifficulty == GameDifficulty.Easy)
            {
                for (int i = 0; i < ScenesMapping.Stages[CurrentStageIndex].Levels.Count; i++)
                {
                    if (ScenesMapping.Stages[CurrentStageIndex].Levels[i].EasyBuildIndex == buildIndex)
                        return i;
                }
            }

            if (s_CurrDifficulty == GameDifficulty.Hard)
            {
                for (int i = 0; i < ScenesMapping.Stages[CurrentStageIndex].Levels.Count; i++)
                {
                    if (ScenesMapping.Stages[CurrentStageIndex].Levels[i].HardBuildIndex == buildIndex)
                        return i;
                }
            }

            // BuildIndex not found
            return -1;
        }

        public static int GetFirstUnlockedLevelBuildIndex()
        {
            int levelIndex = GetFirstUnlockedLevelIndex();

            return GetBuildIndexByLevelIndex(levelIndex);
        }

        public static int GetFirstUnlockedLevelIndex()
        {
            for (int i = 0; i < ScenesMapping.Stages[CurrentStageIndex].Levels.Count; i++)
            {
                LevelKey key = DataFactory.Create(CurrentStageIndex, i, s_CurrDifficulty);
                if (LevelOperation.GetLevelStatus(key) == LevelStatus.Unlocked)
                    return i;
            }

            // BuildIndex not found
            return -1;
        }

        public static LevelKey GetLevelKeyByBuildIndex(int buildIndex)
        {
            int levelIndex = GetLevelIndexByBuildIndex(buildIndex);
            return DataFactory.Create(CurrentStageIndex, levelIndex, s_CurrDifficulty);
        }

        public static LevelKey GetLevelKeyByLevelIndex(int levelIndex)
        {
            return DataFactory.Create(CurrentStageIndex, levelIndex, s_CurrDifficulty);
        }
    }
}
