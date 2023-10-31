namespace ProjectPBR.Data.SaveDatas
{
    using ProjectPBR.Data.SaveDatas.Enums;

    [System.Serializable]
    public class LevelKey
    {
        public int SaveIndex;
        public LevelDifficulty Difficulty;

        public LevelKey(int saveIndex, LevelDifficulty difficulty)
        {
            SaveIndex = saveIndex;
            Difficulty = difficulty;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + SaveIndex.GetHashCode();
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

            return obj.SaveIndex == this.SaveIndex && obj.Difficulty == this.Difficulty;
        }
    }
}
