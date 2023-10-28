namespace ProjectPBR.Level.Grid
{
    using ProjectPBR.Config.Constants;
    using System;
    using UnityEngine;
    using VUDK.Extensions.Colors;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Serializable;

    [RequireComponent(typeof(SpriteRenderer))]
    public class LevelGridGraphicsController : MonoBehaviour
    {
        [SerializeField, Min(0f), Header("Grid Fade")]
        private float _fadeDuration;

        private SpriteRenderer _gridSprite;
        
        private TimeDelay _fadeDelay;
        private bool _isFading;

        private Color _fadeOutColor = new Color(1, 1, 1, 0);

        private void Awake()
        {
            TryGetComponent(out _gridSprite);
            _fadeDelay = new TimeDelay(_fadeDuration);
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginObjectivePhase, StartFading);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginObjectivePhase, StartFading);
        }

        private void Update()
        {
            FadeGridOut();
        }

        private void StartFading()
        {
            _isFading = true;
            _fadeDelay = new TimeDelay(_fadeDuration);
        }

        private void FadeGridOut()
        {
            if (!_isFading) return;

            _fadeDelay.Process();
            _gridSprite.color = Color.Lerp(_gridSprite.color, _fadeOutColor,_fadeDelay.ClampNormalizedTime);
            if(_gridSprite.color.ColorEquals(Color.clear))
                _isFading = false;
        }
    }
}
