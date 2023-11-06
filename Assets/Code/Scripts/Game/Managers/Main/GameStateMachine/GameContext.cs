namespace ProjectPBR.Managers.Main.GameStateMachine
{
    using VUDK.Generic.MainManagers.Main.Bases.GameStateMachine.Contexts;
    using ProjectPBR.Managers.Main.GameManagers;
    using ProjectPBR.Managers.Main.GameManagers.BlocksManagement;
    using ProjectPBR.Managers.Main.SceneManager;

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
