namespace ProjectPBR.Data.SaveDatas
{
    using ProjectPBR.Data.SaveDatas.Enums;

    [System.Serializable]
    public class LevelData
    {
        public int SaveIndex; // To not confuse with the build index
        public LevelDifficulty Difficulty;
        public LevelStatus Status;

        public LevelData(int saveIndex, LevelDifficulty difficulty)
        {
            SaveIndex = saveIndex;
            Difficulty = difficulty;
            Status = LevelStatus.Locked;
        }
    }
}
