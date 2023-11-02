namespace ProjectPBR.Data.SaveDatas
{
    using ProjectPBR.Data.SaveDatas.Enums;

    [System.Serializable]
    public class LevelKey
    {
        public int LevelIndex;
        public GameDifficulty Difficulty;

        public LevelKey(int levelIndex, GameDifficulty difficulty)
        {
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
            hash = hash * 23 + LevelIndex.GetHashCode();
            hash = hash * 23 + Difficulty.GetHashCode();

            return hash;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LevelKey);
        }

        public bool Equals(LevelKey obj)
        {
            if(obj == null) return false;

            return obj.LevelIndex == this.LevelIndex && obj.Difficulty == this.Difficulty;
        }
    }
}
