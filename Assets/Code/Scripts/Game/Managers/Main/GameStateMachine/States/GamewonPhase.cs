namespace ProjectPBR.Managers.Main.GameStateMachine.States
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

        /// <inheritdoc/>
        public override void Enter()
        {
#if DEBUG
            Debug.Log($"<color=green>Enter {StateKey} state</color>");
#endif
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnBeginGameWonPhase);
            Context.SceneManager.LoadNextScene();
        }

        /// <inheritdoc/>
        public override void Exit()
        {
        }

        /// <inheritdoc/>
        public override void FixedProcess()
        {
        }

        /// <inheritdoc/>
        public override void Process()
        {
        }
    }
}
