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
        private TimeDelay _delayForCheck;

        public FallPhase(Enum stateKey, StateMachine relatedStateMachine, GameContext context) : base(stateKey, relatedStateMachine, context)
        {
            _delayForCheck = new TimeDelay(0.2f);
        }

        public override void Enter()
        {
            if(Context.GameManager.GridBlocksManager.IsGridEmpty())
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
                    ReturnInHandTiltedBlocks();
                    ChangeState(GamePhaseKeys.PlacementPhase);
                }
            }
        }

        private bool AreAllBlocksStopped()
        {
            foreach (PlaceableBlock block in Context.GameManager.GridBlocksManager.BlocksOnGrid)
            {
                if (block.IsMoving)
                    return false;
            }
            return true;
        }

        private void ReturnInHandTiltedBlocks()
        {
            List<PlaceableBlock> blocks = Context.GameManager.GridBlocksManager.BlocksOnGrid;

            for(int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].IsTilted)
                {
                    Context.BlocksController.ResetBlockInHand(blocks[i]);
                }
            }
        }

        private void EnableGravity()
        {
            foreach (PlaceableBlock block in Context.GameManager.GridBlocksManager.BlocksOnGrid)
                block.EnableGravity();
        }

        private void DisableGravity()
        {
            foreach (PlaceableBlock block in Context.GameManager.GridBlocksManager.BlocksOnGrid)
                block.DisableGravity();
        }
    }
}
