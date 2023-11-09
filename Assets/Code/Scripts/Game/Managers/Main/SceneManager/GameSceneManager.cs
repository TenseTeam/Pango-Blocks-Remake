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

        /// <summary>
        /// Waits and loads the menu scene.
        /// </summary>
        public void LoadMenu()
        {
            WaitChangeScene(LevelMapper.ScenesMapping.MenuBuildIndex, _loadMenuDelay);
        }

        /// <summary>
        /// Waits and loads the first unlocked level or cutscene of the current stage.
        /// </summary>
        public void LoadCutsceneOrFirstUnlocked()
        {
            int firstUnlocked = LevelMapper.GetCutsceneOrFirstUnlockedOrCompletedLevel();
            WaitChangeScene(firstUnlocked);
        }

        /// <summary>
        /// Waits and loads the next unlocked level or cutscene by the current level scene build index.
        /// </summary>
        private void LaodNextLevel()
        {
            int nextUnlockedLevel = LevelMapper.GetNextUnlockedOrFirstUnlockedLevelOrCutsceneBuildIndexByBuildIndex(SceneManager.GetActiveScene().buildIndex);
            WaitChangeScene(nextUnlockedLevel);
        }

        /// <summary>
        /// Checks if the current scene is the menu scene by its build index.
        /// </summary>
        /// <returns>True if it is the menu scene, False if not.</returns>
        public bool IsThisMenu()
        {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == LevelMapper.ScenesMapping.MenuBuildIndex;
        }

        /// <summary>
        /// Triggers the reset of the current level.
        /// </summary>
        private void ResetLevel()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnResetLevel);
        }

        /// <summary>
        /// Triggers the start of the loading screen.
        /// </summary>
        private void StartLoading()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.UIEvents.OnStartGameoverLoadingScreen);
        }

        /// <summary>
        /// Starts the reset of the level.
        /// </summary>
        private void WaitResetLoading()
        {
            _waitResetLevel.Start();
        }
    }
}