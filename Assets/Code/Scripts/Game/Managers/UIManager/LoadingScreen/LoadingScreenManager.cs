namespace ProjectPBR.Managers.UIManager.LoadingScreen
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using VUDK.Extensions.CustomAttributes;
    using ProjectPBR.Config.Constants;

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
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnStartGameoverLoadingScreen, ResetLevelLoadingScreen);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(EventKeys.SceneEvents.OnBeforeChangeScene, RandomClose);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnStartGameoverLoadingScreen, ResetLevelLoadingScreen);
        }

        [CalledByAnimationEvent]
        public void LoadingScreenCovered()
        {
            MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnGameoverLoadingScreenCovered);
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
            _anim.SetTrigger(Constants.UIAnimations.ResetScreen);
        }

        private void RandomOpen()
        {
            _anim.SetTrigger(Constants.UIAnimations.OpenScreen);
            _anim.SetInteger(Constants.UIAnimations.ScreenState, GetRandom());
        }

        private void RandomClose()
        {
            _anim.SetTrigger(Constants.UIAnimations.CloseScreen);
            _anim.SetInteger(Constants.UIAnimations.ScreenState, GetRandom());
        }

        private int GetRandom()
        {
            return Random.Range(0, Constants.UIAnimations.MaxAnimations);
        }
    }
}
