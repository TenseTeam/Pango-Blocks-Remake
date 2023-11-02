namespace ProjectPBR.Managers.Main.SceneManager
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Serializable;
    using VUDK.Generic.Managers.Main.Bases;
    using ProjectPBR.Data.ScriptableObjects.Levels;
    using ProjectPBR.GameConfig.Constants;

    public class GameSceneManager : SceneManagerBase
    {
        [SerializeField, Header("Level Reset")]
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
            MainManager.Ins.EventManager.AddListener(GameConstants.UIEvents.OnGameoverLoadingScreenCovered, ResetLevel);
            _waitResetLevel.OnCompleted += StartLoading;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnBeginGameWonPhase, LoadNextScene);
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnBeginGameoverPhase, WaitLoading);
            MainManager.Ins.EventManager.RemoveListener(GameConstants.UIEvents.OnGameoverLoadingScreenCovered, ResetLevel);
            _waitResetLevel.OnCompleted -= StartLoading;
        }

        private void ResetLevel()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnResetLevel);
        }

        private void StartLoading()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.UIEvents.OnStartGameoverLoadingScreen);
        }

        private void WaitLoading()
        {
            _waitResetLevel.Start();
        }
    }
}