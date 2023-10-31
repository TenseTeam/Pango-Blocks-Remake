namespace ProjectPBR.Managers.GameStateMachine.States
{
    using System;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.StateMachine;
    using ProjectPBR.GameConfig.Constants;

    public class GamewonPhase : State<GameContext>
    {
        public GamewonPhase(Enum stateKey, StateMachine relatedStateMachine, StateMachineContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
#if DEBUG
            Debug.Log($"<color=green>Enter {StateKey} state</color>");
#endif
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnBeginGameWonPhase);
            Context.SceneManager.LoadNextScene();
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
