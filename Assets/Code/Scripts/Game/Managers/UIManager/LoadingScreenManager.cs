namespace ProjectPBR.Managers
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Serializable;
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
            _image.enabled = true;
        }

        private void Start() => RandomOpen();

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(EventKeys.SceneEvents.OnBeforeChangeScene, RandomClose);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnResetLevel, ResetScreen);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(EventKeys.SceneEvents.OnBeforeChangeScene, RandomClose);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnResetLevel, ResetScreen);
        }

        private void ResetScreen()
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
