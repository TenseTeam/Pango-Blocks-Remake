namespace ProjectPBR.Managers.GameStateMachine.States
{
    using System;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.StateMachine;
    using ProjectPBR.Config.Constants;

    public class ObjectivePhase : State<GameContext>
    {
        public ObjectivePhase(Enum stateKey, StateMachine relatedStateMachine, Context context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
#if DEBUG
            Debug.Log("Objective Phase");
#endif

            MainManager.Ins.EventManager.AddListener(Constants.Events.OnObjectiveAchieved, ChangeToGamewon);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnCantReachObjective, ChangeToGameover);

            MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnBeginObjectivePhase);
        }

        public override void Exit()
        {
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnObjectiveAchieved, ChangeToGamewon);
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnCantReachObjective, ChangeToGameover);
        }

        public override void FixedProcess()
        {
        }

        public override void Process()
        {
        }

        private void ChangeToGameover()
        {
            ChangeState(GamePhaseKeys.GameOverPhase);
        }

        private void ChangeToGamewon()
        {
            ChangeState(GamePhaseKeys.GameWonPhase);
        }
    }
}
