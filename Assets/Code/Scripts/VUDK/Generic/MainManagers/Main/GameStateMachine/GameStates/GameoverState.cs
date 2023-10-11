namespace VUDK.Generic.MainManagers.Main.GameStateMachine.GameStates
{
    using System;
    using UnityEngine;
    using VUDK.Generic.MainManagers.Main.GameStateMachine.Contexts;
    using VUDK.Patterns.StateMachine;

    public class GameoverState : State<GameStateMachineContext>
    {
        public GameoverState(Enum stateKey, StateMachine relatedStateMachine, Context context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
#if DEBUG
            Debug.Log("Gameover Triggered.");
#endif
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
