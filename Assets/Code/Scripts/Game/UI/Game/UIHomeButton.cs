namespace ProjectPBR.UI.Game
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.UI.Buttons;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Managers.Main.SceneManager;

    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class UIHomeButton : UIButton, ICastSceneManager<GameSceneManager>
    {
        private Image _image;

        public GameSceneManager SceneManager => MainManager.Ins.SceneManager as GameSceneManager;

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out _image);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnBeginPlacementPhase, Enable);
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnBeginGameoverPhase, Disable);
        }

        /// <summary>
        /// Triggers the press of this button and load the menu.
        /// </summary>
        protected override void Press()
        {
            base.Press();
            SceneManager.LoadMenu();
        }

        /// <summary>
        /// Enables the button and its image.
        /// </summary>
        protected override void Enable()
        {
            Button.interactable = true;
            _image.enabled = true;
        }

        /// <summary>
        /// Disables the button and its image.
        /// </summary>
        protected override void Disable()
        {
            Button.interactable = false;
            _image.enabled = false;
        }
    }
}
