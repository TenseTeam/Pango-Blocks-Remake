namespace ProjectPBR.Managers.GameStateMachine.States
{
    using System;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.StateMachine;
    using ProjectPBR.Config.Constants;
    using ProjectPBR.Level.PathSystem.Data;

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
            MainManager.Ins.EventManager.AddListener<PathData>(Constants.Events.OnCharacterReachedDestination, CheckWin);
            MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnBeginObjectivePhase);
        }

        public override void Exit()
        {
            MainManager.Ins.EventManager.RemoveListener<PathData>(Constants.Events.OnCharacterReachedDestination, CheckWin);
        }

        public override void FixedProcess()
        {
        }

        public override void Process()
        {
        }

        private void CheckWin(PathData pathData)
        {
            if (pathData.HasReached)
                ChangeState(GamePhaseKeys.GameWonPhase);
            else
                ChangeState(GamePhaseKeys.GameOverPhase);
        }
    }
}
