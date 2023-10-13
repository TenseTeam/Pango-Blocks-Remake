namespace VUDK.Generic.Managers.Main
{
    using UnityEngine;
    using UnityEngine.UI;

    [DefaultExecutionOrder(-800)]
    public sealed class GameConfig : MonoBehaviour
    {
        [field: SerializeField, Header("Main Camera")]
        public Camera MainCamera { get; private set; }

        [SerializeField, Header("Screen")]
        private CanvasScaler _canvasScaler;

        [SerializeField, Min(0f), Header("Game Time")]
        private float _gameScaleTime = 1f;
        [SerializeField, Min(0f)]
        private int _gameFPS = 60;

        public float GameScaleTime
        {
            get
            {
                return _gameScaleTime;
            }
            set
            {
                _gameScaleTime = value;
                Time.timeScale = _gameScaleTime;
            }
        }
        public int GameFPS
        {
            get
            {
                return _gameFPS;
            }
            set
            {
                _gameFPS = value;
                Application.targetFrameRate = _gameFPS;
            }
        }
        public Vector2 ReferenceResolution => _canvasScaler.referenceResolution;

        private void Awake()
        {
            Time.timeScale = _gameScaleTime;
            Application.targetFrameRate = _gameFPS;
        }
    }
}