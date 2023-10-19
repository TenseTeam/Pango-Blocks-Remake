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
            Debug.Log("Gameover Phase");
#endif
            MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnBeginGameoverPhase);
        }

        public override void Exit()
        {
        }

        public override void FixedProcess()
        {
        }

        public override void Process()
        {
        }
    }
}
