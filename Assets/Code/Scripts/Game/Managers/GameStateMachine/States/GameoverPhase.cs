namespace ProjectPBR.Managers.GameStateMachine.States
{
    using ProjectPBR.Config.Constants;
    using System;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.StateMachine;

    public class GameoverPhase : State<GameContext>
    {
        public GameoverPhase(Enum stateKey, StateMachine relatedStateMachine, Context context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
#if DEBUG
            Debug.Log($"<color=red>Enter {StateKey} state</color>");
#endif
            MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnBeginGameoverPhase);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnResetLevel, ChangeToPlacement);
        }

        public override void Exit()
        {
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnResetLevel, ChangeToPlacement);
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
