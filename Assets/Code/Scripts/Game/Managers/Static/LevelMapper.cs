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

        /// <summary>
        /// Tries to set the current stage index, if a stage of the given index exists.
        /// </summary>
        /// <param name="stageIndex">Stage Index to set.</param>
        public static void TrySetCurrentStageIndex(int stageIndex)
        {
            if (stageIndex < 0 || stageIndex >= ScenesMapping.Stages.Count)
                return;

            CurrentStageIndex = stageIndex;
        }

        /// <summary>
        /// Gets the current stage's cutscene build index.
        /// </summary>
        /// <returns>Cutscene build index of the current stage.</returns>
        public static int GetCutsceneBuildIndex()
        {
            return ScenesMapping.Stages[CurrentStageIndex].CutsceneBuildIndex;
        }

        /// <summary>
        /// Gets the cutscene build index if all the levels of the current stage are completed, or the first unlocked or completed level of the stage build index. 
        /// </summary>
        /// <returns>Cutscene or level build index.</returns>
        public static int GetCutsceneOrFirstUnlockedOrCompletedLevel()
        {
            if (LevelOperation.IsCutsceneUnlocked(CurrentStageIndex))
                return GetCutsceneBuildIndex();

            return GetFirstUnlockedLevelBuildIndex();
        }

        /// <summary>
        /// Gets the next unlocked or first unlocked level build index, or cutscene if all levels of the stage are completed build index.
        /// </summary>
        /// <param name="buildIndex"></param>
        /// <returns>Cutscene or level build index.</returns>
        public static int GetNextUnlockedOrFirstUnlockedLevelOrCutsceneBuildIndexByBuildIndex(int buildIndex)
        {
            int currentLevelIndex = GetLevelIndexByBuildIndex(buildIndex);
            int nextLevelIndex = currentLevelIndex + 1;

            if (LevelOperation.IsLevelOfStatus(nextLevelIndex, LevelStatus.Completed) || LevelOperation.IsLevelOfStatus(nextLevelIndex, LevelStatus.Unlocked))
                return GetBuildIndexByLevelIndex(nextLevelIndex);

            if (LevelOperation.IsCutsceneUnlocked(CurrentStageIndex))
                return GetCutsceneBuildIndex();

            return GetFirstUnlockedLevelBuildIndex();
        }

        /// <summary>
        /// Gets the build index of the level with the given level index.
        /// </summary>
        /// <param name="levelIndex">Level Index.</param>
        /// <returns>Level Build Index.</returns>
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

        /// <summary>
        /// Gets the level index of the level with the given build index.
        /// </summary>
        /// <param name="buildIndex">Level Build Index.</param>
        /// <returns>Level Index.</returns>
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

        /// <summary>
        /// Gets the first level build index of status <see cref="LevelStatus.Unlocked"/> or <see cref="LevelStatus.Completed"/>.
        /// </summary>
        /// <param name="from">Level index to start searching from.</param>
        /// <returns>Unlocked level build index.</returns>
        public static int GetFirstUnlockedLevelBuildIndex(int from = 0)
        {
            int levelIndex = GetFirstLevelIndexOfStatus(LevelStatus.Unlocked, from);

            return GetBuildIndexByLevelIndex(levelIndex);
        }

        /// <summary>
        /// Gets the first level build index of status <see cref="LevelStatus.Completed"/>.
        /// </summary>
        /// <param name="from">Level index to start searching from.</param>
        /// <returns>Completed level build index.</returns>
        public static int GetFirstCompletedLevelBuildIndex(int from = 0)
        {
            int levelIndex = GetFirstLevelIndexOfStatus(LevelStatus.Completed, from);

            return GetBuildIndexByLevelIndex(levelIndex);
        }

        /// <summary>
        /// Gets the first level build index of given status.
        /// </summary>
        /// <param name="from">Level index to start searching from.</param>
        /// <returns>Build index of the first  <see cref="LevelStatus.Unlocked"/> level.</returns>
        public static int GetFirstLevelIndexOfStatus(LevelStatus status, int from = 0)
        {
            for (int i = from; i < ScenesMapping.Stages[CurrentStageIndex].Levels.Count; i++)
            {
                if (LevelOperation.IsLevelOfStatus(i, status))
                    return i;
            }

            // BuildIndex not found
            return -1;
        }

        /// <summary>
        /// Gets the <see cref="LevelKey"/> of the level with the given build index.
        /// </summary>
        /// <param name="buildIndex">Level Build Index.</param>
        /// <returns>Level <see cref="LevelKey"/>.</returns>
        public static LevelKey GetLevelKeyByBuildIndex(int buildIndex)
        {
            int levelIndex = GetLevelIndexByBuildIndex(buildIndex);
            return DataFactory.Create(CurrentStageIndex, levelIndex, s_CurrDifficulty);
        }

        /// <summary>
        /// Gets the <see cref="LevelKey"/> of the level with the given level index.
        /// </summary>
        /// <param name="levelIndex">Level Index.</param>
        /// <returns>Level <see cref="LevelKey"/>.</returns>
        public static LevelKey GetLevelKeyByLevelIndex(int levelIndex)
        {
            return DataFactory.Create(CurrentStageIndex, levelIndex, s_CurrDifficulty);
        }
    }
}
