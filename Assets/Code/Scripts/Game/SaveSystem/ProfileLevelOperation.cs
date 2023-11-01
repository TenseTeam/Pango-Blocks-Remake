namespace ProjectPBR.SaveSystem
{
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Data.SaveDatas.Enums;

    public static class ProfileLevelOperation
    {
        public static void SetLevelStatus(LevelKey levelKey, LevelStatus levelStatus)
        {
            ProfileSelector.SelectedProfile.LevelsData[levelKey].Status = levelStatus;
        }
    }
}
