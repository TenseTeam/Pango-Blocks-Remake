namespace ProjectPBR.Managers.SceneManager
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.BaseManagers;
    using VUDK.Generic.Serializable;
    using ProjectPBR.GameConfig.Constants;

    public class GameSceneManager : SceneManagerBase
    {
        [SerializeField]
        private TimeDelay _waitResetLevel;

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
