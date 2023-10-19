namespace ProjectPBR.Managers.GameStateMachine.States
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Patterns.StateMachine;
    using VUDK.Generic.Serializable;
    using ProjectPBR.Level.Blocks;

    public class FallPhase : State<GameContext>
    {
        private TimeDelay _delayForCheck; // Necessary delay to make the rigidbody pick up speed

        public FallPhase(Enum stateKey, StateMachine relatedStateMachine, GameContext context) : base(stateKey, relatedStateMachine, context)
        {
            _delayForCheck = new TimeDelay(0.2f);
        }

        public override void Enter()
        {
            if(Context.GameManager.GameGridManager.IsGridEmpty())
            {
                ChangeState(GamePhaseKeys.PlacementPhase);
                return;
            }

            EnableGravity();
        }

        public override void Exit()
        {
            DisableGravity();
        }

        public override void FixedProcess()
        {
        }

        public override void Process()
        {
            _delayForCheck.AddDeltaTime();

            if (_delayForCheck.IsReady())
            {
                if (AreAllBlocksStopped())
                {
                    ReturnInHandInvalidBlocks();
                    ChangeState(GamePhaseKeys.PlacementPhase);
                }
            }
        }

        private bool AreAllBlocksStopped()
        {
            foreach (PlaceableBlock block in Context.GameManager.GameGridManager.BlocksOnGrid)
            {
                if (block.IsMoving)
                    return false;
            }
            return true;
        }

        private void ReturnInHandInvalidBlocks()
        {
            List<PlaceableBlock> blocks = Context.GameManager.GameGridManager.BlocksOnGrid;
            List<PlaceableBlock> invalidBlocks = blocks.FindAll(block => block.IsTilted || !block.IsInsideGrid());

            foreach (PlaceableBlock block in invalidBlocks)
                Context.BlocksController.ResetBlockInHand(block);
        }

        private void EnableGravity()
        {
            foreach (PlaceableBlock block in Context.GameManager.GameGridManager.BlocksOnGrid)
                block.EnableGravity();
        }

        private void DisableGravity()
        {
            foreach (PlaceableBlock block in Context.GameManager.GameGridManager.BlocksOnGrid)
                block.DisableGravity();
        }
    }
}
