namespace ProjectPBR.Data.SaveDatas
{
    using System.Collections.Generic;
    using VUDK.Features.Main.SaveSystem;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.Patterns.Factories;

    [System.Serializable]
    public class ProfileData : SaveDataBase
    {
        public string ProfileName;
        public List<LevelData> LevelsData;

        public ProfileData(string name) : base()
        {
            LevelsData = new List<LevelData>();
            ProfileName = name;

            for (int i = 0; i < GameConstants.ProfileSaving.NumberOfLevels; i++)
            {
                LevelsData.Add(DataFactory.Create(i, LevelDifficulty.Easy));
                LevelsData.Add(DataFactory.Create(i, LevelDifficulty.Hard));
            }
        }

        public override string ToString()
        {
            return $"{ProfileName} : {ClearedLevelsToString()}";
        }

        private string ClearedLevelsToString()
        {
            string result = string.Empty;

            foreach (var level in LevelsData)
                result += $"\n[{level.SaveIndex} = {level.Status}]";

            return result;
        }
    }
}
