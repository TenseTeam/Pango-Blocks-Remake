namespace ProjectPBR.Managers.Main.GameStateMachine
{
    using VUDK.Generic.Managers.Main.Bases;
    using ProjectPBR.Patterns.Factories;
    using ProjectPBR.Managers.Main.GameStateMachine.States.Keys;
    using ProjectPBR.Managers.Main.GameStateMachine.States;

    public class PBRGameMachine : GameMachineBase
    {
        /// <inheritdoc/>
        public override void Init()
        {
            base.Init();
            GameContext context = MachineFactory.Create();

            PlacementPhase placementPhase = MachineFactory.Create(GamePhaseKeys.PlacementPhase, this, context) as PlacementPhase;
            ObjectivePhase objectivePhase = MachineFactory.Create(GamePhaseKeys.ObjectivePhase, this, context) as ObjectivePhase;
            FallPhase fallPhase = MachineFactory.Create(GamePhaseKeys.FallPhase, this, context) as FallPhase;
            GameoverPhase gameoverPhase = MachineFactory.Create(GamePhaseKeys.GameOverPhase, this, context) as GameoverPhase;
            GamewonPhase gamewonPhase = MachineFactory.Create(GamePhaseKeys.GameWonPhase, this, context) as GamewonPhase;

            AddState(GamePhaseKeys.GameOverPhase, gameoverPhase);
            AddState(GamePhaseKeys.GameWonPhase, gamewonPhase);
            AddState(GamePhaseKeys.PlacementPhase, placementPhase);
            AddState(GamePhaseKeys.ObjectivePhase, objectivePhase);
            AddState(GamePhaseKeys.FallPhase, fallPhase);

            ChangeState(GamePhaseKeys.PlacementPhase);
        }
    }
}
