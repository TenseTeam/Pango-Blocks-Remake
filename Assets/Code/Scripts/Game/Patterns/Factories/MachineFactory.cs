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
    using System;

    /// <summary>
    /// Factory for Game's StateMachines
    /// </summary>
    public static class MachineFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="GameContext"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="GameContext"/>.</returns>
        public static GameContext Create()
        {
            GameManager gm = MainManager.Ins.GameManager as GameManager;
            return new GameContext(InputsManager.Inputs, gm.BlocksManager, gm.GameGridManager, MainManager.Ins.SceneManager as GameSceneManager);
        }

        /// <summary>
        /// Creates a new state based on the specified <see cref="GamePhaseKeys"/>.
        /// </summary>
        /// <param name="stateKey">The key identifying the game phase.</param>
        /// <param name="relatedStateMachine">The state machine to which the new state belongs.</param>
        /// <param name="context">The game context associated with the state.</param>
        /// <returns>A new state instance corresponding to the provided <see cref="GamePhaseKeys"/>.</returns>
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