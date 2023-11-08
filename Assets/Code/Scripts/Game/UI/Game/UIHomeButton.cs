namespace ProjectPBR.UI.Game
{
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Managers.Main.SceneManager;
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;

    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class UIHomeButton : MonoBehaviour, ICastSceneManager<GameSceneManager>
    {
        private Button _button;
        private Image _image;

        public GameSceneManager SceneManager => MainManager.Ins.SceneManager as GameSceneManager;

        private void Awake()
        {
            TryGetComponent(out _button);
            TryGetComponent(out _image);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnHomeButtonClicked);
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnBeginPlacementPhase, Enable);
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnBeginGameoverPhase, Disable);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnHomeButtonClicked);
        }

        private void OnHomeButtonClicked()
        {
            SceneManager.LoadMenu();
        }

        private void Disable()
        {
            _button.interactable = false;
            _image.enabled = false;
        }

        private void Enable()
        {
            _button.interactable = true;
            _image.enabled = true;
        }
    }
}
