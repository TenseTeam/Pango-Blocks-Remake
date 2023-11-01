namespace ProjectPBR.Managers.Static
{
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Data.ScriptableObjects.Levels;
    using ProjectPBR.Patterns.Factories;
    using ProjectPBR.SaveSystem;
    using ProjectPBR.Managers.Main.GameStats;

    public static class LevelMapper
    {
        private static LevelMapping _mappedLevels;
        private static GameDifficulty s_CurrDifficulty => ProfileSelector.SelectedProfile.CurrentDifficulty;

        static LevelMapper()
        {
            _mappedLevels = (MainManager.Ins.GameStats as GameStats).MappedLevels;
        }

        public static int GetBuildIndexBySaveIndex(int saveIndex)
        {
            for(int i = 0; i < _mappedLevels.Levels.Length; i++)
            {
                if (i == saveIndex)
                {
                    if (s_CurrDifficulty == GameDifficulty.Hard)
                        return _mappedLevels.Levels[i].HardBuildIndex;

                    if (s_CurrDifficulty == GameDifficulty.Easy)
                        return _mappedLevels.Levels[i].EasyBuildIndex;
                }
            }

            // SaveIndex not found
            return -1;
        }

        public static int GetLevelIndexByBuildIndex(int buildIndex)
        {
            if (s_CurrDifficulty == GameDifficulty.Easy)
            {
                for (int i = 0; i < _mappedLevels.Levels.Length; i++)
                {
                    if (_mappedLevels.Levels[i].EasyBuildIndex == buildIndex)
                        return i;
                }
            }

            if (s_CurrDifficulty == GameDifficulty.Hard)
            {
                for (int i = 0; i < _mappedLevels.Levels.Length; i++)
                {
                    if (_mappedLevels.Levels[i].HardBuildIndex == buildIndex)
                        return i;
                }
            }

            // BuildIndex not found
            return -1;
        }

        public static LevelKey GetLevelKeyByBuildIndex(int buildIndex)
        {
            int saveIndex = GetLevelIndexByBuildIndex(buildIndex);
            GameDifficulty difficulty = ProfileSelector.SelectedProfile.CurrentDifficulty;

            return DataFactory.CreateLevelKey(saveIndex, difficulty);
        }
    }
}
