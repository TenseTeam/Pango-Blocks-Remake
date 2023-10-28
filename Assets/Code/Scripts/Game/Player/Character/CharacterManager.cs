namespace ProjectPBR.Player.Character
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Config.Constants;
    using System;

    [RequireComponent(typeof(CharacterPathRunner))]
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField, Header("Character Graphics Controller")]
        private CharacterGraphicsController _graphics;

        private CharacterPathRunner _runner;

        private void Awake()
        {
            TryGetComponent(out _runner);
        }

        private void OnEnable()
        {
            // Register here events that will trigger multiple methods
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginObjectivePhase, StartCharacter);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnResetLevel, ResetCharacter);
        }

        private void OnDisable()
        {
            // Unregister here events that will trigger multiple methods
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginObjectivePhase, StartCharacter);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnResetLevel, ResetCharacter);
        }

        private void StartCharacter()
        {
            _runner.StartPath();
            _graphics.SetStartAnimation();
            _graphics.IncreaseRender();
        }

        private void ResetCharacter()
        {
            _runner.ResetPosition();
            _graphics.ResetGraphics();
        }
    }
}
