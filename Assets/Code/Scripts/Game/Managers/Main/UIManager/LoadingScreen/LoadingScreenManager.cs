namespace ProjectPBR.Managers.UIManager.Game.LoadingScreen
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Extensions.CustomAttributes;
    using VUDK.Config;
    using VUDK.Generic.Serializable;
    using ProjectPBR.GameConfig.Constants;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Managers.Main.SceneManager;
    using VUDK.Generic.Managers.Static;

    public class LoadingScreenManager : MonoBehaviour, ICastSceneManager<GameSceneManager>
    {
        [SerializeField, Min(0f), Header("Loading Screen Duration")]
        private float _minDuration;

        [SerializeField, Header("Animators")]
        private Animator _closeAnim;
        [SerializeField]
        private Animator _openAnim;
        [SerializeField]
        private Animator _resetAnim;

        private TimeDelay _waitRandomClose;

        public GameSceneManager SceneManager => MainManager.Ins.SceneManager as GameSceneManager;

        private void Awake()
        {
            _waitRandomClose = new TimeDelay(_minDuration);

            if (!SceneManager.IsThisMenu())
            {
                RandomOpen();
            }
            else
            {
                if (GameControl.HasBeenStarted)
                {

                    RandomOpen();
                }
            }
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<float>(EventKeys.SceneEvents.OnBeforeChangeScene, WaitRandomClose);
            MainManager.Ins.EventManager.AddListener(GameConstants.UIEvents.OnStartGameoverLoadingScreen, ResetLevelLoadingScreen);
            _waitRandomClose.OnCompleted += RandomClose;
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener<float>(EventKeys.SceneEvents.OnBeforeChangeScene, WaitRandomClose);
            MainManager.Ins.EventManager.RemoveListener(GameConstants.UIEvents.OnStartGameoverLoadingScreen, ResetLevelLoadingScreen);
            _waitRandomClose.OnCompleted -= RandomClose;
        }

        private void Update() => _waitRandomClose.Process();

        /// <summary>
        /// Starts the animation for the reset loading screen.
        /// </summary>
        private void ResetLevelLoadingScreen()
        {
            Enable(_resetAnim);
            _resetAnim.SetTrigger(GameConstants.UIAnimations.ResetScreen);
        }

        /// <summary>
        /// Waits a delay before starting the close loading screen animation.
        /// </summary>
        /// <param name="delayChangeScene">Delay to wait.</param>
        private void WaitRandomClose(float delayChangeScene)
        {
            float duration = delayChangeScene - _minDuration;
            _waitRandomClose.ChangeDelay(duration);
            _waitRandomClose.Start();
        }

        /// <summary>
        /// Starts a random open animation.
        /// </summary>
        private void RandomOpen()
        {
            Enable(_openAnim);
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.UIEvents.OnLoadingScreenOpen);
            _openAnim.SetTrigger(GameConstants.UIAnimations.OpenScreen);
            _openAnim.SetInteger(GameConstants.UIAnimations.ScreenState, GetRandom());
        }

        /// <summary>
        /// Starts a random close animation.
        /// </summary>
        private void RandomClose()
        {
            Enable(_closeAnim);
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.UIEvents.OnLoadingScreenClose);
            _closeAnim.SetTrigger(GameConstants.UIAnimations.CloseScreen);
            _closeAnim.SetInteger(GameConstants.UIAnimations.ScreenState, GetRandom());
        }

        /// <summary>
        /// Enables the given animator and disables the others.
        /// </summary>
        /// <param name="anim">Animator to enable.</param>
        private void Enable(Animator anim)
        {
            _openAnim.gameObject.SetActive(false);
            _closeAnim.gameObject.SetActive(false);
            _resetAnim.gameObject.SetActive(false);
            anim.gameObject.SetActive(true);
        }

        /// <summary>
        /// Gets a random number between 0 and <see cref="GameConstants.UIAnimations.MaxAnimations"/>.
        /// </summary>
        /// <returns>Random number.</returns>
        private int GetRandom()
        {
            return Random.Range(0, GameConstants.UIAnimations.MaxAnimations);
        }
    }
}
