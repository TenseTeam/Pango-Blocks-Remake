namespace VUDK.Patterns.Factories
{
    using VUDK.Generic.MainManagers.Main.GameStateMachine.Contexts;
    using VUDK.Generic.MainManagers.Main.GameStateMachine.GameStates;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.StateMachine;

    public class GameStatesFactory
    {
        public static State<GameStateMachineContext> Create(DefaultGameStateKeys gameStateKey, GameStateMachine stateMachine, GameStateMachineContext context) 
        {
            switch (gameStateKey)
            {
                case DefaultGameStateKeys.Gameover:
                    return new GameoverState(gameStateKey, stateMachine, context);

                //case DefaultGameStateKeys.GamePlay:
                //    break;

                //case DefaultGameStateKeys.GamePause:
                //    break;

                //case DefaultGameStateKeys.GameStart:
                //    break;
            }

            return null;
        }
    }
}
