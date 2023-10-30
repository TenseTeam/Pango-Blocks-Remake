namespace ProjectPBR.Data.SaveDatas
{
    using System.Collections.Generic;
    using VUDK.Generic.Serializable;
    using VUDK.SaveSystem;

    [System.Serializable]
    public class LevelsData : SaveDataBase
    {
        //public string ProfileName; // TO DO: Add profile name
        public Dictionary<int, LevelStatus> ClearedLevels;

        public LevelsData(int min, int max) : base()
        {
            ClearedLevels = new Dictionary<int, LevelStatus>();

            for (int i = min; i <= max; i++)
                ClearedLevels.Add(i, LevelStatus.Unlocked);
        }
    }
}
