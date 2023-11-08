namespace ProjectPBR.Managers.Static.Profiles
{
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.Patterns.Factories;

    public static class LevelOperation
    {
        public static void SetLevelStatus(LevelKey levelKey, LevelStatus levelStatus)
        {
            ProfileSelector.SelectedProfile.LevelsData[levelKey].Status = levelStatus;
        }

        /// <summary>
        /// Set the level status to <see cref="LevelStatus.Completed"/> and the next level status to <see cref="LevelStatus.Unlocked"/> if there is a next level in this current stage.
        /// </summary>
        /// <param name="levelKey"><see cref="LevelKey"/> of the level.</param>
        public static void CompleteLevel(LevelKey levelKey)
        {
            SetLevelStatus(levelKey, LevelStatus.Completed);

            if (levelKey.LevelIndex + 1 < LevelMapper.ScenesMapping.Stages[levelKey.StageIndex].Levels.Count)
            {
                levelKey++;
                if (ProfileSelector.SelectedProfile.LevelsData[levelKey].Status == LevelStatus.Locked)
                    SetLevelStatus(levelKey, LevelStatus.Unlocked);
            }
        }

        /// <summary>
        /// Gets the level status of the given <see cref="LevelKey"/> level.
        /// </summary>
        /// <param name="levelKey"><see cref="LevelKey"/> of the level.</param>
        /// <returns><see cref="LevelStatus"/> of the level.</returns>
        public static LevelStatus GetLevelStatus(LevelKey levelKey) 
        {
            return ProfileSelector.SelectedProfile.LevelsData[levelKey].Status;
        }

        /// <summary>
        /// Checks if the cutscene of the given stage index is unlocked.
        /// </summary>
        /// <param name="stageIndex"></param>
        /// <returns>True if it is unlocked, False if not.</returns>
        public static bool IsCutsceneUnlocked(int stageIndex)
        {
            return AreLevelsOfStage(stageIndex, LevelStatus.Completed);
        }

        /// <summary>
        /// Checks if all the levels of the given stage index are of the given status.
        /// </summary>
        /// <param name="stageIndex">Stage Index.</param>
        /// <param name="status"><see cref="LevelStatus"/> to check.</param>
        /// <returns>True if all the levels are of the given <see cref="LevelStatus"/>, False if not.</returns>
        public static bool AreLevelsOfStage(int stageIndex, LevelStatus status)
        {
            for(int i = 0; i < LevelMapper.ScenesMapping.Stages[stageIndex].Levels.Count; i++)
            {
                if (!IsLevelOfStatus(i, status))
                    return false;
            }

            return true;
        }

        public static bool IsLevelOfStatus(int levelIndex, LevelStatus status)
        {
            if (levelIndex < 0 || levelIndex >= LevelMapper.ScenesMapping.Stages[LevelMapper.CurrentStageIndex].Levels.Count)
                return false;

            LevelKey key = DataFactory.Create(LevelMapper.CurrentStageIndex, levelIndex, ProfileSelector.SelectedProfile.CurrentDifficulty);
            return GetLevelStatus(key) == status;
        }
    }
}
