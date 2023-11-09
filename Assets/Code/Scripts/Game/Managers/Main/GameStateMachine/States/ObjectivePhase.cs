namespace ProjectPBR.Managers.Main.GameStateMachine.States
{
    using System;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.StateMachine;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Level.PathSystem;
    using ProjectPBR.Managers.Main.GameStateMachine.States.Keys;

    public class ObjectivePhase : State<GameContext>
    {
        public ObjectivePhase(Enum stateKey, StateMachine relatedStateMachine, StateMachineContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        /// <inheritdoc/>
        public override void Enter()
        {
#if DEBUG
            Debug.Log($"<color=yellow>Enter {StateKey} state</color>");
#endif
            MainManager.Ins.EventManager.AddListener<Path>(GameConstants.Events.OnCharacterReachedDestination, CheckWin);
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnBeginObjectivePhase);
        }

        /// <inheritdoc/>
        public override void Exit()
        {
            MainManager.Ins.EventManager.RemoveListener<Path>(GameConstants.Events.OnCharacterReachedDestination, CheckWin);
        }

        /// <inheritdoc/>
        public override void FixedProcess()
        {
        }

        /// <inheritdoc/>
        public override void Process()
        {
        }

        /// <summary>
        /// Checks if the character has reached the objective.
        /// </summary>
        /// <param name="pathData"><see cref="Path"/> data to check.</param>
        private void CheckWin(Path pathData)
        {
            if (pathData.HasReached)
                ChangeState(GamePhaseKeys.GameWonPhase);
            else
                ChangeState(GamePhaseKeys.GameOverPhase);
        }
    }
}
