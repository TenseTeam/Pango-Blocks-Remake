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

        public static LevelStatus GetLevelStatus(LevelKey levelKey) 
        {
            return ProfileSelector.SelectedProfile.LevelsData[levelKey].Status;
        }

        public static bool IsCutsceneUnlocked(int stageIndex)
        {
            return AreLevelsOfStage(stageIndex, LevelStatus.Completed);
        }

        public static bool AreLevelsOfStage(int stageIndex, LevelStatus status)
        {
            for(int i = 0; i < LevelMapper.ScenesMapping.Stages[stageIndex].Levels.Count; i++)
            {
                LevelKey key = DataFactory.Create(stageIndex, i, ProfileSelector.SelectedProfile.CurrentDifficulty);
                if (GetLevelStatus(key) != status)
                    return false;
            }

            return true;
        }
    }
}
