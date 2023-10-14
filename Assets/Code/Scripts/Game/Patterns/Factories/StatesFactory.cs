namespace ProjectPBR.Patterns.Factories
{
    using VUDK.Patterns.StateMachine;
    using ProjectPBR.Managers.GameStateMachine;
    using ProjectPBR.Managers.GameStateMachine.States;

    public static class StatesFactory
    {
        public static State<GameContext> Create(StateKeys stateKey, StateMachine relatedStateMachine, GameContext context)
        {
            switch (stateKey)
            {
                case StateKeys.Check:
                    return new CheckState(stateKey, relatedStateMachine, context);
                case StateKeys.Objective:
                    return new ObjectiveState(stateKey, relatedStateMachine, context);
            }

            return null;
        }
    }
}
