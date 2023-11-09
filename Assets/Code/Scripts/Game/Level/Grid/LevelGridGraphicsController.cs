namespace ProjectPBR.Level.Grid
{
    using UnityEngine;
    using VUDK.Extensions.Colors;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Serializable;
    using ProjectPBR.GameConfig.Constants;

    [RequireComponent(typeof(SpriteRenderer))]
    public class LevelGridGraphicsController : MonoBehaviour
    {
        [SerializeField, Header("Grid Fade")]
        private TimeDelay _fadeDelay;

        private SpriteRenderer _gridSprite;
        private bool _isFading;

        [SerializeField]
        private Color _startColor;
        [SerializeField]
        private Color _fadeOutColor;

        private void Awake()
        {
            TryGetComponent(out _gridSprite);

            _startColor = _gridSprite.color.Copy();
            _fadeOutColor = _startColor.Copy();
            _fadeOutColor.a = 0f;
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnBeginObjectivePhase, StartFading);
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnResetLevel, ResetGraphicsGrid);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnBeginObjectivePhase, StartFading);
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnResetLevel, ResetGraphicsGrid);
        }

        private void Update()
        {
            FadeGridOut();
        }

        /// <summary>
        /// Resets the grid graphics to its start values.
        /// </summary>
        private void ResetGraphicsGrid()
        {
            _gridSprite.color = _startColor;
        }

        /// <summary>
        /// Starts the grid fading to its fade out color.
        /// </summary>
        private void StartFading()
        {
            _isFading = true;
            _fadeDelay.Start();
        }

        /// <summary>
        /// Fades the grid out.
        /// </summary>
        private void FadeGridOut()
        {
            if (!_isFading) return;

            _fadeDelay.Process();
            _gridSprite.color = Color.Lerp(_gridSprite.color, _fadeOutColor,_fadeDelay.ClampNormalizedTime);
            if(_gridSprite.color.ColorEquals(_fadeOutColor))
                _isFading = false;
        }
    }
}
