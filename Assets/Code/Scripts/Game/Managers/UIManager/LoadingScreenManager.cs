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
            TryGetComponent(out _image);
            TryGetComponent(out _anim);
            _image.enabled = true;
        }

        private void Start()
        {
            RandomOpen();
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginGameWonPhase, WaitRandomClose);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginGameoverPhase, WaitRandomClose);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginGameWonPhase, RandomClose);
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginGameoverPhase, RandomClose);
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
