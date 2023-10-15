namespace ProjectPBR.Managers.GameStateMachine
{
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.Level.Grid;
    using ProjectPBR.Player;
    using ProjectPBR.Player.PlayerHandler;
    using VUDK.Features.Main.InputSysten.MobileInputs;
    using VUDK.Generic.MainManagers.Main.GameStateMachine.Contexts;
    using VUDK.Generic.Managers.Main;

    public class GameContext : GameStateMachineContext
    {
        public MobileInputsManager MobileInputs { get; private set; }
        public GameGridManager Grid { get; private set; } 
        public BlockDragger Dragger { get; private set; }
        public PlayerHandLayout HandLayout { get; private set; }

        public GameContext(InputsMap inputs, MobileInputsManager mobileInputs, GameGridManager grid, GameContextData contextData) : base(inputs)
        {
            MobileInputs = mobileInputs;
            Grid = grid;
            Dragger = contextData.dragger;
            HandLayout = contextData.handLayout;
        }
    }
}
