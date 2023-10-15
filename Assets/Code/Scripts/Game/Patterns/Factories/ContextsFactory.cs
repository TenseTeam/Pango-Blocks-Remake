namespace ProjectPBR.Patterns.Factories
{
    using VUDK.Patterns.StateMachine;
    using ProjectPBR.Managers.GameStateMachine;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Managers;
    using ProjectPBR.Player;

    public static class ContextsFactory
    {
        public static GameContext Create(GameContextData contextData)
        {
            PBRGameManager gm = (MainManager.Ins.GameManager as PBRGameManager);

            return new GameContext(
                InputsManager.Inputs,
                gm.MobileInputsManager, 
                gm.GridBlocksManager,
                contextData);
        }
    }
}
