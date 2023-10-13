namespace VUDK.Generic.Managers.Main
{
    using UnityEngine;
    using VUDK.Generic.MainManagers.Main.GameStateMachine.Contexts;
    using VUDK.Generic.MainManagers.Main.GameStateMachine.GameStates;
    using VUDK.Patterns.Factories;
    using VUDK.Patterns.StateMachine;

    [DefaultExecutionOrder(-990)]
    public class GameMachine : StateMachine
    {
        public override void Init()
        {
            GameStateMachineContext context = ContextFactory.Create();

            GameRunningState gameRunning = GameStatesFactory.Create(DefaultGameStateKeys.Running, this, context) as GameRunningState;
            GameoverState gameOver = GameStatesFactory.Create(DefaultGameStateKeys.Gameover, this, context) as GameoverState;

            AddState(DefaultGameStateKeys.Running, gameRunning);
            AddState(DefaultGameStateKeys.Gameover, gameOver);

            ChangeState(DefaultGameStateKeys.Running);
#if DEBUG
            Debug.Log("GameStateMachine initialized.");
#endif
        }
    }
}
