namespace ProjectPBR.Managers.Main.GameManagers.BlocksManagement
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.Level.Grid;
    using ProjectPBR.Player.PlayerHandler;

    public class BlocksManager : MonoBehaviour, ICastGameManager<GameManager>
    {
        [SerializeField, Min(0f), Header("Reset Time")]
        private float _resetBlockTime;

        [field: SerializeField, Header("Blocks Dragger")]
        public BlockDragger Dragger { get; private set; }

        [field: SerializeField, Header("Player Hand")]
        public PlayerHand PlayerHand { get; private set; }

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        private GameGridManager _grid => GameManager.GameGridManager;

        private void OnValidate()
        {
            if(!PlayerHand)
                PlayerHand = FindObjectOfType<PlayerHand>();
        }

        /// <summary>
        /// Reset the block in the player hand layout by starting its lerp.
        /// </summary>
        /// <param name="block"><see cref="PlaceableBlockBase"/> to reset.</param>
        public void RemoveFromGridAndPlaceInHand(PlaceableBlockBase block)
        {
            _grid.RemoveBlockFromGrid(block);
            block.PlaceableBlockReset();
            //MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnBlockStartReset, block);
            PlayerHand.Layout.LerpPositionToHand(block, _resetBlockTime);
        }

        public bool TryToPlaceBlockOnGrid()
        {
            if (GameManager.MobileInputsManager.IsTouchOn2D(out LevelGrid grid, _grid.GridLayerMask))
            {
                PlaceableBlockBase block = Dragger.CurrentDraggedBlock;
                LevelTile closestTile = _grid.GetClosestTile(block.transform.position);

                if(_grid.AreTilesFreeForBlock(closestTile, block))
                {
                    block.Place();
                    //MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnBlockPlaced, block);
                    _grid.Insert(closestTile, block);
                    return true;
                }
            }
            return false;
        }

        public bool TryToPlaceBlockInHand()
        {
            if (GameManager.MobileInputsManager.IsTouchOn2D(out PlayerHandLayout layout, PlayerHand.Layout.LayoutMask))
            {
                layout.SetResetPositionInLayoutBounds(Dragger.CurrentDraggedBlock);
                RemoveFromGridAndPlaceInHand(Dragger.CurrentDraggedBlock);
                return true;
            }

            return false;
        }

        public bool TryGetBlockFromTouch(out PlaceableBlockBase block, out Vector2 touchOffsetPosition)
        {
            RaycastHit2D hit = GameManager.MobileInputsManager.RaycastFromTouch2D(_grid.BlocksLayerMask);

            if (hit && hit.transform.TryGetComponent(out PlaceableBlockBase bl) && !bl.IsResettingPosition)
            {
                block = PlayerHand.Layout.GetAndRemoveFromHand(bl);
                touchOffsetPosition = hit.point - (Vector2)block.transform.position;
                return true;
            }

            block = null;
            touchOffsetPosition = Vector3.zero;
            return false;
        }

        /// <summary>
        /// Return all the invalid blocks in the player hand layout.
        /// </summary>
        public void ReturnInHandInvalidBlocks()
        {
            List<PlaceableBlockBase> blocks = _grid.BlocksOnGrid;
            List<PlaceableBlockBase> invalidBlocks = blocks.FindAll(block => block.IsInvalid() || block.IsTilted());

            foreach (PlaceableBlockBase block in invalidBlocks)
                RemoveFromGridAndPlaceInHand(block);
        }

        /// <summary>
        /// Enable the gravity for all the blocks on the grid.
        /// </summary>
        public void EnableBlocksGravity()
        {
            foreach (PlaceableBlockBase block in _grid.BlocksOnGrid)
                block.EnableGravity();
        }

        /// <summary>
        /// Disable the gravity for all the blocks on the grid.
        /// </summary>
        public void DisableBlocksGravity()
        {
            foreach (PlaceableBlockBase block in _grid.BlocksOnGrid)
                block.DisableGravity();
        }
    }
}
