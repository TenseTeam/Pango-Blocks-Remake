namespace ProjectPBR.Managers
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Config.Constants;
    using UnityEngine.UI;
    using System.Collections;

    [RequireComponent(typeof(Animator))]
    public class LoadingScreenManager : MonoBehaviour
    {
        [SerializeField, Min(0f), Header("Loading Time")]
        private float _waitAfterGameIsEnded;

        private Animator _anim;
        private Image _image;

        private void Awake()
        {
            TryGetComponent(out _anim);
            TryGetComponent(out _image);
            _image.enabled = true;
        }

        private void Start()
        {
            RandomOpen();
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(EventKeys.SceneEvents.OnBeforeChangeScene, RandomClose);
            //MainManager.Ins.EventManager.AddListener(EventKeys.GameEvents.OnGameMachineStart, RandomOpen);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginGameWonPhase, WaitRandomClose);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginGameoverPhase, WaitRandomClose);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(EventKeys.SceneEvents.OnBeforeChangeScene, RandomClose);
            //MainManager.Ins.EventManager.RemoveListener(EventKeys.GameEvents.OnGameMachineStart, RandomOpen);
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginGameWonPhase, WaitRandomClose);
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginGameoverPhase, WaitRandomClose);
        }

        private void WaitRandomClose()
        {
            Invoke(nameof(RandomClose), _waitAfterGameIsEnded);
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
