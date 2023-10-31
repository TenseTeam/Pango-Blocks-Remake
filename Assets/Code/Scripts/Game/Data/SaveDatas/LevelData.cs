namespace ProjectPBR.Data.SaveDatas
{
    using ProjectPBR.Data.SaveDatas.Enums;

    [System.Serializable]
    public class LevelData
    {
        public int LevelNumber; // To not confuse with the build index
        public LevelDifficulty Difficulty;
        public LevelStatus Status;

        public LevelData(int levelNumber, LevelDifficulty difficulty)
        {
            LevelNumber = levelNumber;
            Difficulty = difficulty;
            Status = LevelStatus.Locked;
        }
    }
}
