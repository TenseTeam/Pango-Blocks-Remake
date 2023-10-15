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
                if (!Context.Dragger.IsDragging && TryGetBlockFromTouch(out PlaceableBlock block))
                {
                    Context.Dragger.StartDrag(block);
                }
            }
        }

        private void TryToPlaceBlock(InputAction.CallbackContext context)
        {
            if (!Context.Dragger.IsDragging) return;

            if (Context.MobileInputs.IsTouchOn(out PlayerHandLayout layout))
            {
                layout.InsertInBounds(Context.Dragger.CurrentDraggedBlock);
                ResetBlockInHand(Context.Dragger.CurrentDraggedBlock);
                return;
            }

            if (Context.MobileInputs.IsTouchOn(out LevelTile tile) && 
                Context.Grid.AreTilesFreeForBlock(tile, Context.Dragger.CurrentDraggedBlock))
            {
                Context.Grid.PlaceBlockInGrid(tile, Context.Dragger.CurrentDraggedBlock);
                Context.Dragger.StopDrag();
                return;
            }

            // If nothing happens, reset the block in hand
            ResetBlockInHand(Context.Dragger.CurrentDraggedBlock);
        }

        private bool TryGetBlockFromTouch(out PlaceableBlock block)
        {
            RaycastHit2D hit = Context.MobileInputs.Raycast2DFromTouch(Context.Grid.BlocksLayerMask);

            if (hit && hit.transform.TryGetComponent(out PlaceableBlock bl))
            {
                block = Context.HandLayout.GetAndRemoveFromHand(bl);
                return true;
            }

            block = null;
            return false;
        }

        private void ResetBlockInHand(PlaceableBlock block)
        {
            block.EnableCollider();
            block.ResetPosition();
            Context.HandLayout.ResetBlockInLayout(block);
            Context.Dragger.StopDrag();
        }
    }
}
