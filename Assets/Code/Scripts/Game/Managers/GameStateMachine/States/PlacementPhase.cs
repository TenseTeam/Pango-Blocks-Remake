namespace ProjectPBR.Managers.GameStateMachine.States
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Patterns.StateMachine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Player.PlayerHandler;
    using ProjectPBR.Level.Grid;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.Config.Constants;

    public class PlacementPhase : State<GameContext>
    {
        public PlacementPhase(Enum stateKey, StateMachine relatedStateMachine, Context context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnObjeciveTouched, ChangeToObjectivePhase);
            Context.Inputs.Interaction.Interact.canceled += TryToPlaceBlock;
        }

        public override void Exit()
        {
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnObjeciveTouched, ChangeToObjectivePhase);
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

            if (Context.GameManager.MobileInputsManager.IsTouchOn2D(out PlayerHandLayout layout, Context.GameManager.BlocksController.PlayerHand.Layout.LayoutMask))
            {
                layout.InsertInBounds(Context.GameManager.BlocksController.Dragger.CurrentDraggedBlock);
                ResetBlockInHand(Context.GameManager.BlocksController.Dragger.CurrentDraggedBlock);
                return;
            }

            if (Context.GameManager.MobileInputsManager.IsTouchOn2D(out LevelTile tile, ~Context.GameManager.GridBlocksManager.BlocksLayerMask) && 
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
            RaycastHit2D hit = Context.GameManager.MobileInputsManager.RaycastFromTouch2D(Context.GameManager.GridBlocksManager.BlocksLayerMask);

            if (hit && hit.transform.TryGetComponent(out PlaceableBlock bl))
            {
                block = Context.BlocksController.PlayerHand.Layout.GetAndRemoveFromHand(bl);
                return true;
            }

            block = null;
            return false;
        }

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

        private void ChangeToObjectivePhase()
        {
            ChangeState(GamePhaseKeys.ObjectivePhase);
        }
    }
}
