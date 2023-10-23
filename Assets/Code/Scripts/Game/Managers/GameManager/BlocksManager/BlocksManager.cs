namespace ProjectPBR.Managers
{
    using UnityEngine;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.Level.Grid;
    using ProjectPBR.Player.PlayerHandler;

    public class BlocksManager : MonoBehaviour
    {
        [field: SerializeField, Header("Blocks Dragger")]
        public BlockDragger Dragger { get; private set; }
        
        [field: SerializeField, Header("Player Hand")]
        public PlayerHand PlayerHand { get; private set; }
        
        [SerializeField]
        private GameGridManager _grid;

        [SerializeField, Min(0f)]
        private float _resetBlockTime;

        /// <summary>
        /// Reset the block in the player hand layout by starting its lerp.
        /// </summary>
        /// <param name="block"><see cref="PlaceableBlock"/> to reset.</param>
        public void LerpResetBlockInHand(PlaceableBlock block)
        {
            block.EnableCollider();
            block.DisableGravity();
            block.SetIsInvalid(false);
            _grid.RemoveBlockFromGrid(block);
            PlayerHand.Layout.LerpPutItBackInHand(block, _resetBlockTime);
        }

        public void PlaceBlockOnGrid(PlaceableBlock block, LevelTile tile)
        {
            _grid.PlaceBlockOnGrid(tile, block);
        }
    }
}
