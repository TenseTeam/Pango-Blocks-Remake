namespace ProjectPBR.Managers.GameStateMachine.States
{
    using System;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.StateMachine;
    using ProjectPBR.Config.Constants;
    using ProjectPBR.Level.PathSystem;

    public class ObjectivePhase : State<GameContext>
    {
        public ObjectivePhase(Enum stateKey, StateMachine relatedStateMachine, Context context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
#if DEBUG
            Debug.Log($"<color=yellow>Enter {StateKey} state</color>");
#endif
            MainManager.Ins.EventManager.AddListener<Path>(Constants.Events.OnCharacterReachedDestination, CheckWin);
            MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnBeginObjectivePhase);
        }

        public override void Exit()
        {
            MainManager.Ins.EventManager.RemoveListener<Path>(Constants.Events.OnCharacterReachedDestination, CheckWin);
        }

        public override void FixedProcess()
        {
        }

        public override void Process()
        {
        }

        private void CheckWin(Path pathData)
        {
            if (pathData.HasReached)
                ChangeState(GamePhaseKeys.GameWonPhase);
            else
                ChangeState(GamePhaseKeys.GameOverPhase);
        }
    }
}
