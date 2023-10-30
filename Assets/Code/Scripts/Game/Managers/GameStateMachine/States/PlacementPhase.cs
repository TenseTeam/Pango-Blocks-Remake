namespace ProjectPBR.Managers.GameStateMachine.States
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Patterns.StateMachine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Player.Objective;
    using ProjectPBR.Managers.GameManagers;

    public class PlacementPhase : State<GameContext>
    {
        public PlacementPhase(Enum stateKey, StateMachine relatedStateMachine, Context context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
#if DEBUG
            Debug.Log($"<color=yellow>Enter {StateKey} state</color>");
#endif
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnBeginPlacementPhase);

            Context.Inputs.Interaction.Interact.canceled += TryToPlaceBlock;
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnObjectiveTriggered, ChangeToObjectivePhase);
        }

        public override void Exit()
        {
            Context.Inputs.Interaction.Interact.canceled -= TryToPlaceBlock;
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnObjectiveTriggered, ChangeToObjectivePhase);
        }
        
        public override void FixedProcess()
        {
        }

        public override void Process()
        {
            if (Context.Inputs.Interaction.Interact.IsInProgress())
            {
                if (Context.BlocksManager.Dragger.IsDragging) return;

                TryToGetBlock();
                CheckObjectiveTouch();
            }
        }

        private void CheckObjectiveTouch()
        {
            RaycastHit2D hit = (MainManager.Ins.GameManager as GameManager).MobileInputsManager.RaycastFromTouch2D(MainManager.Ins.GameConfig.PlayerLayerMask);

            if (hit && hit.transform.TryGetComponent(out ObjectiveTrigger objective))
                objective.Trigger();
        }

        private void TryToGetBlock()
        {
            if (Context.BlocksManager.TryGetBlockFromTouch(out PlaceableBlock block, out Vector2 offset))
            {
                Context.BlocksManager.Dragger.StartDrag(block, offset);
            }
        }

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

        private void ResetBlockInHand(PlaceableBlock block)
        {
            Context.BlocksManager.LerpResetBlockInHand(block);
            ChangeToFallPhase();
        }

        private void ChangeToFallPhase()
        {
            Context.BlocksManager.Dragger.StopDrag();

            if(!Context.Grid.IsGridEmpty()) // No need to go the fall phase if the grid is empty
                ChangeState(GamePhaseKeys.FallPhase);
        }

        private void ChangeToObjectivePhase()
        {
            ChangeState(GamePhaseKeys.ObjectivePhase);
        }
    }
}
