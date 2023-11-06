namespace ProjectPBR.Managers.Main.SceneManager
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Serializable;
    using VUDK.Generic.Managers.Main.Bases;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Managers.Main.GameStats;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Managers.Static;

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
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnBeginGameWonPhase, LaodNextUnlockedLevelOrBackToMenu);
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnBeginGameoverPhase, WaitResetLoading);
            MainManager.Ins.EventManager.AddListener(GameConstants.UIEvents.OnGameoverLoadingScreenCovered, ResetLevel);
            _waitResetLevel.OnCompleted += StartLoading;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnBeginGameWonPhase, LaodNextUnlockedLevelOrBackToMenu);
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnBeginGameoverPhase, WaitResetLoading);
            MainManager.Ins.EventManager.RemoveListener(GameConstants.UIEvents.OnGameoverLoadingScreenCovered, ResetLevel);
            _waitResetLevel.OnCompleted -= StartLoading;
        }

        public void LoadMenu()
        {
            WaitChangeScene(LevelMapper.ScenesMapping.MenuBuildIndex, _loadMenuDelay);
        }

        public bool TryLaodNextUnlockedLevel()
        {
            int nextUnlockedLevel = LevelMapper.GetFirstUnlockedLevelOrCutsceneBuildIndex();
            if (nextUnlockedLevel < 0)
                return false;

            WaitChangeScene(nextUnlockedLevel);
            return true;
        }

        private void LaodNextUnlockedLevelOrBackToMenu()
        {
            if (!TryLaodNextUnlockedLevel())
                LoadMenu();
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