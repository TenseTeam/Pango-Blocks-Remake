namespace ProjectPBR.Managers
{
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Patterns.Factories;
    using ProjectPBR.Managers.GameStateMachine;
    using ProjectPBR.Managers.GameStateMachine.States;
    using ProjectPBR.Config.Constants;

    public class PBRGameMachine : GameMachineBase
    {
        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnResetLevel, ResetMachine);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnResetLevel, ResetMachine);
        }

        public override void Init()
        {
            base.Init();
            GameContext context = ContextsFactory.Create();

            PlacementPhase placementPhase = StatesFactory.Create(GamePhaseKeys.PlacementPhase, this, context) as PlacementPhase;
            ObjectivePhase objectivePhase = StatesFactory.Create(GamePhaseKeys.ObjectivePhase, this, context) as ObjectivePhase;
            FallPhase fallPhase = StatesFactory.Create(GamePhaseKeys.FallPhase, this, context) as FallPhase;
            GameoverPhase gameoverPhase = StatesFactory.Create(GamePhaseKeys.GameOverPhase, this, context) as GameoverPhase;
            GamewonPhase gamewonPhase = StatesFactory.Create(GamePhaseKeys.GameWonPhase, this, context) as GamewonPhase;

            AddState(GamePhaseKeys.GameOverPhase, gameoverPhase);
            AddState(GamePhaseKeys.GameWonPhase, gamewonPhase);
            AddState(GamePhaseKeys.PlacementPhase, placementPhase);
            AddState(GamePhaseKeys.ObjectivePhase, objectivePhase);
            AddState(GamePhaseKeys.FallPhase, fallPhase);

            ChangeState(GamePhaseKeys.PlacementPhase);
        }

        private void ResetMachine()
        {
            ChangeState(GamePhaseKeys.PlacementPhase);
        }
    }
}
