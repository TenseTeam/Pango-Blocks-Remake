namespace VUDK.Patterns.Factories
{
    using VUDK.Generic.MainManagers.Main.GameStateMachine.Contexts;
    
    public static class ContextFactory
    {
        public static GameStateMachineContext Create()
        {
            return new GameStateMachineContext();
        }
    }
}
