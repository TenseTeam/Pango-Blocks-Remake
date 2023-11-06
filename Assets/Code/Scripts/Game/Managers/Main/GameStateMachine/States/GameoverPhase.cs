namespace ProjectPBR.Managers.Main.GameStateMachine.States
{
    using System;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.StateMachine;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Managers.Main.GameStateMachine.States.Keys;

    public class GameoverPhase : State<GameContext>
    {
        public GameoverPhase(Enum stateKey, StateMachine relatedStateMachine, StateMachineContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
#if DEBUG
            Debug.Log($"<color=yellow>Enter {StateKey} state</color>");
#endif
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnBeginGameoverPhase);
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnResetLevel, ChangeToPlacement);
        }

        public override void Exit()
        {
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnResetLevel, ChangeToPlacement);
        }

        public override void FixedProcess()
        {
        }

        public override void Process()
        {
        }

        private void ChangeToPlacement()
        {
            ChangeState(GamePhaseKeys.PlacementPhase);
        }
    }
}
