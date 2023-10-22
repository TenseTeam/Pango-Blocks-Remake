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

        public void ResetBlockInHand(PlaceableBlock block)
        {
            block.EnableCollider();
            block.ResetPosition();
            _grid.RemoveBlockFromGrid(block);
            PlayerHand.Layout.ResetBlockInLayout(block);
        }

        public void PlaceBlockOnGrid(PlaceableBlock block, LevelTile tile)
        {
            _grid.PlaceBlockOnGrid(tile, block);
        }

        public void PlaceBlockInHand(PlaceableBlock block)
        {
            _grid.RemoveBlockFromGrid(block);
            PlayerHand.Layout.ResetBlockInLayout(block);
        }
    }
}
