namespace ProjectPBR.Patterns.Factories
{
    using ProjectPBR.Managers.GameManagers;
    using ProjectPBR.Managers.GameStateMachine;
    using ProjectPBR.Managers.SceneManager;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Generic.Managers.Main;

    public static class ContextsFactory
    {
        public static GameContext Create()
        {
            GameManager gm = MainManager.Ins.GameManager as GameManager;
            return new GameContext(InputsManager.Inputs, gm.BlocksManager, gm.GameGridManager, MainManager.Ins.SceneManager as GameSceneManager);
        }
    }
}