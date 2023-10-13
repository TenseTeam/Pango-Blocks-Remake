namespace ProjectPBR.Factories
{
    using VUDK.Patterns.StateMachine;
    using ProjectPBR.Managers.GameStateMachine;

    public static class ContextsFactory
    {
        public static GameContext Create()
        {
            return new GameContext();
        }
    }
}
