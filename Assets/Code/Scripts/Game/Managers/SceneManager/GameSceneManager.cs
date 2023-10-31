namespace ProjectPBR.Managers.SceneManager
{
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.Data.ScriptableObjects.Levels;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Patterns.Factories;
    using ProjectPBR.SaveSystem;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.BaseManagers;
    using VUDK.Generic.Serializable;

    public class GameSceneManager : SceneManagerBase
    {
        [SerializeField, Header("Level Reset")]
        private TimeDelay _waitResetLevel;

        [SerializeField, Header("Level Build Indexes")]
        private LevelBuildIndexes _levelBuildIndexes;

        protected override void Update()
        {
            base.Update();
            _waitResetLevel.Process();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnBeginGameWonPhase, LoadNextScene);
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnBeginGameoverPhase, WaitLoading);
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnGameoverLoadingScreenCovered, ResetLevel);
            _waitResetLevel.OnCompleted += StartLoading;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnBeginGameWonPhase, LoadNextScene);
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnBeginGameoverPhase, WaitLoading);
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnGameoverLoadingScreenCovered, ResetLevel);
            _waitResetLevel.OnCompleted -= StartLoading;
        }

        public int GetBuildIndexBySaveIndex(int saveIndex)
        {
            LevelDifficulty difficulty = ProfileSelector.SelectedProfile.CurrentDifficulty;

            switch (difficulty)
            {
                case LevelDifficulty.Easy:
                    foreach (LevelIndex levelIndex in _levelBuildIndexes.EasyLevels)
                    {
                        if (levelIndex.LevelNumber == saveIndex)
                        {
                            return levelIndex.LevelBuildIndex;
                        }
                    }
                    break;

                case LevelDifficulty.Hard:
                    foreach (LevelIndex levelIndex in _levelBuildIndexes.HardLevels)
                    {
                        if (levelIndex.LevelNumber == saveIndex)
                        {
                            return levelIndex.LevelBuildIndex;
                        }
                    }
                    break;
            }

            // SaveIndex not found
            return -1;
        }

        public int GetSaveIndexByBuildIndex(int buildIndex)
        {
            LevelDifficulty difficulty = ProfileSelector.SelectedProfile.CurrentDifficulty;

            switch (difficulty)
            {
                case LevelDifficulty.Easy:
                    foreach (LevelIndex levelIndex in _levelBuildIndexes.EasyLevels)
                    {
                        if (levelIndex.LevelBuildIndex == buildIndex)
                        {
                            return levelIndex.LevelNumber;
                        }
                    }
                    break;

                case LevelDifficulty.Hard:
                    foreach (LevelIndex levelIndex in _levelBuildIndexes.HardLevels)
                    {
                        if (levelIndex.LevelBuildIndex == buildIndex)
                        {
                            return levelIndex.LevelNumber;
                        }
                    }
                    break;
            }

            // BuildIndex not found
            return -1;
        }

        public LevelKey GetLevelKeyByBuildIndex(int buildIndex)
        {
            int saveIndex = GetSaveIndexByBuildIndex(buildIndex);
            LevelDifficulty difficulty = ProfileSelector.SelectedProfile.CurrentDifficulty;

            return DataFactory.CreateLevelKey(saveIndex, difficulty);
        }

        private void ResetLevel()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnResetLevel);
        }

        private void StartLoading()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnStartGameoverLoadingScreen);
        }

        private void WaitLoading()
        {
            _waitResetLevel.Start();
        }
    }
}