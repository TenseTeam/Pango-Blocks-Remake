namespace ProjectPBR.Patterns.Factories
{
    using VUDK.Patterns.StateMachine;
    using ProjectPBR.Managers.GameStateMachine;
    using ProjectPBR.Managers.GameStateMachine.States;

    public static class StatesFactory
    {
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
