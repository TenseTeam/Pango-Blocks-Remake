namespace ProjectPBR.Player.Character
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.GameConfig.Constants;
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
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnBeginObjectivePhase, StartCharacter);
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnResetLevel, ResetCharacter);
        }

        private void OnDisable()
        {
            // Unregister here events that will trigger multiple methods
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnBeginObjectivePhase, StartCharacter);
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnResetLevel, ResetCharacter);
        }

        /// <summary>
        /// Starts the character pathing.
        /// </summary>
        private void StartCharacter()
        {
            _runner.StartPath();
            _graphics.SetStartAnimation();
            _graphics.IncreaseRender();
        }

        /// <summary>
        /// Resets the character to its start position.
        /// </summary>
        private void ResetCharacter()
        {
            _runner.ResetPosition();
            _graphics.ResetGraphics();
        }
    }
}
