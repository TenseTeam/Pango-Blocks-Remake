namespace ProjectPBR.Managers
{
    using ProjectPBR.Factories;
    using ProjectPBR.Managers.GameStateMachine;
    using ProjectPBR.Managers.GameStateMachine.States;
    using VUDK.Generic.Managers.Main;

    public class PBRStateMachine : GameMachine
    {
        public override void Init()
        {
            base.Init();
            GameContext context = ContextsFactory.Create();

            CheckState check = StatesFactory.Create(StateKeys.Check, this, context) as CheckState;
            ObjectiveState objective = StatesFactory.Create(StateKeys.Objective, this, context) as ObjectiveState;

            AddState(StateKeys.Check, check);
            AddState(StateKeys.Objective, objective);
        }
    }
}
