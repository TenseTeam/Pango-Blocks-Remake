namespace ProjectPBR.Patterns.Factories
{
    using VUDK.Features.Main.InputSystem;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.StateMachine;
    using ProjectPBR.Managers.Main.GameManagers;
    using ProjectPBR.Managers.Main.GameStateMachine;
    using ProjectPBR.Managers.Main.GameStateMachine.States.Keys;
    using ProjectPBR.Managers.Main.SceneManager;
    using ProjectPBR.Managers.Main.GameStateMachine.States;

    /// <summary>
    /// Factory for Game's StateMachines
    /// </summary>
    public static class MachineFactory
    {
        public static GameContext Create()
        {
            GameManager gm = MainManager.Ins.GameManager as GameManager;
            return new GameContext(InputsManager.Inputs, gm.BlocksManager, gm.GameGridManager, MainManager.Ins.SceneManager as GameSceneManager);
        }

        public static State<GameContext> Create(GamePhaseKeys stateKey, StateMachine relatedStateMachine, GameContext context)
        {
            switch (stateKey)
            {
                case GamePhaseKeys.PlacementPhase:
                    return new PlacementPhase(stateKey, relatedStateMachine, context);
                case GamePhaseKeys.ObjectivePhase:
                    return new ObjectivePhase(stateKey, relatedStateMachine, context);
                case GamePhaseKeys.FallPhase:
                    return new FallPhase(stateKey, relatedStateMachine, context);
                case GamePhaseKeys.GameOverPhase:
                    return new GameoverPhase(stateKey, relatedStateMachine, context);
                case GamePhaseKeys.GameWonPhase:
                    return new GamewonPhase(stateKey, relatedStateMachine, context);
            }

            return null;
        }
    }
}