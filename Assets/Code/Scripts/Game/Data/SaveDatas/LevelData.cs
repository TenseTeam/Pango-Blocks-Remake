using ProjectPBR.Data.SaveDatas.Enums;

namespace ProjectPBR.Data.SaveDatas
{
    [System.Serializable]
    public class LevelData
    {
        public int LevelNumber;
        public LevelStatus Status;

        public LevelData(int levelNumber)
        {
            LevelNumber = levelNumber;
            Status = LevelStatus.Locked;
        }
    }
}
