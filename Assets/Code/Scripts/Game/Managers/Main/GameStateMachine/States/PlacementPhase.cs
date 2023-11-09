namespace ProjectPBR.Managers.Main.GameStateMachine.States
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Patterns.StateMachine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Player.Objective;
    using ProjectPBR.Managers.Main.GameManagers;
    using ProjectPBR.Managers.Main.GameStateMachine.States.Keys;

    public class PlacementPhase : State<GameContext>
    {
        public PlacementPhase(Enum stateKey, StateMachine relatedStateMachine, StateMachineContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        /// <inheritdoc/>
        public override void Enter()
        {
#if DEBUG
            Debug.Log($"<color=yellow>Enter {StateKey} state</color>");
#endif
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnBeginPlacementPhase);

            Context.Inputs.Interaction.Interact.canceled += TryToPlaceBlock;
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnObjectiveTriggered, ChangeToObjectivePhase);
        }

        /// <inheritdoc/>
        public override void Exit()
        {
            Context.Inputs.Interaction.Interact.canceled -= TryToPlaceBlock;
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnObjectiveTriggered, ChangeToObjectivePhase);
        }

        /// <inheritdoc/>
        public override void FixedProcess()
        {
        }

        /// <inheritdoc/>
        public override void Process()
        {
            if (Context.Inputs.Interaction.Interact.IsInProgress())
            {
                if (Context.BlocksManager.Dragger.IsDragging) return;

                TryToGetBlock();
                CheckObjectiveTouch();
            }
        }

        /// <summary>
        /// Checks if the player is touching an objective and triggers it if so.
        /// </summary>
        private void CheckObjectiveTouch()
        {
            RaycastHit2D hit = (MainManager.Ins.GameManager as GameManager).MobileInputsManager.RaycastFromTouch2D(MainManager.Ins.GameStats.PlayerLayerMask);

            if (hit && hit.transform.TryGetComponent(out ObjectiveTrigger objective))
                objective.Trigger();
        }

        /// <summary>
        /// Tries to get a block from the touch position.
        /// </summary>
        private void TryToGetBlock()
        {
            if (Context.BlocksManager.TryGetBlockFromTouch(out PlaceableBlockBase block, out Vector2 offset))
            {
                Context.BlocksManager.Dragger.StartDrag(block, offset);
            }
        }

        /// <summary>
        /// Tries to place the block in hand or in grid.
        /// </summary>
        private void TryToPlaceBlock(InputAction.CallbackContext context)
        {
            if (!Context.BlocksManager.Dragger.IsDragging) return;

            if (Context.BlocksManager.TryToPlaceBlockInHand() ||
                Context.BlocksManager.TryToPlaceBlockOnGrid())
            {
                ChangeToFallPhase();
                return;
            }

            // If nothing happens, reset the block in hand
            ResetBlockInHand(Context.BlocksManager.Dragger.CurrentDraggedBlock);
        }

        /// <summary>
        /// Remove the block from the grid, place it in hand and change to fall phase.
        /// </summary>
        /// <param name="block">Block to remove.</param>
        private void ResetBlockInHand(PlaceableBlockBase block)
        {
            Context.BlocksManager.RemoveFromGridAndPlaceInHand(block);
            ChangeToFallPhase();
        }

        /// <summary>
        /// Changes to fall phase.
        /// </summary>
        private void ChangeToFallPhase()
        {
            Context.BlocksManager.Dragger.StopDrag();

            if(!Context.Grid.IsGridEmpty()) // No need to go the fall phase if the grid is empty
                ChangeState(GamePhaseKeys.FallPhase);
        }

        /// <summary>
        /// Changes to objective phase.
        /// </summary>
        private void ChangeToObjectivePhase()
        {
            ChangeState(GamePhaseKeys.ObjectivePhase);
        }
    }
}
