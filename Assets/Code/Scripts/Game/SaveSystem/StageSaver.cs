namespace ProjectPBR.SaveSystem
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using VUDK.Generic.Managers.Main;
    using VUDK.SaveSystem;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Data.SaveDatas;
    using System;

    public static class StageSaver
    {
        public static LevelsData LevelsData;

        static StageSaver()
        {
            LoadAllData();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            SceneManager.sceneLoaded += BindEvents;
            SceneManager.sceneUnloaded += UnbindEvents;
        }

        private static void BindEvents(Scene arg0, LoadSceneMode arg1)
        {
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnBeginGameWonPhase, SaveLevel);
        }

        private static void UnbindEvents(Scene arg0)
        {
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnBeginGameWonPhase, SaveLevel);
        }

        private static void SaveAllData()
        {
            SaveManager.Save(LevelsData);
        }

        private static void LoadAllData()
        {
            if (!SaveManager.TryLoad(out LevelsData))
            {
                LevelsData = new LevelsData(GameConstants.Levels.MinDefaultLevel, GameConstants.Levels.MaxDefaultLevel);
                SaveAllData();
            }
        }

        private static void SaveLevel()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;

            if (!LevelsData.ClearedLevels.TryAdd(currentScene, LevelStatus.Completed))
                LevelsData.ClearedLevels[currentScene] = LevelStatus.Completed;

            SaveAllData();
        }
    }
}
