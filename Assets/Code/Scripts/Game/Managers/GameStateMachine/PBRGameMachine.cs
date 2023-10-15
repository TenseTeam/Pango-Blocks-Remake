namespace ProjectPBR.Managers
{
    using System;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.Patterns.Factories;
    using ProjectPBR.Managers.GameStateMachine;
    using ProjectPBR.Managers.GameStateMachine.States;
    using ProjectPBR.Player.PlayerHandler;

    [Serializable]
    public struct GameContextData
    {
        public BlockDragger dragger;
        public PlayerHandLayout handLayout;
    }

    public class PBRGameMachine : GameMachine
    {
        [field: SerializeField, Header("Context Data")]
        public GameContextData GameContextData { get; private set; }

        public override void Init()
        {
            base.Init();
            GameContext context = ContextsFactory.Create(GameContextData);

            PlacementPhase placementPhase = StatesFactory.Create(GamePhaseKeys.PlacementPhase, this, context) as PlacementPhase;
            ObjectivePhase objectivePhase = StatesFactory.Create(GamePhaseKeys.ObjectivePhase, this, context) as ObjectivePhase;

            AddState(GamePhaseKeys.PlacementPhase, placementPhase);
            AddState(GamePhaseKeys.ObjectivePhase, objectivePhase);

            ChangeState(GamePhaseKeys.PlacementPhase);
        }
    }
}
