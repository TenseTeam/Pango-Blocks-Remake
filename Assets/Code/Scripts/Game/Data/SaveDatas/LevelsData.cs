namespace ProjectPBR.Data.SaveDatas
{
    using System.Collections.Generic;
    using VUDK.SaveSystem;

    [System.Serializable]
    public class LevelsData : SaveDataBase
    {
        //public string ProfileName; // TO DO: Add profile name
        public Dictionary<int, bool> ClearedLevels;

        public LevelsData() : base()
        {
            ClearedLevels = new Dictionary<int, bool>();
        }
    }
}
