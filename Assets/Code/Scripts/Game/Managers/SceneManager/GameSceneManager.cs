namespace ProjectPBR.Managers
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.BaseManagers;
    using VUDK.Generic.Serializable;
    using ProjectPBR.Config.Constants;

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
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginGameWonPhase, LoadNextScene);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginGameoverPhase, WaitResetLevel);
            _waitResetLevel.OnCompleted += ResetLevel;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginGameWonPhase, LoadNextScene);
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginGameoverPhase, WaitResetLevel);
            _waitResetLevel.OnCompleted -= ResetLevel;
        }

        private void ResetLevel()
        {
            MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnResetLevel);
        }

        private void WaitResetLevel()
        {
            _waitResetLevel.Reset();
            _waitResetLevel.Start();
        }
    }
}
