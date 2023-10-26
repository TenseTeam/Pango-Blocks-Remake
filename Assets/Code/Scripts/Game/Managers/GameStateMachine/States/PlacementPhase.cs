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
    using ProjectPBR.Player.Objective;

    public class PlacementPhase : State<GameContext>
    {
        public PlacementPhase(Enum stateKey, StateMachine relatedStateMachine, Context context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
#if DEBUG
            Debug.Log("Placement Phase");
#endif
            Context.Inputs.Interaction.Interact.canceled += TryToPlaceBlock;
            MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnBeginPlacementPhase);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnObjectiveTriggered, ChangeToObjectivePhase);
        }

        public override void Exit()
        {
            Context.Inputs.Interaction.Interact.canceled -= TryToPlaceBlock;
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnObjectiveTriggered, ChangeToObjectivePhase);
        }
        
        public override void FixedProcess()
        {
        }

        public override void Process()
        {
            if (Context.Inputs.Interaction.Interact.IsInProgress() && !Context.BlocksManager.Dragger.IsDragging) 
            {
                CheckObjectiveTouch(); // Did it this way due to a Unity's bug causing false events triggering

                if (TryGetBlockFromTouch(out PlaceableBlock block))
                {
                    Context.BlocksManager.Dragger.StartDrag(block);
                }
            }
        }

        private void CheckObjectiveTouch()
        {
            RaycastHit2D hit = Context.GameManager.MobileInputsManager.RaycastFromTouch2D(MainManager.Ins.GameConfig.PlayerLayerMask);

            if (hit && hit.transform.TryGetComponent(out ObjectiveTrigger objective))
                objective.Trigger();
        }

        private void TryToPlaceBlock(InputAction.CallbackContext context)
        {
            if (!Context.BlocksManager.Dragger.IsDragging) return;

            if (Context.GameManager.MobileInputsManager.IsTouchOn2D(out PlayerHandLayout layout, Context.GameManager.BlocksManager.PlayerHand.Layout.LayoutMask))
            {
                layout.SetResetPositionInLayoutBounds(Context.GameManager.BlocksManager.Dragger.CurrentDraggedBlock);
                ResetBlockInHand(Context.GameManager.BlocksManager.Dragger.CurrentDraggedBlock);
                return;
            }

            if (Context.GameManager.MobileInputsManager.IsTouchOn2D(out LevelTile tile, ~Context.GameManager.GameGridManager.BlocksLayerMask) && 
                Context.GameManager.GameGridManager.AreTilesFreeForBlock(tile, Context.BlocksManager.Dragger.CurrentDraggedBlock))
            {
                PlaceBlockOnGrid(Context.BlocksManager.Dragger.CurrentDraggedBlock, tile);
                return;
            }

            // If nothing happens, reset the block in hand
            ResetBlockInHand(Context.BlocksManager.Dragger.CurrentDraggedBlock);
        }

        private bool TryGetBlockFromTouch(out PlaceableBlock block)
        {
            RaycastHit2D hit = Context.GameManager.MobileInputsManager.RaycastFromTouch2D(Context.GameManager.GameGridManager.BlocksLayerMask);
            if (hit && hit.transform.TryGetComponent(out PlaceableBlock bl) && !bl.IsResettingPosition)
            {
                block = Context.BlocksManager.PlayerHand.Layout.GetAndRemoveFromHand(bl);
                return true;
            }

            block = null;
            return false;
        }

        private void ResetBlockInHand(PlaceableBlock block)
        {
            Context.GameManager.BlocksManager.LerpResetBlockInHand(block);
            ChangeToFallPhase();
        }

        private void PlaceBlockOnGrid(PlaceableBlock block, LevelTile tile)
        {
            Context.GameManager.BlocksManager.PlaceBlockOnGrid(block, tile);
            ChangeToFallPhase();
        }

        private void ChangeToFallPhase()
        {
            Context.BlocksManager.Dragger.StopDrag();
            ChangeState(GamePhaseKeys.FallPhase);
        }

        private void ChangeToObjectivePhase()
        {
            ChangeState(GamePhaseKeys.ObjectivePhase);
        }
    }
}
