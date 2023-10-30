﻿namespace ProjectPBR.Managers.UIManager.LoadingScreen
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using VUDK.Extensions.CustomAttributes;
    using VUDK.Config;
    using ProjectPBR.GameConfig.Constants;
    using VUDK.Generic.Serializable;

    [RequireComponent(typeof(Animator))]
    public class LoadingScreenManager : MonoBehaviour
    {
        [SerializeField, Header("Wait Before Close")]
        private TimeDelay _waitRandomClose;

        private Animator _anim;
        private Image _image;

        private void Awake()
        {
            TryGetComponent(out _anim);
            TryGetComponent(out _image);
            EnableScreen();
        }

        private void Start() => RandomOpen();

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(EventKeys.SceneEvents.OnBeforeChangeScene, WaitRandomClose);
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnStartGameoverLoadingScreen, ResetLevelLoadingScreen);
            _waitRandomClose.OnCompleted += RandomClose;
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(EventKeys.SceneEvents.OnBeforeChangeScene, WaitRandomClose);
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnStartGameoverLoadingScreen, ResetLevelLoadingScreen);
            _waitRandomClose.OnCompleted -= RandomClose;
        }

        private void Update() => _waitRandomClose.Process();

        [CalledByAnimationEvent]
        public void LoadingScreenCovered()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnGameoverLoadingScreenCovered);
        }

        public void EnableScreen()
        {
            _image.enabled = true;
        }

        public void DisableScreen()
        {
            _image.enabled = false;
        }

        private void ResetLevelLoadingScreen()
        {
            _anim.SetTrigger(GameConstants.UIAnimations.ResetScreen);
        }

        private void WaitRandomClose()
        {
            _waitRandomClose.Start();
        }

        private void RandomOpen()
        {
            _anim.SetTrigger(GameConstants.UIAnimations.OpenScreen);
            _anim.SetInteger(GameConstants.UIAnimations.ScreenState, GetRandom());
        }

        private void RandomClose()
        {
            _anim.SetTrigger(GameConstants.UIAnimations.CloseScreen);
            _anim.SetInteger(GameConstants.UIAnimations.ScreenState, GetRandom());
        }

        private int GetRandom()
        {
            return Random.Range(0, GameConstants.UIAnimations.MaxAnimations);
        }
    }
}
