namespace ProjectPBR.Managers.GameStateMachine
{
    using VUDK.Generic.MainManagers.Main.GameStateMachine.Contexts;
    using ProjectPBR.Managers.GameManagers;
    using ProjectPBR.Managers.GameManagers.BlocksManagement;
    using ProjectPBR.Managers.SceneManager;

    public class GameContext : GameMachineContext
    {
        public BlocksManager BlocksManager { get; private set; }
        public GameGridManager Grid { get; private set; }
        public GameSceneManager SceneManager { get; private set; }

        public GameContext(InputsMap inputs, BlocksManager blocksManager, GameGridManager grid, GameSceneManager sceneManager) : base(inputs)
        {
            BlocksManager = blocksManager;
            Grid = grid;
            SceneManager = sceneManager;
        }
    }
}
