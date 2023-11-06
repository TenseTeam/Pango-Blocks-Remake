namespace ProjectPBR.Data.SaveDatas
{
    using ProjectPBR.Data.SaveDatas.Enums;

    [System.Serializable]
    public class LevelData
    {
        public LevelStatus Status;

        public LevelData()
        {
            Status = LevelStatus.Locked;
        }
    }
}
