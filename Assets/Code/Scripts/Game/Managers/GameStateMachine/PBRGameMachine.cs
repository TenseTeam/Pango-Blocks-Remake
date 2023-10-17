namespace ProjectPBR.Managers
{
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Patterns.Factories;
    using ProjectPBR.Managers.GameStateMachine;
    using ProjectPBR.Managers.GameStateMachine.States;

    public class PBRGameMachine : GameMachineBase
    {
        public override void Init()
        {
            base.Init();
            GameContext context = ContextsFactory.Create();

            PlacementPhase placementPhase = StatesFactory.Create(GamePhaseKeys.PlacementPhase, this, context) as PlacementPhase;
            ObjectivePhase objectivePhase = StatesFactory.Create(GamePhaseKeys.ObjectivePhase, this, context) as ObjectivePhase;
            FallPhase fallPhase = StatesFactory.Create(GamePhaseKeys.FallPhase, this, context) as FallPhase;

            AddState(GamePhaseKeys.PlacementPhase, placementPhase);
            AddState(GamePhaseKeys.ObjectivePhase, objectivePhase);
            AddState(GamePhaseKeys.FallPhase, fallPhase);

            ChangeState(GamePhaseKeys.PlacementPhase);
        }
    }
}
