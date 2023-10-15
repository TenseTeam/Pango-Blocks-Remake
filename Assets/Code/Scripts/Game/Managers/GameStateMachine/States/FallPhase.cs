namespace ProjectPBR.Managers.GameStateMachine.States
{
    using System;
    using UnityEngine;
    using ProjectPBR.Level.Blocks;
    using VUDK.Patterns.StateMachine;
    using System.Collections.Generic;

    public class FallPhase : State<GameContext>
    {
        private float delayBeforeCheck = 1.0f;
        private float currentTime = 0.0f;

        public FallPhase(Enum stateKey, StateMachine relatedStateMachine, GameContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
            currentTime = 0.0f;
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
            currentTime += Time.deltaTime;

            if (currentTime >= delayBeforeCheck)
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
