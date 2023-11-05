namespace ProjectPBR.Data.SaveDatas
{
    using ProjectPBR.Data.SaveDatas.Enums;

    [System.Serializable]
    public class LevelKey
    {
        public int StageIndex;
        public int LevelIndex;
        public GameDifficulty Difficulty;

        public LevelKey(int stageIndex, int levelIndex, GameDifficulty difficulty)
        {
            StageIndex = stageIndex;
            LevelIndex = levelIndex;
            Difficulty = difficulty;
        }

        public static LevelKey operator ++(LevelKey key)
        {
            key.LevelIndex++;
            return key;
        }

        public static LevelKey operator --(LevelKey key)
        {
            key.LevelIndex--;
            return key;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + StageIndex.GetHashCode();
            hash = hash * 23 + LevelIndex.GetHashCode();
            hash = hash * 23 + Difficulty.GetHashCode();

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj is not LevelKey) return false;
            LevelKey key = (LevelKey)obj;

            return
                key.StageIndex == StageIndex &&
                key.LevelIndex == LevelIndex &&
                key.Difficulty == Difficulty;
        }
    }
}
