namespace ProjectPBR.Managers.Main.SceneManager
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Serializable;
    using VUDK.Generic.Managers.Main.Bases;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Managers.Static;
    using UnityEngine.SceneManagement;

    public class GameSceneManager : SceneManagerBase
    {
        [SerializeField, Header("Level Reset")]
        private TimeDelay _waitResetLevel;
        [SerializeField, Min(0f)]
        private float _loadMenuDelay;

        protected override void Update()
        {
            base.Update();
            _waitResetLevel.Process();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnSavedCompletedLevel, LaodNextLevel);
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnBeginGameoverPhase, WaitResetLoading);
            MainManager.Ins.EventManager.AddListener(GameConstants.UIEvents.OnGameoverLoadingScreenCovered, ResetLevel);
            _waitResetLevel.OnCompleted += StartLoading;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnSavedCompletedLevel, LaodNextLevel);
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnBeginGameoverPhase, WaitResetLoading);
            MainManager.Ins.EventManager.RemoveListener(GameConstants.UIEvents.OnGameoverLoadingScreenCovered, ResetLevel);
            _waitResetLevel.OnCompleted -= StartLoading;
        }

        public void LoadMenu()
        {
            WaitChangeScene(LevelMapper.ScenesMapping.MenuBuildIndex, _loadMenuDelay);
        }

        public void LoadCutsceneOrFirstUnlocked()
        {
            int firstUnlocked = LevelMapper.GetCutsceneOrFirstUnlockedOrCompletedLevel();
            WaitChangeScene(firstUnlocked);
        }

        private void LaodNextLevel()
        {
            int nextUnlockedLevel = LevelMapper.GetNextUnlockedOrFirstUnlockedLevelOrCutsceneBuildIndexByBuildIndex(SceneManager.GetActiveScene().buildIndex);
            WaitChangeScene(nextUnlockedLevel);
        }

        public bool IsThisMenu()
        {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == LevelMapper.ScenesMapping.MenuBuildIndex;
        }

        private void ResetLevel()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnResetLevel);
        }

        private void StartLoading()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.UIEvents.OnStartGameoverLoadingScreen);
        }

        private void WaitResetLoading()
        {
            _waitResetLevel.Start();
        }
    }
}