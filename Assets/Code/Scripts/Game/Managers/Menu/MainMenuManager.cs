namespace ProjectPBR.Managers.Menu
{
    using UnityEngine;
    using VUDK.UI.Menu;

    [RequireComponent(typeof(UIProfiler))]
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField, Header("Sprites")]
        private SpriteRenderer _easySpriteRenderer;
        [SerializeField]
        private SpriteRenderer _hardSpriteRenderer;

        public UIProfiler Profiler { get; private set; }

        private void Awake()
        {
            TryGetComponent(out UIProfiler profiler);
            Profiler = profiler;
        }

        private void OnEnable()
        {
            
        }

        public void ChangeDifficultySprites()
        {

        }
    }
}
