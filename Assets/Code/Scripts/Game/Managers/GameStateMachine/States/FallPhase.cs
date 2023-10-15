namespace ProjectPBR.Managers.GameStateMachine.States
{
    using System;
    using VUDK.Patterns.StateMachine;

    public class FallPhase : State<GameContext>
    {
        public FallPhase(Enum stateKey, StateMachine relatedStateMachine, Context context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
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
