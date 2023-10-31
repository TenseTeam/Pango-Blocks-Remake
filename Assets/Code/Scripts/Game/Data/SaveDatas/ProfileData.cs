namespace ProjectPBR.Data.SaveDatas
{
    using ProjectPBR.GameConfig.Constants;
    using System.Collections.Generic;
    using VUDK.Features.Main.SaveSystem;

    [System.Serializable]
    public class ProfileData : SaveDataBase
    {
        public string ProfileName;
        public Dictionary<int, LevelStatus> ClearedLevels;

        public ProfileData(string name) : base()
        {
            ClearedLevels = new Dictionary<int, LevelStatus>();
            ProfileName = name;

            for (int i = GameConstants.ProfileSaving.MinDefaultLevel; i <= GameConstants.ProfileSaving.MaxDefaultLevel; i++)
                ClearedLevels.Add(i, LevelStatus.Unlocked);
        }

        public override string ToString()
        {
            return $"{ProfileName}: {ClearedLevelsToString()}";
        }

        private string ClearedLevelsToString()
        {
            string result = string.Empty;

            foreach (var levelPair in ClearedLevels)
                result += $"\n[{levelPair.Key} = {levelPair.Value}]";

            return result;
        }
    }
}
