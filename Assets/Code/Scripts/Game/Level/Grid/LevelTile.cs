namespace ProjectPBR.Level.Grid
{
    using ProjectPBR.Level.Player.PlayerHandler.Blocks;
    using ProjectPBR.Player.PlayerHandler.Blocks;
    using UnityEngine;
    using VUDK.Extensions.Transform;
    using VUDK.Generic.Structures.Grid;    

    public class LevelTile : GridTileBase
    {
        public BlockBase Block { get; private set; }
        public bool IsOccupied { get; private set; }

        public void InsertBlock(PlaceableBlock block)
        {
            block.transform.parent = null;
            block.transform.position = transform.position;
            //block.transform.parent = transform;
            //block.transform.localPosition = Vector3.zero;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BlockBase block))
            {
                Block = block;
                IsOccupied = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IsOccupied = false;
        }
    }
}
