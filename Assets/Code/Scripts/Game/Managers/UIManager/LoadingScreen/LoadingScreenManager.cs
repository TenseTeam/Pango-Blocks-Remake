namespace ProjectPBR.Managers.UIManager.LoadingScreen
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using VUDK.Extensions.CustomAttributes;
    using VUDK.Config;
    using ProjectPBR.GameConfig.Constants;

    [RequireComponent(typeof(Animator))]
    public class LoadingScreenManager : MonoBehaviour
    {
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
            MainManager.Ins.EventManager.AddListener(EventKeys.SceneEvents.OnBeforeChangeScene, RandomClose);
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnStartGameoverLoadingScreen, ResetLevelLoadingScreen);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(EventKeys.SceneEvents.OnBeforeChangeScene, RandomClose);
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnStartGameoverLoadingScreen, ResetLevelLoadingScreen);
        }

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
