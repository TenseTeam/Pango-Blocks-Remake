namespace ProjectPBR.Managers.Main.GameStateMachine.States
{
    using System;
    using UnityEngine;
    using VUDK.Patterns.StateMachine;
    using VUDK.Generic.Serializable;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.Managers.Main.GameStateMachine.States.Keys;

    public class FallPhase : State<GameContext>
    {
        private TimeDelay _delayForCheck; // Necessary delay to make the rigidbody pick up speed

        public FallPhase(Enum stateKey, StateMachine relatedStateMachine, GameContext context) : base(stateKey, relatedStateMachine, context)
        {
            _delayForCheck = new TimeDelay(0.2f);
        }

        public override void Enter()
        {
#if DEBUG
            Debug.Log($"<color=yellow>Enter {StateKey} state</color>");
#endif
            _delayForCheck.Start();
            Context.BlocksManager.EnableBlocksGravity();
        }

        public override void Exit()
        {
            CheckBlocks();
            Context.BlocksManager.DisableBlocksGravity();
        }

        public override void FixedProcess()
        {
        }

        public override void Process()
        {
            _delayForCheck.Process();

            if (_delayForCheck.IsReady)
            {
                if (AreAllBlocksStopped())
                    ChangeState(GamePhaseKeys.PlacementPhase);
            }
        }

        private bool AreAllBlocksStopped()
        {
            foreach (PlaceableBlock block in Context.Grid.BlocksOnGrid)
            {
                if (block.IsMoving)
                    return false;
            }
            return true;
        }

        private void CheckBlocks()
        {
            Context.BlocksManager.ReturnInHandInvalidBlocks();
            Context.Grid.AdjustBlocksPositionOnGrid();
        }
    }
}
