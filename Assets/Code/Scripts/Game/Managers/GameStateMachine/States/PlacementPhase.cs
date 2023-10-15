namespace ProjectPBR.Managers.GameStateMachine.States
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Patterns.StateMachine;
    using ProjectPBR.Player.PlayerHandler;
    using ProjectPBR.Level.Grid;
    using ProjectPBR.Level.Blocks;

    public class PlacementPhase : State<GameContext>
    {
        public PlacementPhase(Enum stateKey, StateMachine relatedStateMachine, Context context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
            Context.Inputs.Interaction.Interact.canceled += TryToPlaceBlock;
        }

        public override void Exit()
        {
            Context.Inputs.Interaction.Interact.canceled -= TryToPlaceBlock;
        }

        public override void FixedProcess()
        {
        }

        public override void Process()
        {
            if (Context.Inputs.Interaction.Interact.IsInProgress()) 
            {
                if (!Context.BlocksController.Dragger.IsDragging && TryGetBlockFromTouch(out PlaceableBlock block))
                {
                    Context.BlocksController.Dragger.StartDrag(block);
                }
            }
        }

        private void TryToPlaceBlock(InputAction.CallbackContext context)
        {
            if (!Context.BlocksController.Dragger.IsDragging) return;

            if (Context.GameManager.MobileInputsManager.IsTouchOn(out PlayerHandLayout layout))
            {
                layout.InsertInBounds(Context.GameManager.BlocksController.Dragger.CurrentDraggedBlock);
                ResetBlockInHand(Context.GameManager.BlocksController.Dragger.CurrentDraggedBlock);
                return;
            }

            if (Context.GameManager.MobileInputsManager.IsTouchOn(out LevelTile tile) && 
                Context.GameManager.GridBlocksManager.AreTilesFreeForBlock(tile, Context.BlocksController.Dragger.CurrentDraggedBlock))
            {
                PlaceBlockOnGrid(Context.BlocksController.Dragger.CurrentDraggedBlock, tile);
                return;
            }

            // If nothing happens, reset the block in hand
            ResetBlockInHand(Context.BlocksController.Dragger.CurrentDraggedBlock);
        }

        private bool TryGetBlockFromTouch(out PlaceableBlock block)
        {
            RaycastHit2D hit = Context.GameManager.MobileInputsManager.Raycast2DFromTouch(Context.GameManager.GridBlocksManager.BlocksLayerMask);

            if (hit && hit.transform.TryGetComponent(out PlaceableBlock bl))
            {
                block = Context.BlocksController.PlayerHand.Layout.GetAndRemoveFromHand(bl);
                return true;
            }

            block = null;
            return false;
        }

        //private void PlaceBlockInHand(PlaceableBlock block)
        //{
        //    Context.GameManager.BlocksController.PlaceBlockInHand(block);
        //    ChangeToFallPhase();
        //}

        private void ResetBlockInHand(PlaceableBlock block)
        {
            Context.GameManager.BlocksController.ResetBlockInHand(block);
            ChangeToFallPhase();
        }

        private void PlaceBlockOnGrid(PlaceableBlock block, LevelTile tile)
        {
            Context.GameManager.BlocksController.PlaceBlockOnGrid(block, tile);
            ChangeToFallPhase();
        }

        private void ChangeToFallPhase()
        {
            Context.BlocksController.Dragger.StopDrag();
            ChangeState(GamePhaseKeys.FallPhase);
        }
    }
}
