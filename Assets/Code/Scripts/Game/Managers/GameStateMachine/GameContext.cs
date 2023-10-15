namespace ProjectPBR.Managers.GameStateMachine
{
    using VUDK.Generic.MainManagers.Main.GameStateMachine.Contexts;

    public class GameContext : GameMachineContext
    {
        public GameManager GameManager { get; private set; }
        public BlocksController BlocksController => GameManager.BlocksController;

        public GameContext(InputsMap inputs, GameManager gameManager) : base(inputs)
        {
            GameManager = gameManager;
        }
    }
}
